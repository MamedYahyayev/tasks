using demo.Command;
using System;

namespace demo.ViewModel
{
    public class ButtonBgColorUpdater : BaseUpdaterCommand
    {
        public ButtonBgColorUpdater(Action<string> executeMethod, bool canExecute) : base(executeMethod, canExecute)
        {
        }
    }
}
