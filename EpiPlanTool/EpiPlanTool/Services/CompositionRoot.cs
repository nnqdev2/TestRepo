using System;
using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Factory;
using ExpressMapper;
using EpiPlanTool.Services;
using EpiPlanTool.ViewModels;
using EpiPlanTool.Models;

namespace EpiPlanTool {

  public class CompositionRoot {

    private class CompositionRootModule : NinjectModule {
      public override void Load() {
        /* Factories */
        Bind<IBookedOrderViewModelFactory>().To<BookedOrderViewModelFactory>().InSingletonScope();
        Bind<IBookedOrderLookup>().To<BookedOrderViewModelFactory>().InSingletonScope();
        Bind<IReactorViewModelFactory>().To<ReactorViewModelFactory>().InSingletonScope();
        Bind<ITaskViewModelFactory>().To<TaskViewModelFactory>().InSingletonScope(); ;

        /* Services */
        Bind<AuthenticationService>().ToSelf().InSingletonScope();
        Bind<StatusMessageService>().ToSelf().InSingletonScope();
        Bind<IAppRepository>().To<AppRepository>().InSingletonScope();

        /* View Models */
        Bind<LoginViewModel>().ToSelf();
        Bind<MainViewModel>().ToSelf();
        Bind<ScheduleViewModel>().ToSelf();
        Bind<ReactorViewModel>().ToSelf();
        Bind<BookedOrdersViewModel>().ToSelf();
        Bind<BookedOrderViewModel>().ToSelf();
      }
    }

    private IKernel Kernel;

    public CompositionRoot() {
      /* Mapper Registration */
      Mapper.Register<BookedOrder, BookedOrderViewModel>();
      Mapper.Register<TaskViewModel, Task>();
      Mapper.Register<Task, TaskViewModel>();
      Mapper.Register<TaskViewModel, TaskViewModel>();
      Mapper.Compile();
      /* Ninject Setup */
      var settings = new NinjectSettings {
        InjectNonPublic = true,
        UseReflectionBasedInjection = false
      };
      Kernel = new StandardKernel(settings, new CompositionRootModule());
    }
    public MainView MainView { get { return Kernel.Get<MainView>(); }}
    public IAppRepository Repository { get { return Kernel.Get<IAppRepository>(); }}
  }

}
