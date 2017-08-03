using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Magnum.Concurrency;
using Magnum.Concurrency.Internal;
using WeatherForecast.Cache;
using MongoDB.Bson;
using OfficeOpenXml;
using OfficeOpenXml.Style;

using Ionic.Zip;
using System.Threading.Tasks;
namespace WeatherForecast.Utility {
  public class ExcelExportColumnSelector<TEntity> {
    public String Title { get; set; }
    public Func<TEntity, object> Selector;
    public Func<TEntity, string> Commentor;
    public Action<ExcelRange, object> ConfigFunc;
    public double Width { get; set; }
    public bool IsEmpty { get; set; }
  }



  public class ExcelExporter<TEntity> {

    public const string ExcelNumberFmtIntMoney = "#,##0";
    public const string ExcelNumberFmtMoney = "#,##0.00";
    public const string ExcelNumberIntPercentFmt = "0%";
    public const string ExcelNumberOnePointPercentFmt = "0.0%";
    public const string ExcelNumberTwoPointPercentFmt = "0.00%";
    public const string ExcelNumberFmtNoPoint = "0";
    public const string ExcelNumberFmtOnePoint = "0.0";
    public const string ExcelNumberFmtTwoPoint = "0.00";
    public const string ExcelNumberFmtFourPoint = "0.0000";
    public const string ExcelNumberFmtSixPoint = "0.000000";
    public ExcelExporter() {
      selectors = new List<ExcelExportColumnSelector<TEntity>>();
      AddStandHeader = true;
      //SkipHeader = true;
    }
    protected List<ExcelExportColumnSelector<TEntity>> selectors;
    public Func<ExcelWorksheet, int> headerFuntion = null;
    public Func<ExcelWorksheet, int> footerFuntion = null;
    public bool AddStandHeader { get; set; }
    //public bool SkipHeader { get; set; }
    public ExcelExporter<TEntity> SetHeaderFunc(Func<ExcelWorksheet, int> func, bool addStandHeader = true) {
      headerFuntion = func;
      AddStandHeader = addStandHeader;
      return this;
    }
    public ExcelExporter<TEntity> SetFooterFunc(Func<ExcelWorksheet, int> func) {
      footerFuntion = func;
      return this;
    }
    public ExcelExporter<TEntity> AddColumn(string title, Func<TEntity, object> selector, Action<ExcelRange, object> configFunc = null,Func<TEntity, string> commentor =null, double width = -1) {
      selectors.Add(new ExcelExportColumnSelector<TEntity> {
        Title = title,
        Selector = selector,
        ConfigFunc = configFunc,
        Commentor = commentor,
        Width = width,
      });
      return this;
    }

    public ExcelExporter<TEntity> AddColumnWithFmt(string title, Func<TEntity, object> selector, string fmt,
      Func<TEntity, string> commentor = null,
      object defaultVal = null, double width = -1) {
      selectors.Add(new ExcelExportColumnSelector<TEntity> {
        Title = title,
        Selector = selector,
        Commentor = commentor,
        ConfigFunc = (c, v) => {
          if (v == null & defaultVal != null) {
            c.Value = defaultVal;
          } else {
            c.Value = v;
          }
          c.Style.Numberformat.Format = fmt;
        },
        Width = width,
      });
      return this;
    }

    public ExcelExporter<TEntity> AddEmptyColumn(/*string title, double width = -1*/) {
      selectors.Add(new ExcelExportColumnSelector<TEntity> {
        IsEmpty = true
      });
      return this;
    }

    public void SetContentDispositionHeader(HttpResponseBase respose, string fileName = null, bool autoLimit = true) {
      if (autoLimit && HttpContext.Current != null) {
        // 检查是否有超时
        var url = HttpContext.Current.Request.RawUrl;
        bool exist = true;
        
        DataCache.GetOrDefaultBySession("export_report" + url, () => {
          exist = false;
          return "e";
        }, 30);

        if (exist) throw new Exception("报表导出请间隔30秒");
      }
      // 增加TimeOut
      ExcelZippedExporter.SetContentDispositionHeader(respose, fileName);
    }

    public byte[] ConvertToZipFile(Func<IEnumerable<TEntity>> entitiesFunc, string sheetName = "Sheet1") {
      return ConvertToZipFile(entitiesFunc(), sheetName);
    }

    internal string TemplateFileName { get; set; }

    public async Task<byte[]> ConvertToZipFileSync(IEnumerable<TEntity> entities, string sheetName = "Sheet1", bool enableProfileLog = true)
    {
        var zipper = new ExcelZippedExporter();
        zipper.TemplateFileName = TemplateFileName;
        zipper.AddExport(sheetName, this, entities);
        zipper.EnableProfileLog = enableProfileLog;
        return await zipper.BuildZipFileSync();
    }

    public byte[] ConvertToZipFile(IEnumerable<TEntity> entities, string sheetName = "Sheet1", bool enableProfileLog= true) {

      


        var zipper = new ExcelZippedExporter();
        zipper.TemplateFileName = TemplateFileName;
        zipper.AddExport(sheetName, this, entities);
        zipper.EnableProfileLog = enableProfileLog;
        return zipper.BuildZipFile();

      /*
      if (string.IsNullOrEmpty(TemplateFileName)) {
        using (ExcelPackage pck = new ExcelPackage()) {
          var ws = pck.Workbook.Worksheets.Add(sheetName);
          RenderWorkSheet(entities, ws);
          return ZipPackage(pck);
        }
      } else {
        using (var fs = new FileStream(TemplateFileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
          using (ExcelPackage pck = new ExcelPackage(fs)) {
            ExcelWorksheet ws = pck.Workbook.Worksheets.First(i => i.Name == sheetName);
            RenderWorkSheet(entities, ws);

            var dataRange = ws.Cells["A1:" + ws.Dimension.End.Address.ToString()];
            // 刷新所有透视表
            foreach (var w in pck.Workbook.Worksheets) {
              foreach (var p in w.PivotTables) {
                p.CacheDefinition.SourceRange = dataRange;
              }
            }

            return ZipPackage(pck);
          }
        }
      }*/
    }

    //private static byte[] ZipPackage(ExcelPackage pck) {
    //  // Zip it
    //  using (MemoryStream ms = new MemoryStream()) {
    //    using (ZipFile zip = new ZipFile()) {
    //      zip.AddEntry(Util.RPCNow.Ticks.ToString() + ".xlsx", pck.GetAsByteArray());
    //      zip.Save(ms);
    //    }
    //    return ms.ToArray();
    //  }
    //}
    internal void RenderWorkSheet(IEnumerable<TEntity> entities, ExcelWorksheet ws) {

      int offset = 0;
      if (headerFuntion != null) {
        offset += headerFuntion(ws);
      }
      if (AddStandHeader) {
        ++offset;
        //if (!SkipHeader) {
          for (int i = 0; i < selectors.Count; ++i) {
            if (selectors[i].IsEmpty) {
              continue;
            }
            using (var cell = ws.Cells[offset, i + 1]) {
              cell.Value = selectors[i].Title;
              //cell.Style.Font.Bold = true;
              cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
              //cell.Style.Fill.PatternColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 153));
              cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 153));
              cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
              cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
              cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
              cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

              if (selectors[i].Width > 0) {
                ws.Column(i + 1).Width = selectors[i].Width;
              }
            }
          }
       // }
      }
      var widthArray = new int[selectors.Count];
      int row = 0;
      foreach (var e in entities) {
        ++row;
        for (int i = 0; i < selectors.Count; ++i) {
          var selector = selectors[i];
          if (selector.IsEmpty) {
            continue;
          }
          if (selector.Selector == null) {
            continue;
          }

          var text = selector.Selector(e);
          if (text != null)
          {
              var width = text.ToString().Length;
              if (width > widthArray[i])
                  widthArray[i] = width;
          }
          //if (!string.IsNullOrEmpty(text)) {
          //  ws.Cells[row, i + 1].Value = text;
          //}
          
          if (selector.Commentor != null || selector.ConfigFunc != null) {
          //  if(true){
            // http://epplus.codeplex.com/discussions/397172
            using (var cell = ws.Cells[offset + row, i + 1]) {
              if (selector.ConfigFunc != null) {
                selector.ConfigFunc(cell, text);
              } else {
                cell.Value = text;
              }
              // Add Comment
              if (selector.Commentor != null) {
                var comment = selector.Commentor(e);
                if (!string.IsNullOrEmpty(comment)) {
                  cell.AddComment(comment, null);
                }
              }
            }
          } else {
            ws.SetValue(offset + row, i + 1,text);
          }
          
         
        }
      }
      for (int i = 0; i < selectors.Count; ++i) {
        if (selectors[i].Width < 0) {
          //ws.Column(i + 1).AutoFit();// ws.Column(i + 1).Width = textLength[i] * 2 + 1;
          ws.Column(i + 1).Width = widthArray[i] * 2 + 1;
        }
      }
      if (null != footerFuntion) {
        footerFuntion(ws);
      }
    }



    public void LoadTemplate(string filename) {
      TemplateFileName = filename;
      //SkipHeader = true;
    }
    public string ContentType {
      get {
        //return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; 
        return "application/zip";
      }
    }
  }
}
