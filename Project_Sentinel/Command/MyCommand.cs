using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_Sentinel.Command
{
    public class MyCommand : ICommand
    {
        private readonly Action<object> forExecute;
        private readonly Func<object?, bool>? canExecute;

        public event EventHandler? CanExecuteChanged;

        public MyCommand(Action<object> forExecute, Func<object?, bool>? canExecute = null)
        {
            this.forExecute = forExecute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            forExecute(parameter);
        }
    }
}