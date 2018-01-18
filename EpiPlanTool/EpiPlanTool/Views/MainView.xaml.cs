using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Documents;
using Excel = Microsoft.Office.Interop.Excel;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;

/*
 *  To get version:
 *  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
 */

namespace EpiPlanTool {

  using EpiPlanTool.ViewModels;

  public partial class MainView : Window {

    public MainView(MainViewModel vm) {
      InitializeComponent();
      DataContext = vm;
    }

    private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e) {
      var hyperlink = sender as Hyperlink;
      Process.Start(hyperlink.NavigateUri.ToString());
      e.Handled = true;
    }

    private void File_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e) {
      Excel.Application xlApp = new Excel.Application();
      Excel.Workbook workbook = xlApp.Workbooks.Open(e.Uri.LocalPath);
      xlApp.Visible = true; 
    }

  }

}

/*
if (e.Key == Key.Escape) 
if (e.Key == Key.F5) 
*/
