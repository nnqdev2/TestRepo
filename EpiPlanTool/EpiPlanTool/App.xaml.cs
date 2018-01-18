using System;
using System.Windows;
using System.Windows.Threading;
using EpiPlanTool.Services;
using NLog;

namespace EpiPlanTool {

  public partial class App : Application {

    private static readonly CompositionRoot root = new CompositionRoot();
    private static Logger _logger = LogManager.GetCurrentClassLogger();

    protected override void OnStartup(StartupEventArgs e) {
      base.OnStartup(e);
      AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnAppDomainCurrentDomainUnhandledException);
      MainView _view = root.MainView;
      _view.Show();
    }

    static void OnAppDomainCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs args) {
       var errorMessage = string.Format("AppDomain.CurrentDomain.UnhandledException: {0} {1}", Environment.NewLine, args.ExceptionObject.ToString());
       _logger.Error(errorMessage);
       MessageBox.Show(errorMessage, "Encountered unhandled application error ...", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
       var errorMessage = string.Format("Application.DispatcherUnhandledException: {0} {1}", Environment.NewLine, e.Exception.ToString());
       _logger.Error(errorMessage);
       MessageBox.Show(errorMessage, "Application shutting down due to ...", MessageBoxButton.OK, MessageBoxImage.Error);
       Application.Current.Shutdown();
    }
  }
}
