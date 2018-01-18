using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EpiPlanTool.Views {

  public partial class TaskEditView : Window {

    private bool _isEditing = true;
    private Binding _IsEditingBinding;

    public TaskEditView() {
      InitializeComponent();
      DataContextChanged += (s, e) => {
        _IsEditingBinding = new Binding {
          Source = DataContext,
          Path = new PropertyPath("IsEditing"),
          Mode = BindingMode.TwoWay
        };
        this.SetBinding(IsEditingProperty, _IsEditingBinding);
      };
    }

    public static DependencyProperty IsEditingProperty =
       DependencyProperty.Register(
       "IsEditing",
       typeof(bool),
       typeof(TaskEditView),
       new FrameworkPropertyMetadata(
         (d, e) => { 
           if((bool)e.NewValue == false) ((Window)d).Close(); 
         }
       ));

    public bool IsEditing {
      get { return _isEditing; }
      set { 
        _isEditing = value;
        
      }
    }

    private void TaskEditor_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
      textbox1.GetBindingExpression(TextBox.TextProperty).UpdateSource();
    }
    
  }

}
