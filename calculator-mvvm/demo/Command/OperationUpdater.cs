using demo.Command;
using System;

namespace demo.ViewModel
{
    public class OperationUpdater : BaseUpdaterCommand
    {
        public OperationUpdater(Action<string> executeMethod, bool canExecute)
            : base(executeMethod, canExecute)
        {
        }
    }
}