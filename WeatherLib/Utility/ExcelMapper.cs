using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace WeatherForecast.Utility {
  public class ExcelMapperSelector<TEntity> where TEntity : class {
    public string ColumnName { get; set; }
    public int ColumnIdx { get; set; }
    //public Expression<Func<TEntity, TResult>> MapTarget { get; set; }
    //public Action<TEntity, object> MapFunc { get; set;}
    internal Action<TEntity, Object> SetValueFunc { get; set; }
  }

  public class ExcelMapperValidator<TEntity> where TEntity:class,new() {
    public string ColumnName { get; set; }
    public int ColumnIdx { get; set; }
    internal Func<ExcelMapper<TEntity>, TEntity, object, bool> ValidateFunc { get; set; }

  }

  /// <summary>
  /// 映射过程中发生错误
  /// </summary>
  public class ExcelMapperExcpetion : Exception {
    public ExcelMapperExcpetion(string sheetName, int row, string columnName, Exception innerException)
      : this(sheetName, row, columnName, "数据导入错误", innerException) {
      Row = row;
      ColumnName = columnName;
    }

    public ExcelMapperExcpetion(string sheetName, int row, string columnName, string message, Exception innerException)
      : base(message, innerException) {
      SheetName = sheetName;
      Row = row;
      ColumnName = columnName;
    }
    public string SheetName { get; set; }
    public int Row { get; set; }
    public string ColumnName { get; set; }
    //public Exception InnerException { get; set; }
  }
  public class ExcelMapper<TEntity> : IDisposable
    where TEntity : class, new() {
    /// <summary>
    /// 建立导入
    /// </summary>
    /// <param name="inFile">文件名</param>
    /// <param name="worksheetOffset">第N个工作表</param>
    /// <param name="headOffset">跳过的头部高度(行)</param>
    /// <param name="contentOffset">内容开始位置(行)</param>
    public ExcelMapper(Stream inFile, int worksheetOffset = 1, int headOffset = 1, int contentOffset = 2, bool autoReset = true) {
      pck = new ExcelPackage(inFile);
      if (autoReset) {
        Reset(worksheetOffset, headOffset, contentOffset);
      }
    }

    public void Reset(int worksheetOffset = 1, int headOffset = 1, int contentOffset = 2) {
      WorksheetOffset = worksheetOffset;
      HeadOffset = headOffset;
      ContentOffset = contentOffset;
      selectors = new List<ExcelMapperSelector<TEntity>>();
      commentSelectors = new List<ExcelMapperSelector<TEntity>>();
      validators = new List<ExcelMapperValidator<TEntity>>();
      UpdateFunc = null;
      ParseDocumnet();
    }

    public static int GetSheetNum(Stream inFile) {
      var p = new ExcelPackage(inFile);
      return p.Workbook.Worksheets.Count;
    }

    public int SheetNum {
      get { return pck.Workbook.Worksheets.Count; }
    }
    public string CurrentSheetName {
      get { return pck.Workbook.Worksheets[WorksheetOffset].Name; }
    }

    /// <summary>
    /// 设置映射结果
    /// 对每一列，都会调用member= mapFunc(this[columnName])
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="columnName">对应的Excel列名</param>
    /// <param name="member">选择要映射到成员</param>
    /// <param name="mapFunc">映射函数，如果为空，就调用ToString</param>
    /// <returns></returns>
    public ExcelMapper<TEntity> MapFor<TResult>(string columnName, Expression<Func<TEntity, TResult>> member, Func<object, TResult> mapFunc = null) {
      var map = new ExcelMapperSelector<TEntity>() {
        ColumnName = columnName,
        ColumnIdx = IndexDict[columnName],
        SetValueFunc = null,
      };

      if (mapFunc == null) {

        ParameterExpression objectParameterExpression = Expression.Parameter(typeof(TEntity)), valueParameterExpression = Expression.Parameter(typeof(string));
        Expression<Action<TEntity, string>> setValueExpression = Expression.Lambda<Action<TEntity, string>>(
            Expression.Block(
                Expression.Assign(Expression.Property(objectParameterExpression, ((MemberExpression)member.Body).Member.Name), valueParameterExpression)
            ), objectParameterExpression, valueParameterExpression
        );
        Action<TEntity, string> setValue = setValueExpression.Compile();
        map.SetValueFunc = (e, val) => {
          setValue(e, val.ToString());
        };
      } else {


        ParameterExpression objectParameterExpression = Expression.Parameter(typeof(TEntity)), valueParameterExpression = Expression.Parameter(typeof(TResult));
        Expression<Action<TEntity, TResult>> setValueExpression = Expression.Lambda<Action<TEntity, TResult>>(
            Expression.Block(
                Expression.Assign(Expression.Property(objectParameterExpression, ((MemberExpression)member.Body).Member.Name), valueParameterExpression)
            ), objectParameterExpression, valueParameterExpression
        );
        Action<TEntity, TResult> setValue = setValueExpression.Compile();

        map.SetValueFunc = (e, val) => {
          TResult rst = mapFunc(val);
          setValue(e, rst);
        };
      }
      selectors.Add(map);
      return this;
    }


    /// <summary>
    /// 用于映射Comment的函数
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    public ExcelMapper<TEntity> MapCommentFor(string columnName, Expression<Func<TEntity, string>> member) {
      var map = new ExcelMapperSelector<TEntity>() {
        ColumnName = columnName,
        ColumnIdx = IndexDict[columnName],
        SetValueFunc = null,
      };

      

        ParameterExpression objectParameterExpression = Expression.Parameter(typeof(TEntity)), valueParameterExpression = Expression.Parameter(typeof(string));
        Expression<Action<TEntity, string>> setValueExpression = Expression.Lambda<Action<TEntity, string>>(
            Expression.Block(
                Expression.Assign(Expression.Property(objectParameterExpression, ((MemberExpression)member.Body).Member.Name), valueParameterExpression)
            ), objectParameterExpression, valueParameterExpression
        );
        Action<TEntity, string> setValue = setValueExpression.Compile();
        map.SetValueFunc = (e, val) => {
          setValue(e, (string)val);
        };
        commentSelectors.Add(map);
        return this;
    }
    /// <summary>
    /// 映射完成后，总体变更函数
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ExcelMapper<TEntity> MapFunc(Action<ExcelMapper<TEntity>, TEntity> func) {
      UpdateFunc = func;
      return this;
    }

    public ExcelMapper<TEntity> ValidateFor(string columnName, Func<ExcelMapper<TEntity>, TEntity, object, bool> validateFunc) {
      this.validators.Add(new ExcelMapperValidator<TEntity>() {
        ColumnName = columnName,
        ColumnIdx = IndexDict[columnName],
        ValidateFunc = validateFunc,
      });
      return this;
    }

    /// <summary>
    /// 映射一行
    /// </summary>
    /// <param name="entity">需要更新的对象，为空时新建</param>
    /// <returns></returns>
    public TEntity MapRow(TEntity entity = null) {
      if (entity == null) {
        entity = new TEntity();
      }
      foreach (var s in selectors) {
        var obj = this[s.ColumnIdx];
        try {
          s.SetValueFunc(entity, obj);
        } catch (Exception e) {
          if (e is KeyNotFoundException) {
            throw new ExcelMapperExcpetion(CurrentSheetName, CurrentRow, s.ColumnName, "给定关键词不在字典中", e);
          } else if (e is FormatException) {
            throw new ExcelMapperExcpetion(CurrentSheetName, CurrentRow, s.ColumnName, "格式错误", e);
          }
          throw new ExcelMapperExcpetion(CurrentSheetName, CurrentRow, s.ColumnName, e);
        }
      }

      if (UpdateFunc != null) {
        try {
          UpdateFunc(this, entity);
        } catch (Exception e) {
          throw new ExcelMapperExcpetion(CurrentSheetName, CurrentRow, "未知", e);
        }
      }

      foreach (var s in commentSelectors) {
        var obj = this.GetComment(s.ColumnIdx);
        try {
          s.SetValueFunc(entity, obj);
        } catch (Exception e) {
          throw new ExcelMapperExcpetion(CurrentSheetName, CurrentRow, s.ColumnName + "#Comment", e);
        }
      }

      foreach (var v in validators) {
        bool rst = true;

        try {
          var obj = this[v.ColumnIdx];
          rst = v.ValidateFunc(this, entity,obj);
        } catch (Exception e) {
          throw new ExcelMapperExcpetion(CurrentSheetName, CurrentRow, v.ColumnName, e);
        }
        if (!rst) {
          throw new ExcelMapperExcpetion(CurrentSheetName, CurrentRow, v.ColumnName, "数据验证失败", null);
        }
      }

      //if (ValidateFunc != null) {
      //  try {
      //    if (!ValidateFunc(this, entity)) {
      //      throw new ExcelMapperExcpetion(CurrentSheetName, CurrentRow, "未知", "数据验证失败");
      //    }
      //  } catch (Exception e) {
      //    throw new ExcelMapperExcpetion(CurrentSheetName, CurrentRow, "未知", e);
      //  }
      //}

      return entity;
    }

    /// <summary>
    /// 返回所有行的映射
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TEntity> ReadAndMapAllRow() {
      while (this.Read()) {
        yield return MapRow();
      }
    }

    protected List<ExcelMapperSelector<TEntity>> selectors;
    protected List<ExcelMapperSelector<TEntity>> commentSelectors;
    protected List<ExcelMapperValidator<TEntity>> validators;
    protected ExcelPackage pck;
    protected ExcelWorksheet ws;
    protected int WorksheetOffset { get; set; }
    protected int HeadOffset { get; set; }
    protected int ContentOffset { get; set; }
    protected int CurRow { get; set; }
    protected int MaxRow { get; set; }
    protected Dictionary<string, int> IndexDict { get; set; }
    protected Action<ExcelMapper<TEntity>, TEntity> UpdateFunc { get; set; }

    public int CurrentRow { get { return CurRow; } }
    private void ParseDocumnet() {
      ws = pck.Workbook.Worksheets[WorksheetOffset];
      var maxColumn = ws.Dimension.End.Column;
      MaxRow = ws.Dimension.End.Row;
      IndexDict = new Dictionary<string, int>(maxColumn);
      for (int i = 1; i <= maxColumn; ++i) {
        var colunmName = (ws.Cells[HeadOffset, i].Value ?? "").ToString();
        if (string.IsNullOrEmpty(colunmName) || IndexDict.ContainsKey(colunmName)) {
          continue;
        }
        IndexDict.Add(colunmName.Trim(), i);
      }
      CurRow = ContentOffset - 1;
    }

    public bool Read() {
      ++CurRow;
      return CurRow <= MaxRow;
    }

    public void Close() {
      ws = null;
      pck.Dispose();
      pck = null;
      IndexDict = null;
      selectors = null;
      UpdateFunc = null;
    }
    public bool IsHideenSheet {get { return ws.Hidden != eWorkSheetHidden.Visible; }}
    /// <summary>
    /// 根据index序号取得对应列
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public object this[int index] {
      get {
        var cell = ws.Cells[CurRow, index];
        return cell.Value;
      }
    }

    /// <summary>
    /// 根据列名取得对应object
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public object this[string name] {
      get {
        var idx = IndexDict[name.Trim()];
        return this[idx];
      }
    }

    /// <summary>
    /// 获取对应列单元格上的批注
    /// </summary>
    /// <returns></returns>
    public string GetComment(int index) {
      var cell = ws.Cells[CurRow,index];
      var comment = cell.Comment;
      if (comment == null) {
        return null;
      }
      return comment.Text;
    }
    /// <summary>
    /// 获取对应列单元格上的批注
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetComment(string name) {
      var idx = IndexDict[name];
      return GetComment(idx);
    }

    /// <summary>
    /// 是否包含某一列
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool ContainColumn(string name) {
      return IndexDict.ContainsKey(name);
    }

    /// <summary>
    /// 用于返回特定的行和列
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public ExcelRange GetCell(int row, int col) {
      return ws.Cells[row, col];
    }

    #region IDisposable 成员
    public void Dispose() {
      Close();
    }

    #endregion


  }
}
