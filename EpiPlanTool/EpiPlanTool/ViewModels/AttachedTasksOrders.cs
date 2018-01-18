using System;
using System.Linq;
using System.Collections.Generic;
using LightMessageBus;
using EpiPlanTool.Messages;
using System.Collections.ObjectModel;

namespace EpiPlanTool.ViewModels {

  public class AttachedTask {

    private String _key;

    public TaskViewModel Task { get; set; }
    public BookedOrderViewModel Order { get; set; }

    public AttachedTask(TaskViewModel task, BookedOrderViewModel order){
      Task = task;
      Order = order;
      _key = order.OrderID;
    }

    public String Key {
      get { return _key; }
    }
  }

  public class AttachedTasks : ObservableCollection<AttachedTask> { }

  public class AttachedOrderTasks : Dictionary<BookedOrderViewModel, AttachedTasks> {
    public AttachedOrderTasks(int capacity) : base(capacity) { }
  }

}