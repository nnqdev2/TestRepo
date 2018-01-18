using System;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using EpiPlanTool.Models;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Services {

  public interface IReactorViewModelFactory {
    ReactorViewModel Create(ReactorSchedule reactorSchedule);
  }

  public class ReactorViewModelFactory : IReactorViewModelFactory {
    readonly IResolutionRoot resolutionRoot;

    public ReactorViewModelFactory(IResolutionRoot resolutionRoot) {
      this.resolutionRoot = resolutionRoot;
    }

    ReactorViewModel IReactorViewModelFactory.Create(ReactorSchedule reactorSchedule) {
      var vm = this.resolutionRoot.Get<ReactorViewModel>(
        new IParameter[] {
          new ConstructorArgument("id", reactorSchedule.ReactorScheduleID,true),
          new ConstructorArgument("reactorId", reactorSchedule.ReactorID, true),
          new ConstructorArgument("caption", reactorSchedule.Reactor.Caption,true),
          new ConstructorArgument("reactType", reactorSchedule.Reactor.ReactType,true),
          new ConstructorArgument("reactNumber", reactorSchedule.Reactor.ReactorNumber,true)
        }
      );
      return vm;    
    }

  }

}
