using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrayKit
{
  internal class ActionCommand : ICommand
  {

    private readonly Action<object> executeHandler;
    private readonly Func<object, bool> canExecuteHandler;

    public ActionCommand(Action<object> execute, Func<object, bool> canExecute)
    {
      if (execute == null)
      {
        throw new ArgumentNullException(nameof(execute));
      }
      executeHandler = execute;
      canExecuteHandler = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      if (canExecuteHandler == null)
      {
        return true;
      }
      return canExecuteHandler(parameter);
    }

    public void Execute(object parameter)
    {
      executeHandler(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }
  }
}
