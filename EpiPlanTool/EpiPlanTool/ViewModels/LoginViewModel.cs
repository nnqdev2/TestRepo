using System;
using System.Security;
using MvvmFoundation.Wpf;
using Ninject;
using EpiPlanTool.Services;

namespace EpiPlanTool.ViewModels {

  public class LoginViewModel : ObservableObject {

    private AuthenticationService _authService;

    [Inject]
    public LoginViewModel(AuthenticationService service) {
      _authService = service;
    }

    private bool CanExecuteLogin() {
      return (UserID != null && UserID.Length > 0)
        && (BindablePassword != null && BindablePassword.Length > 3)
        && !IsAuthenticated;
    }

    public SecureString BindablePassword {
      get { return _authService.Password; }
    }

    public string UserID {
      get { return _authService.UserID;  }
    }

    public bool IsAuthenticated { 
      get { return _authService.IsAuthenticated; }
    }

    public bool LoginError {
      get { return false; }
    }

//    public ICommand LoginCommand { get { return _loginCommand; } }

  }
}
