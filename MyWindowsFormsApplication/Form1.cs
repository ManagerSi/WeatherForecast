using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWindowsFormsApplication {
  public partial class Form1 : Form {
    public Form1() {
      InitializeComponent();
    }
    public delegate void TestHander(string something);
    private void button1_Click(object sender,EventArgs e) {
      Console.WriteLine("同步方法开始");
      for(int i = 0;i < 5;i++) {
        this.doSomeThingLong("同步");
      }
      Console.WriteLine("同步方法结束");

    }

    private void button2_Click(object sender,EventArgs e) {
      Console.WriteLine("异步方法开始");
      for(int i = 0;i < 5;i++) {
        var hander = new TestHander(this.doSomeThingLong);
        hander.BeginInvoke("异步",null,null);
      }
      Console.WriteLine("异步方法结束");
    }

    private void doSomeThingLong(string name) {
      Console.WriteLine("doSomeThingLong,{0},{1},{2}",name,System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(),System.DateTime.Now);
      for(int i = 0;i < 100000;i++) {
        for(int j = 0;j < 10000;j++) {

        }
      }
    }

  }
}
