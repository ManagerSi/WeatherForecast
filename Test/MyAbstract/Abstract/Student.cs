using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAbstract.Abstract {
  public class Student {
    public decimal Id { get; set; }
    public string Name { get; set; }
  
    public  void PlayPhone(BasePhone phone) {
      Console.WriteLine("user {0},{1},use {2} ",this.Id.ToString(),this.Name,phone.Brand());
    }


  }
}
