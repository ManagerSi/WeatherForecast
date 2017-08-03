using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest.MyAsync {
  public class AsyncMain {

    public AsyncMain() {
      //示例
      ThreadCoreLong();

      Console.WriteLine(".....天龙八部。。。。");
      Hero q = new Hero() { Name="乔峰"};
      Hero d = new Hero() { Name = "段誉" };
      Hero x = new Hero() { Name = "虚竹" };
      Stopwatch sw = new Stopwatch();
      sw.Start();
      List<Task> taskList = new List<Task>();
      List<Task> cTaskList = new List<Task>();

      var qTask = Task.Factory.StartNew(hero => q.StoryOne("丐帮帮主"),q);
      var dTask = Task.Factory.StartNew(hero => d.StoryOne("钟灵儿"),d);
      var xTask = Task.Factory.StartNew(hero => x.StoryOne("小和尚"),x);
      taskList.Add(qTask);
      taskList.Add(dTask);
      taskList.Add(xTask);
      Task.Factory.ContinueWhenAny(taskList.ToArray(),t =>
        Console.WriteLine("天龙八部就此拉开序幕。。。。")
      );
      var qcTask = qTask.ContinueWith((t,hero) => {
        q.StoryTwo("契丹人");
        q.StoryThree("南院大王");
        q.StoryFour("挂印离开");
      },q).ContinueWith((t) => {
        if(t.AsyncState is Hero) {
          Console.WriteLine(((Hero)t.AsyncState).Name + "已经做好准备啦。。。。");
        } else {
          Console.WriteLine("某某已经做好准备啦。。。。");
        }
      });
      var dcTask = dTask.ContinueWith((t ,hero) => {
        d.StoryTwo("王语嫣");
        d.StoryThree("木婉清");
        d.StoryFour("大理国王");
      },d).ContinueWith((t) => {
        if(t.AsyncState is Hero) {
          Console.WriteLine(((Hero)t.AsyncState).Name + "已经做好准备啦。。。。");
        } else {
          Console.WriteLine("某某已经做好准备啦。。。。");
        }
      });
      var xcTask = xTask.ContinueWith((t ,hero) => {
        x.StoryTwo("逍遥掌门");
        x.StoryThree("灵鹫宫宫主");
        x.StoryFour("西夏驸马");
      },x).ContinueWith((t) => {
        if(t.AsyncState is Hero) {
          Console.WriteLine(((Hero)t.AsyncState).Name + "已经做好准备啦。。。。");
        } else {
          Console.WriteLine("某某已经做好准备啦。。。。");
        }
      });
      cTaskList.Add(qcTask);
      cTaskList.Add(dcTask);
      cTaskList.Add(xcTask);

      Task.WaitAll(cTaskList.ToArray());
      Console.WriteLine("中原群雄大战辽兵，忠义两难一死谢天。。。。");

      sw.Stop();
      Console.WriteLine((sw.ElapsedMilliseconds/1000).ToString());
    }

    public void Example() {
      Action<string> act = DoNothing;
      //异步调用
      AsyncCallback callback = t => {
        Console.WriteLine("-------天龙八部就此拉开序幕。。。。--------");
      };
      IAsyncResult iasyncResult = act.BeginInvoke("string",callback,null);
      //while(!iasyncResult.IsCompleted) {//边等待边操作，可以加进度条
      //  System.Threading.Thread.CurrentThread.Join(1000);
      //  Console.WriteLine("继续");
      //}

      //iasyncResult.AsyncWaitHandle.WaitOne();//等到异步完成
      //iasyncResult.AsyncWaitHandle.WaitOne(2000);//最多等待2000ms，否则继续操作

      this.ThreadCoreLong();
    }

    public void DoNothing(string s) {
      long lResult = 0;
      for(int i = 1;i < 1000000;i++) {
        lResult += i;
      }
      Console.WriteLine("-------DoNothing。。。。--------");
    }

   
    private void DoSomethingLong(string name) {
      Console.WriteLine("-----------DoSomethingLong start threadID:{0},time:{1}-----------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
      long lResult = 0;
      for(int i = 1;i < 1000000;i++) {
        lResult += i;
      }
        Console.WriteLine("-----------DoSomethingLong end threadID:{0},time:{1}-----------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
    }

    /// <summary>
    /// 异步
    /// </summary>
    private void SyncLong() {
      Stopwatch watch = new Stopwatch();
      watch.Start();
      Console.WriteLine("-----------SyncLong start threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
      Action<string> act = this.DoSomethingLong;
      for(int i = 1;i < 5;i++) {
        string name = "click" + i.ToString();
        act.BeginInvoke(name,null,null);
      }
      Console.WriteLine("-----------SyncLong end threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
      watch.Stop();
      Console.WriteLine(watch.ToString());
    }
    /// <summary>
    /// 线程,,住
    /// </summary>
    private void ThreadLong() {
      Stopwatch watch = new Stopwatch();
      watch.Start();
      Console.WriteLine("-----------SyncLong start threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
      
     // System.Threading.ThreadStart 
      for(int i = 1;i < 5;i++) {
        string name = "click" + i.ToString();
        ThreadStart start = ()=>DoSomethingLong(name);
        Thread thread = new Thread(start);
        thread.Start();
      }
      Console.WriteLine("-----------SyncLong end threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
      watch.Stop();
      Console.WriteLine(watch.ToString());
    }
    private void TaskLong() {
      Console.WriteLine("-----------SyncLong start threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
      List<Task> taskList = new List<Task>();
      for(int i = 0;i < 5;i++) {
        string name = "TaskLong_" + i.ToString();
        Task task = Task.Factory.StartNew(() => { 
          this.DoSomethingLong(name); 
          this.DoSomethingLong(name); 
        });

        task.ContinueWith(t=>this.DoNothing("你好"));//任务等待后直接执行

        taskList.Add(task);
      }


      taskList.Add(Task.Factory.ContinueWhenAny(taskList.ToArray(),t => {
        Console.WriteLine(t.IsCompleted);
        Console.WriteLine("ContinueWhenAny " + Thread.CurrentThread.ManagedThreadId);
      }));

      taskList.Add(Task.Factory.ContinueWhenAll(taskList.ToArray(),tList => {
        Console.WriteLine(tList[0].IsCompleted);
        Console.WriteLine("ContinueWhenAll " + Thread.CurrentThread.ManagedThreadId);
      }));

      Console.WriteLine("before WaitAny");
      Task.WaitAny(taskList.ToArray());//当前线程等待某个任务的完成  主线程
      Console.WriteLine("after WaitAny");

      Console.WriteLine("before WaitAll");
      Task.WaitAll(taskList.ToArray());//当前线程等待某个任务的完成  主线程
      Console.WriteLine("after WaitAll");

      Console.WriteLine("-----------SyncLong end threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
    }
    /// <summary>
    /// parallel 会使用主线程干活，会卡界面
    /// 可以指定线程数量
    /// </summary>
    private void ParallelLong() {
      Console.WriteLine("-----------ParallelLong start threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
      //
      //Parallel.Invoke(// == Task + waitAll
      //  () => this.DoSomethingLong("0")
      //  ,() => this.DoSomethingLong("1")
      //  ,() => this.DoSomethingLong("2")
      //  ,() => this.DoSomethingLong("3")
      //  ,() => this.DoSomethingLong("4")
      //  ,() => this.DoSomethingLong("5")
      //  );

      Parallel.For(0,5,t => this.DoSomethingLong(t.ToString()));

      Parallel.ForEach(new int[] { 1,2,3,4,5 },t => this.DoSomethingLong(t.ToString()));

      ParallelOptions option = new ParallelOptions() {  MaxDegreeOfParallelism=3 };
      Parallel.For(0,5,option,(t,state) => {
        //state.Break();//这一次结束
        //return;
        //state.Stop();//整个parallel结束
        this.DoSomethingLong(t.ToString());
      });

      Console.WriteLine("-----------ParallelLong end threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
    }

    /// <summary>
    /// 异常处理、线程取消、多线程的临时变量和线程安全
    /// </summary>
    private void ThreadCoreLong() {
      Console.WriteLine("-----------ThreadCoreLong start threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
     
      try {
        TaskFactory tfactory = new TaskFactory();
        List<Task> taskList = new List<Task>();
        #region 异常处理
        //在线程action加上try catch ，日志记录，不抛异常
        //for(int i = 0;i < 20;i++) {
        //  string name = string.Format("btnClick{0}",i);
        //  Action<object> act = t => {
        //    try {
        //      Thread.Sleep(2000);
        //      if(t.ToString().Equals("btnClick11")) {
        //        throw new Exception(t.ToString() + "执行失败");
        //      }
        //      if(t.ToString().Equals("btnClick13")) {
        //        throw new Exception(t.ToString() + "执行失败");
        //      }
        //      Console.WriteLine(t.ToString() + "执行成功");
        //    } catch(Exception e) {
        //      Console.WriteLine(e.Message);
        //    }
        //  };
        //  taskList.Add(Task.Factory.StartNew(act,name));
        //}
        ////如果不用WaitAll，异常就不会触发，使用后，异常是AggregateException捕获
        //Task.WaitAll(taskList.ToArray());
        #endregion

        #region 线程取消
        //线程间的通信是通过 共有变量：都能访问 局部变量/全局变量/数据库值/硬盘文件
        //线程不能被外部停止，只能自身停止自身；或者在任务启动前停止，会报异常
        //CancellationTokenSource cts = new CancellationTokenSource();
        //for(int i = 0;i < 40;i++) {
        //  string name = string.Format("btnClick{0}",i);
        //  Action<object> act = t => {
        //    try {
        //      //if(cts.IsCancellationRequested) {
        //      //  Console.WriteLine("获取信号量，改变任务执行：{0}",i);
        //      //} 
        //      Thread.Sleep(2000);
        //      if(t.ToString().Equals("btnClick11")) {
        //        throw new Exception(t.ToString() + "执行失败");
        //      }
        //      if(t.ToString().Equals("btnClick13")) {
        //        throw new Exception(t.ToString() + "执行失败");
        //      }
        //      if(cts.IsCancellationRequested) {
        //        Console.WriteLine("获取信号量，改变任务执行：{0}",i);
        //      } else {
        //        Console.WriteLine(t.ToString() + "执行成功");
        //      }
        //    } catch(Exception e) {
        //      cts.Cancel();//表示修改信号量，让大家改变执行策略
        //      Console.WriteLine(e.Message);
        //    }
        //  };
        //  taskList.Add(Task.Factory.StartNew(act,name,cts.Token));//没有启动的任务，在cancel后抛出异常，AggregateException：取消任务
        //}
        //Task.WaitAll(taskList.ToArray());
        #endregion 


        #region 多线程临时变量
        //for(int i = 0;i < 5;i++) {
        //  int k = i;
        //  new Action(() => {
        //    Thread.Sleep(1000);
        //    //Console.WriteLine(i);
        //    Console.WriteLine(k);//每循环一次就声明一次k，是五个k
        //  }).BeginInvoke(null,null);
        //}
        #endregion 

        #region 线程安全 lock

        #endregion

      } catch(AggregateException aex) {
        foreach(var item in aex.InnerExceptions) {
          Console.WriteLine(item.Message);
        }
      } catch(Exception e) {
        Console.WriteLine(e.Message);
      }
    
      Console.WriteLine("-----------ThreadCoreLong end threadID:{0},time:{1}-------------",System.Threading.Thread.CurrentThread.ManagedThreadId,System.DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
      
    }
  }


}
