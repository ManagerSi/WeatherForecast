using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI.DB.Interface;
using System.Data.SqlClient;
using System.Reflection;

namespace SI.DB.SqlServer
{
  public class SqlServerHelper : IDBHelper {
    public SqlServerHelper() {
      Console.WriteLine("SqlServerHelper is Created");
    }
    public void Query() {
      Console.WriteLine("SqlServerHelper is Query");
    }

    public T GetByID<T>(int id) {
      Type type = typeof(T);
      string columsString = string.Join(",",type.GetProperties().Select(i => string.Format("[{0}]",i.Name)));
      string sql = string.Format("SELECT {0} FROM {1} WHERE id ={2}",
        columsString,type.Name,id);
      #region 
      //T t = default(T);
      //T t = (T)Activator.CreateInstance(type);
      //using(SqlConnection cnn = new SqlConnection()) {
      //  using(SqlCommand cmd = new SqlCommand(sql,cnn)) {
      //    if(cnn.State != System.Data.ConnectionState.Open) {
      //      cnn.Open();
      //    }
      //    SqlDataReader sr = cmd.ExecuteReader();
      //    if(sr.Read()) {
      //      foreach(var item in type.GetProperties()) {
      //        item.SetValue(t,sr[item.Name]);
      //      }
      //    }
      //    cnn.Close();
      //  }
      //}

      //return t;
      #endregion
      #region
      //Func<SqlCommand,T> func = c => {
      //  T obj = (T)Activator.CreateInstance(type);
      //  SqlDataReader dr = c.ExecuteReader();
      //  while(dr.Read()) {
      //    foreach(var item in type.GetProperties()) {
      //      item.SetValue(obj,dr[item.Name]);
      //    }
      //  };
      //  return obj;
      //};
      //return ExcuteSql(sql,func);
      #endregion
      return ExcuteSql(sql,c => {
        T obj = (T)Activator.CreateInstance(type);
        SqlDataReader dr = c.ExecuteReader();
        while(dr.Read()) {
          foreach(var item in type.GetProperties()) {
            item.SetValue(obj,dr[item.Name]);
          }
        };
        return obj;
      });
    }

    public List<T> GetList<T>(string whereStr) {
      Type type = typeof(T);
      string columString = string.Join(",",type.GetProperties().Select(i => string.Format("[{0}]",i.Name)));
      string sql = string.Format("select {0} from {1} where {2}",
        columString,type.Name,whereStr);
      return ExcuteSql<List<T>>(sql,c => {
        List<T> list = new List<T>();
        T obj = (T)Activator.CreateInstance(type);
        SqlDataReader dr = c.ExecuteReader();
        while(dr.Read()) {
          foreach(var item in type.GetProperties()) {
            item.SetValue(obj,dr[item.Name]);
          }
          list.Add(obj);
        };
        return list;
      });
    }

    public int Update<T>(T obj) {
      Type type = typeof(T);
      //string columString = string.Join(",",type.GetProperties().Select(i => string.Format("[{0}]=@{0}",i.Name)));
      string fileds = "";
      string idvalues = "";
      List<SqlParameter> sqlparams = new List<SqlParameter>();
      foreach(var item in type.GetProperties()) {
        if(item.Name == ("id")) {
           idvalues = item.GetValue(obj,null).ToString();
          continue;
        }
        fileds += item.Name + "=@" + item.Name + ",";
        var val = item.GetValue(obj,null);
        sqlparams.Add(new SqlParameter(item.Name,(val ?? DBNull.Value)));
      }
      fileds = fileds.Substring(0,fileds.Length - 1);
      string sql = string.Format("update {0} set {1} where id = {2}",
        type.Name,fileds,idvalues);
      return ExcuteSql<int>(sql,c => {
        return c.ExecuteNonQuery();
      });
    }

    public T ExcuteSql<T>(string sql,Func<SqlCommand,T> fun) {
      using(SqlConnection conn = new SqlConnection()) {
        conn.Open();
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.CommandType = System.Data.CommandType.Text;
        return fun(cmd);
      }
    }
    /// <summary>
    /// 异常处理
    /// </summary>
    /// <param name="act"></param>

    public void SaveInvoke(Action act) {
      try {
        act.Invoke();
      } catch(Exception e) {
        Console.WriteLine(e.Message);
      }
    }
    public T SaveFuncInvoke<T>(Func<T> fun) {
      try {
        return fun.Invoke();
      } catch(Exception e) {
        Console.WriteLine(e.Message);
        return default(T);
      }
    }
  }
}
