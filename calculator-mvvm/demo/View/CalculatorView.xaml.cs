using demo.Service;
using demo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo.View
{
    public partial class CalculatorView : Window
    {
        /*private readonly CalculatorViewModel calculatorViewModel;*/
        private readonly CalculatorViewModel _calculatorViewModelReactive;

        public CalculatorView()
        {
            InitializeComponent();
            _calculatorViewModelReactive = new CalculatorViewModel(new DialogService());
            DataContext = _calculatorViewModelReactive;
        }

    }
}

