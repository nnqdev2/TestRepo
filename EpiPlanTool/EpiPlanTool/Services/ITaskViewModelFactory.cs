using System;
using Ninject;
using Ninject.Syntax;
using Ninject.Parameters;
using ExpressMapper;
using EpiPlanTool.Models;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Services {
  public interface ITaskViewModelFactory {
    TaskViewModel Create(ReactorViewModel reactor);
    TaskViewModel Create(ReactorViewModel reactor, Task task);
    TaskViewModel Create(ReactorViewModel reactor, BookedOrderViewModel order);
  } 

  public class TaskViewModelFactory : ITaskViewModelFactory {
    readonly IResolutionRoot resolutionRoot;
    readonly IAppRepository repo;
    readonly IBookedOrderLookup lookup;

    static TaskViewModelFactory() {    }

    public TaskViewModelFactory(IResolutionRoot resolutionRoot, IBookedOrderLookup lookup) {
      this.resolutionRoot = resolutionRoot;
      this.repo = this.resolutionRoot.Get<IAppRepository>();
      this.lookup = lookup;
    }

    public TaskViewModel CreateModel(ReactorViewModel reactor) {
      return
        this.resolutionRoot.Get<TaskViewModel>(new ConstructorArgument("reactor", reactor, true));
    }

    TaskViewModel ITaskViewModelFactory.Create(ReactorViewModel reactor) {
      var vm = CreateModel(reactor);
      return vm;
    }

    TaskViewModel ITaskViewModelFactory.Create(ReactorViewModel reactor, Task task) {
      var vm = CreateModel(reactor);
      if (task != null) {
        Mapper.Map<Task, TaskViewModel>(task, vm);
        if (task.TaskType == "O" && task.BookedOrderID.HasValue) 
          vm.AttachedOrder = lookup[task.BookedOrderID.Value];
      }
      return vm;
    }

    TaskViewModel ITaskViewModelFactory.Create(ReactorViewModel reactor, BookedOrderViewModel order) {
      var vm = CreateModel(reactor);
      if (order != null) vm.AttachedOrder = order;
      return vm;
    }

  }
}
