using System;
using System.Windows;
using System.Windows.Threading;
using PropertyChanged;
using Commander;
using EpiPlanTool.Services;
using System.Reflection;

namespace EpiPlanTool.ViewModels {

  [ImplementPropertyChanged]
  public class MainViewModel {

    #region Private Fields
    private readonly AuthenticationService _authService;
    private readonly StatusMessageService _messageService;
    private readonly string _appbuildnumber;
    #endregion

    #region Constructors
    public MainViewModel(
      AuthenticationService authService,
      StatusMessageService messageService,
      ScheduleViewModel schedule) {
      _authService = authService;
      _messageService = messageService;
      Schedule = schedule;
      _appbuildnumber = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
    #endregion

    #region Public Properties
    public ScheduleViewModel Schedule { get; private set; }
    public AuthenticationService AuthenticationService { get { return _authService; } }
    public StatusMessageService StatusMessageService { get { return _messageService; } }
    public string AppBuildNumber { get { return _appbuildnumber; } }
    #endregion

    #region Command Methods
    [OnCommand("OnEscKeyPressed")]
    private void OnEscKeyPressed() {
      Schedule.BookedOrders.FocusedTasks = null;
    }

    [OnCommand("LoadedCommand")]
    private void OnLoadedCommand() {
      Schedule.Load();
      AuthenticationService.Login(false);
    }

    [OnCommand("LoginCommand")]
    private void Login() {
      if (AuthenticationService.IsLoggedIn) {
        AuthenticationService.Logout();
      }
      else {
        AuthenticationService.Login(false);
      }
    }

    [OnCommandCanExecute("PublishCommand")]
    private bool CanPublish() { return AuthenticationService.IsPlanner; }

    [OnCommand("PublishCommand")]
    private void Publish() {
      Schedule.Publish();
    }
    #endregion
  }
}
