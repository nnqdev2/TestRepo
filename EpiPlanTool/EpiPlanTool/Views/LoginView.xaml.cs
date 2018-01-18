using System;
using System.Windows;
using System.Windows.Controls;
using Ninject;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Views {
  public partial class LoginView : Window {

    public LoginView() {
      InitializeComponent();
    }

    [Inject]
    public LoginView(LoginViewModel vm) : this() {
      Console.WriteLine("LoginView()");
      this.DataContext = vm;
      pBox.Focus();
    }

    private void Cancel(object sender, RoutedEventArgs e) {
      var btn = sender as Button;
      if (btn.IsCancel) this.Close();
    }

  }

}


