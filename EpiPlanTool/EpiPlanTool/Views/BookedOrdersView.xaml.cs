using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;

namespace EpiPlanTool.Views {

  using EpiPlanTool.Utilities;
  using EpiPlanTool.ViewModels;

  public partial class BookedOrdersView : UserControl {

    public BookedOrdersView() {
      InitializeComponent();
      foreach (var col in bookedOrders.Columns) col.IsReadOnly = true;
    }

    private void ContextMenu_PreviewKeyUp(object sender, KeyEventArgs e) {
       if (e.Key == Key.Enter) {
          btnOk.Command.Execute(true);
          ((ContextMenu)sender).IsOpen = false;
       }
    }

    private void MenuItem_PreviewKeyUp(object sender, KeyEventArgs e) {
      if (e.Key == Key.Enter) {
         ((ContextMenu)(((MenuItem)sender).Tag)).IsOpen = false;
      }
    }

     private void Button_btnOk_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
      if (e.ButtonState == MouseButtonState.Released) {
        ((ContextMenu)(((Button)sender).Tag)).IsOpen = false;
      }
    }

    private void bookedOrders_Loaded(object sender, RoutedEventArgs e) {
      var dg = sender as DataGrid;
      foreach(var col in dg.Columns){
        Console.WriteLine("{0}: {1}", col, col.Header);

      }
    }

    private void bookedOrders_InitializingNewItem(object sender, InitializingNewItemEventArgs e) {
      Console.WriteLine("{1}, New Item: {0}", e.NewItem, sender);
    }

  }
}
