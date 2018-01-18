using System;
using System.Windows;
using System.Windows.Threading;
using EpiPlanTool.Services;

namespace EpiPlanTool.ViewModels {

  public static class PropertyChangedNotificationInterceptor {

    public static void Intercept(object target, Action onPropertyChangedAction, string propertyName) {
      if(onPropertyChangedAction != null) onPropertyChangedAction();
      if (target is StatusMessageService) {
        Action callBack = () => {
          var m = ((StatusMessageService)target).Message;
        };
        Application.Current.Dispatcher.Invoke(
          callBack,
          DispatcherPriority.ApplicationIdle
        );
      }
    }
  }

}
