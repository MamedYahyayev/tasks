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

namespace demo
{
    public partial class MainWindow : Window
    {
        private readonly CalculatorViewModel calculatorViewModel;
        public MainWindow()
        {
            InitializeComponent();
            calculatorViewModel = new CalculatorViewModel(new DialogService());
            DataContext = calculatorViewModel;
        }
    }
}

