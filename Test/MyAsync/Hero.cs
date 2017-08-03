using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest.MyAsync {
  public class Hero {
    public string Name { get; set; }
    private Random random = new Random();
    public void StoryOne(string Key) {
      for(int i = 0;i < 300000000;i++) {
        i++;
      }
      Console.WriteLine("{0}-故事一:{1}",this.Name,Key);
    }
    public void StoryTwo(string Key) {
      for(int i = 0;i < 30000000;i++) {
        i++;
      }
      Console.WriteLine("{0}-故事二:{1}",this.Name,Key);
    }
    public void StoryThree(string Key) {
      for(int i = 0;i < 30000000;i++) {
        i++;
      }
      Console.WriteLine("{0}-故事三:{1}",this.Name,Key);
    }
    public void StoryFour(string Key) {
      for(int i = 0;i < 300000000;i++) {
        i++;
      }
      Console.WriteLine("{0}-故事四:{1}",this.Name,Key);
    }
  }
}
