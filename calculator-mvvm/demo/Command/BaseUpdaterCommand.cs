using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace demo.Command
{
    public class BaseUpdaterCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action<string> _executeMethod;
        private bool _canExecute;

        public BaseUpdaterCommand(Action<string> executeMethod, bool canExecute)
        {
            _executeMethod = executeMethod;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute;
        }

        public void Execute(object? parameter)
        {
            _executeMethod(parameter.ToString());
        }
    }
}

