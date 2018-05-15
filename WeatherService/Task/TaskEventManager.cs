using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace WeatherService.Task {


  /// <summary>
  /// 处理一个事件列表
  /// </summary>
  /// <param name="eventList"></param>
  /// <returns></returns>
  public delegate void TaskEventProcessFunc(IEnumerable<TaskEvent> eventList);

  public class TaskEvent {
    public Type taskType;
    public string eventName;
    public object data;
  }
  /// <summary>
  /// 用于管理每个任务触发的事件
  /// </summary>
  public class TaskEventManager {
    public TaskEventManager() {
      eventHandles = new List<Tuple<Type, TaskEventProcessFunc>>();
      eventList = new List<TaskEvent>();
    }
    protected IList<Tuple<Type, TaskEventProcessFunc>> eventHandles;
    protected IList<TaskEvent> eventList;

    /// <summary>
    /// 出发一个延迟加载的事件
    /// </summary>
    /// <param name="taskType"></param>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    public void OnLazyEvent(Type taskType, string eventName, object data) {
      lock (this) {
        eventList.Add(
          new TaskEvent() {
            taskType = taskType,
            eventName = eventName,
            data = data,
          });
      }
    }

    /// <summary>
    /// 触发一个事件
    /// </summary>
    /// <param name="taskType"></param>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    public void OnEvent(Type taskType, string eventName, object data) {
      lock (this) {
        var list = new List<TaskEvent>(){
        new TaskEvent(){
          taskType = taskType,
          eventName = eventName,
          data = data,
        }
      };

        foreach (var h in eventHandles.Where(i => i.Item1 == taskType)) {
          h.Item2(list);
        }
      }
    }

    /// <summary>
    /// 注册一个事件处理句柄
    /// </summary>
    /// <param name="taskType"></param>
    /// <param name="func"></param>
    public void RegisterEventHandle(Type taskType, TaskEventProcessFunc func, bool replace = true) {
      lock (this) {
        if (replace) {
          var old = eventHandles.FirstOrDefault(i => i.Item1 == taskType);
          if (null != old) {
            eventHandles.Remove(old);
          }
        }
        eventHandles.Add(Tuple.Create(taskType, func));
      }
    }

    /// <summary>
    /// 处理所有的LazyEvent
    /// </summary>
    public void ProcessLazyEvent() {
      lock (this) {
        while (this.eventList.Count > 0) {

          var lookup = eventList.ToLookup(i => i.taskType);
          eventList.Clear();

          foreach (var h in eventHandles) {
            if (lookup.Contains(h.Item1)) {
              h.Item2(lookup[h.Item1]);
            }
          }

        }
      }
    }
  }
}
