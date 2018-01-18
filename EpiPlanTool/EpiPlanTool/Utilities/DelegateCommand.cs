using System;
using System.Windows.Input;

namespace EpiPlanTool.Utilities {

  public class DelegateCommand : ICommand {
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) {
      throw new NotImplementedException();
    }

    public void Execute(object parameter) {
      throw new NotImplementedException();
    }

    protected virtual void OnCanExecuteChanged() {
      EventHandler handler = CanExecuteChanged;
      if (handler != null) handler(this, EventArgs.Empty);
    }
  }
}

