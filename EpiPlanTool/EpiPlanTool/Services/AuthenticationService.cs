using System;
using System.Security;
using System.DirectoryServices.AccountManagement;
using PropertyChanged;

namespace EpiPlanTool.Services {

  using EpiPlanTool.Utilities;
  using EpiPlanTool.Models;

  [ImplementPropertyChanged]
  public class AuthenticationService {

    public AuthenticationService() {
      IsAuthenticated = false;
      IsLoggedIn = false;
      IsPlanner = false;
      IsAdmin = false;
      UserID = Environment.UserName;
    }

    #region Public Properties
    public SecureString Password { get; set; }
    public string UserID { get; set; }
    public bool IsAuthenticated { get; private set; }
    public bool IsLoggedIn { get; private set; }
    public bool IsPlanner { get; private set; }
    public bool IsAdmin { get; private set; }
    public bool CanLogin { get { return IsPlanner | IsAdmin; } }
    #endregion

    #region Private Methods
    private void LoadUser(){
      IsLoggedIn = false;
      AppUserRole user;
      using (var ctx = new EpiPlanTool.Context.PlanContext()) {
        user = ctx.AppUserRoles.Find(UserID.ToLowerInvariant());
      }
      if (user != null) {
        IsPlanner = user.Roles.HasFlag(RoleFlags.Planner);
        IsAdmin = user.Roles.HasFlag(RoleFlags.Admin);
      }
    }
    #endregion

    #region Public Methods
    private void Authenticate() {
      var principalCtx = new PrincipalContext(ContextType.Domain, "WACKER");
      IsAuthenticated = principalCtx.ValidateCredentials(UserID, Password.ConvertToUnsecureString());
      IsLoggedIn = IsAuthenticated;
    }

    public bool Login(bool useCredentials = true) {
      LoadUser();
      if (CanLogin) {
        if (useCredentials) Authenticate();
        else {
          IsLoggedIn = true;
        }
      }
      return IsLoggedIn;
    }

    public void OnIsLoggedInChanged(){
      if (IsLoggedIn == false) {
        IsAdmin = false;
        IsPlanner = false;
        IsAuthenticated = false;
      }
    }

    public void Logout() {
      IsLoggedIn = false;
    }
    #endregion
  }
}
