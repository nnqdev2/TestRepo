using System;
using System.Windows;
using System.Windows.Threading;
using PropertyChanged;
using Commander;
using EpiPlanTool.Services;

namespace EpiPlanTool.ViewModels
{

    [ImplementPropertyChanged]
    public class MainViewModel
    {

        #region Private Fields
        private readonly AuthenticationService _authService;
        private readonly StatusMessageService _messageService;
        #endregion

        #region Constructors
        public MainViewModel(
          AuthenticationService authService,
          StatusMessageService messageService,
          ScheduleViewModel schedule)
        {
            _authService = authService;
            _messageService = messageService;
            Schedule = schedule;
        }
        #endregion

        #region Public Properties
        public ScheduleViewModel Schedule { get; private set; }
        public AuthenticationService AuthenticationService { get { return _authService; } }
        public StatusMessageService StatusMessageService { get { return _messageService; } }
        #endregion

        #region Command Methods
        [OnCommand("OnEscKeyPressed")]
        private void OnEscKeyPressed()
        {
            Schedule.BookedOrders.FocusedTasks = null;
        }

        [OnCommand("LoadedCommand")]
        private void OnLoadedCommand()
        {
            Schedule.Load();
            AuthenticationService.Login(false);

            Utilities.Util.logger.Info("Function " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." +
                                             System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [OnCommand("LoginCommand")]
        private void Login()
        {
            Utilities.Util.logger.Info("Function " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." +
                                         System.Reflection.MethodBase.GetCurrentMethod().Name);

            if (AuthenticationService.IsLoggedIn)
            {
                Utilities.Util.logger.Info("'if' branch of " + System.Reflection.MethodBase.GetCurrentMethod().Name);
                AuthenticationService.Logout();
            }
            else
            {
                Utilities.Util.logger.Info("'else' branch of " + System.Reflection.MethodBase.GetCurrentMethod().Name);
                AuthenticationService.Login(false);
            }
        }

        [OnCommandCanExecute("PublishCommand")]
        private bool CanPublish() 
        {
            Utilities.Util.logger.Info("Function " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." +
                                        System.Reflection.MethodBase.GetCurrentMethod().Name);

            return AuthenticationService.IsPlanner; 
        }

        [OnCommand("PublishCommand")]
        private void Publish()
        {
            Utilities.Util.logger.Info("Function " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." +
                             System.Reflection.MethodBase.GetCurrentMethod().Name);

            Schedule.Publish();
        }
        #endregion
    }
}
