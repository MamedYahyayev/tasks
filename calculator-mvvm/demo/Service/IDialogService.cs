using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Service
{
    public interface IDialogService
    {
        void ShowMessageBox(string message);

        void ShowErrorMessageBox(string message, string caption);
    }
}
