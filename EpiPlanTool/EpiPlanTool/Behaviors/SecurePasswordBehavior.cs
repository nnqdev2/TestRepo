using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Security;
using System.Windows.Data;

namespace EpiPlanTool.Behaviors {

  public class SecurePasswordBehavior : Behavior<PasswordBox> {

    private BindingExpression bindExp;

    protected override void OnAttached() {
      if (this.AssociatedObject != null) {
        base.OnAttached();
        var pBox = this.AssociatedObject as PasswordBox;
        bindExp = BindingOperations.GetBindingExpression(this, BindablePasswordProperty);
        pBox.PasswordChanged += elem_PasswordChanged;
      }
    }

    protected override void OnDetaching() {
      if (this.AssociatedObject != null) {
        var pBox = this.AssociatedObject;
        pBox.PasswordChanged -= elem_PasswordChanged;
        pBox.ClearValue(BindablePasswordProperty);
        BindablePassword = null;
        base.OnDetaching();
      }
    }

    void elem_PasswordChanged(object sender, RoutedEventArgs e) {
      var pBox = sender as PasswordBox;
      BindablePassword = pBox.SecurePassword;
    }

    public static readonly DependencyProperty BindablePasswordProperty =
       DependencyProperty.Register(
       "BindablePassword",
       typeof(SecureString),
       typeof(SecurePasswordBehavior),
       new FrameworkPropertyMetadata(
         (d, e) => { }
       ));

    public SecureString BindablePassword {
      get { return (SecureString)GetValue(BindablePasswordProperty); }
      set { SetValue(BindablePasswordProperty, value); }
    }
  }

}
