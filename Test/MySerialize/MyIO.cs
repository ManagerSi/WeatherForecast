using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MySerialize {
  public class MyIO {

    public static void Show() {
      {
        //check 文件夹
        if(!Directory.Exists(MyConstant.FilePath)) {//检测文件夹是否存在

        }
        DirectoryInfo directory = new DirectoryInfo(MyConstant.FilePath);//不存在不抱错，注意exist属性
        Console.WriteLine(string.Format("{0},{1},{2}",directory.FullName,directory.CreationTime,directory.LastWriteTime));
        //文件
        if(!File.Exists(Path.Combine(MyConstant.FilePath,"info.txt"))) {//把几个字符窜拼装完整路径，自动加上//

        }
        FileInfo fileInfo = new FileInfo(Path.Combine(MyConstant.FilePath,"info.txt"));
        Console.WriteLine(string.Format("{0},{1},{2}",fileInfo.FullName,fileInfo.CreationTime,fileInfo.LastWriteTime));
      }

      {
        //Directory 创建、移动、删除
        if(!Directory.Exists(MyConstant.FilePath)) {
          DirectoryInfo dInfo = Directory.CreateDirectory(MyConstant.FilePath);//一次性创建所有子路径
          Directory.Move(MyConstant.FilePath,MyConstant.FileMovePath);
          Directory.Delete(MyConstant.FileMovePath);//删除
        }
       }
      {//file 创建、删除
        string fileName = Path.Combine(MyConstant.FilePath,"log.txt");
        string fileCopy = Path.Combine(MyConstant.FilePath,"coyp.info");

        if(!File.Exists(fileName)) {
          Directory.CreateDirectory(MyConstant.FilePath);//创建文件夹后，才能创建里面的文件
          //1 创建
          using(FileStream filestream = File.Create(fileName)) {//打开文件流（穿件并写入））
            string name = "123456";
            byte[] bytes = Encoding.Default.GetBytes(name);
            filestream.Write(bytes,0,bytes.Length);
            filestream.Flush();
          }
          //2
          using(FileStream filestream = File.Create(fileName)) {//打开文件流（穿件并写入））
            StreamWriter sw = new StreamWriter(filestream);
            sw.WriteLine("321645");
            sw.Flush();
          }

          using(StreamWriter sw = File.AppendText(MyConstant.FilePath)) {//追加
            string msg = "今天上课";
            sw.WriteLine(msg);
            sw.Flush();
          }
          using(StreamWriter sw = File.AppendText(MyConstant.FilePath)) {//追加
            string name = "shangke";
            byte[] bytes = Encoding.Default.GetBytes(name);
            sw.BaseStream.Write(bytes,0,bytes.Length);
            sw.Flush();
          }
        }

      }

     }
  }
}
