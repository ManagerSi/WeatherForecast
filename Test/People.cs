using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest {
  public class People {
    public int Age { get; set; }
    public string Name { get; set; }

    public void SayHi() {
      Console.WriteLine("{0},{1}",Name,Age);
    }
  }
}
