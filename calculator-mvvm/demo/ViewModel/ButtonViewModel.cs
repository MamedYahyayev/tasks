using demo.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.ViewModel
{

    public partial class CalculatorViewModel
    {
        public class ButtonViewModel : INotifyPropertyChanged
        {
            private BaseUpdaterCommand _buttonBgColorUpdaterCommand;
            private bool _isPressed = false;

            public bool IsPressed
            {
                get { return _isPressed; }
                set
                {
                    if (_isPressed != value)
                    {
                        _isPressed = value;
                        Console.WriteLine("i am here on the button view model set method" + _isPressed);
                        OnPropertyChanged(nameof(IsPressed));
                    }
                }
            }


            public event PropertyChangedEventHandler? PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
}
