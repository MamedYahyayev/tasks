using demo.Command;
using System;

namespace demo.ViewModel
{
    public class NumUpdater : BaseUpdaterCommand
    {
        public NumUpdater(Action<string> executeMethod, bool canExecute)
            : base(executeMethod, canExecute)
        {
        }

    }
}
