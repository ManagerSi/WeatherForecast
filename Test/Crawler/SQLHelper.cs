using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.Crawler {
  public class SQLHelper {
    
    /// 一个有效的数据库连接对象 
    /// 命令类型(存储过程,命令文本或其它.) 
    /// T存储过程名称或T-SQL语句 
    /// SqlParamter参数数组 
    /// 返回影响的行数 
    public static int ExecuteNonQueryForProduct(List<Product> prodList) {
      int count =0;
      //string dbConnectStr = System.Configuration.ConfigurationSettings.AppSettings["DBContext"].ToString();
      var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;
      using(SqlConnection connection = new SqlConnection(connectionString)) {
        if(connection.State != ConnectionState.Open) {
          connection.Open();
        }
        // 创建SqlCommand命令,并进行预处理 
        using(SqlCommand cmd = new SqlCommand()) {
          cmd.Connection = connection;
             cmd.CommandText = "insert into JD_Product values(@pid,@sku,@url,@img,@price,@title)";
          foreach(var prod in  prodList){
            try {
              cmd.Parameters.Add(new SqlParameter("@pid",prod.pid));
              cmd.Parameters.Add(new SqlParameter("@sku",prod.sku));
              cmd.Parameters.Add(new SqlParameter("@url",prod.url));
              cmd.Parameters.Add(new SqlParameter("@img",prod.img));
              cmd.Parameters.Add(new SqlParameter("@price",prod.price));
              cmd.Parameters.Add(new SqlParameter("@title",prod.title));
              // Finally, execute the command 
              int retval = cmd.ExecuteNonQuery();
              if(retval == 0) {
                Console.WriteLine("插入错误：");
              }
              count += retval;
            } catch (Exception e){
              Console.WriteLine("插入错误："+e.Message);
            }          
            // 清除参数,以便再次使用. 
            cmd.Parameters.Clear();
          }
         
        }
        connection.Close();
      }
      
      return count;
    }

    public static int ExecuteNonQueryForCity(List<City> cityList) {
      int count = 0;
      //string dbConnectStr = System.Configuration.ConfigurationSettings.AppSettings["DBContext"].ToString();
      var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;
      using(SqlConnection connection = new SqlConnection(connectionString)) {
        if(connection.State != ConnectionState.Open) {
          connection.Open();
        }
        // 创建SqlCommand命令,并进行预处理 
        using(SqlCommand cmd = new SqlCommand()) {
          cmd.Connection = connection;
          cmd.CommandText = "insert into base_city(ID,name,Code,Contry,Loc_x,Loc_y,Org_Level,ParentCode,ParentID,state) values(@ID,@name,@Code,@Contry,@Loc_x,@Loc_y,@Org_Level,@ParentCode,@ParentID,@state)";
          foreach(var city in cityList) {
            try {
              if(string.IsNullOrEmpty(city.Name))
                city.Name = "";
              if(string.IsNullOrEmpty(city.Code))
                city.Code = "";
              if(string.IsNullOrEmpty(city.Contry))
                city.Contry = "";
              if(string.IsNullOrEmpty(city.Loc_x))
                city.Loc_x = "";
              if(string.IsNullOrEmpty(city.Loc_y))
                city.Loc_y = "";
              if(string.IsNullOrEmpty(city.Org_Level))
                city.Org_Level = "";
              if(string.IsNullOrEmpty(city.ParentCode))
                city.ParentCode = "";

              cmd.Parameters.Add(new SqlParameter("@ID",city.ID));
              cmd.Parameters.Add(new SqlParameter("@name",city.Name));
              cmd.Parameters.Add(new SqlParameter("@Code",city.Code));
              cmd.Parameters.Add(new SqlParameter("@Contry",city.Contry));             
              cmd.Parameters.Add(new SqlParameter("@Loc_x",city.Loc_x));  
              cmd.Parameters.Add(new SqlParameter("@Loc_y",city.Loc_y));
              cmd.Parameters.Add(new SqlParameter("@Org_Level",city.Org_Level));
              cmd.Parameters.Add(new SqlParameter("@ParentCode",city.ParentCode));
              cmd.Parameters.Add(new SqlParameter("@ParentID",city.ParentID));
              cmd.Parameters.Add(new SqlParameter("@state","1"));
              // Finally, execute the command 
              int retval = cmd.ExecuteNonQuery();
              if(retval == 0) {
                Console.WriteLine("插入错误：");
              }
              count += retval;
            } catch(Exception e) {
              Console.WriteLine("插入错误：" + e.Message);
            }
            // 清除参数,以便再次使用. 
            cmd.Parameters.Clear();
          }

        }
        connection.Close();
      }

      return count;
    }
  }
}
