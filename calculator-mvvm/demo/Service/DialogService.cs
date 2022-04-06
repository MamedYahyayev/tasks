using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace demo.Service
{
    public class DialogService : IDialogService
    {
        public void ShowErrorMessageBox(string message, string caption) 
            => MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);

        public void ShowMessageBox(string message) => MessageBox.Show(message);
    }
}
