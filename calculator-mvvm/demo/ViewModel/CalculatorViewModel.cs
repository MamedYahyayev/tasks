using demo.Command;
using demo.Model;
using demo.Service;
using System;
using System.ComponentModel;

namespace demo.ViewModel
{
    public partial class CalculatorViewModel : INotifyPropertyChanged
    {
        private BaseUpdaterCommand? _numCommand;
        private BaseUpdaterCommand? _operationCommand;
        private Calculator _calculator = new Calculator();
        private readonly IDialogService _dialogService;

        private double _firstOperand;
        private double _secondOperand;
        private CalcOperation _lastOperation = CalcOperation.UNSET;
        private bool _isResultCalculated = false;

        public CalculatorViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public double Number
        {
            get { return _calculator.Number; }
            set
            {
                if (_calculator.Number != value)
                {
                    _calculator.Number = value;
                }
            }
        }

        #region Properties For Decimal Number Calculation

        private bool _isDotUsed = false;
        private string _display = string.Empty;

        #endregion

        public string Display
        {
            get { return _display; }
            set
            {
                if (_display != value)
                {
                    _display = value;
                    OnPropertyChange(nameof(Display));
                    Console.WriteLine("I am changed in display setter: " + Display);
                }

            }
        }

        #region Private Properties for Operation Press Style
        private bool _isAddPressed = false;
        private bool _isSubtractPressed = false;
        private bool _isDividePressed = false;
        private bool _isMultiplyPressed = false;
        private bool _isPercentagePressed = false;
        #endregion

        #region Public Properties for Operation Press Style
        public bool IsAddPressed
        {
            get { return _isAddPressed; }
            set
            {
                if (_isAddPressed != value)
                {
                    _isAddPressed = value;
                    OnPropertyChange(nameof(IsAddPressed));
                }
            }
        }

        public bool IsSubtractPressed
        {
            get { return _isSubtractPressed; }
            set
            {
                if (_isSubtractPressed != value)
                {
                    _isSubtractPressed = value;
                    OnPropertyChange(nameof(IsSubtractPressed));
                }
            }
        }

        public bool IsMultiplyPressed
        {
            get { return _isMultiplyPressed; }
            set
            {
                if (_isMultiplyPressed != value)
                {
                    _isMultiplyPressed = value;
                    OnPropertyChange(nameof(IsMultiplyPressed));
                }
            }
        }

        public bool IsDividePressed
        {
            get { return _isDividePressed; }
            set
            {
                if (_isDividePressed != value)
                {
                    _isDividePressed = value;
                    OnPropertyChange(nameof(IsDividePressed));
                }
            }
        }

        public bool IsPercentagePressed
        {
            get { return _isPercentagePressed; }
            set
            {
                if (_isPercentagePressed != value)
                {
                    _isPercentagePressed = value;
                    OnPropertyChange(nameof(IsPercentagePressed));
                }
            }
        }
        #endregion

        #region Property Change Event Handler
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChange(string properyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }
        #endregion

        #region Button Commands
        public BaseUpdaterCommand NumButtonPressCommand
        {
            get
            {
                if (_numCommand == null)
                    _numCommand = new NumUpdater(NumButtonPress, true);
                return _numCommand;
            }
            set { _numCommand = value; }
        }

        public BaseUpdaterCommand OperationBtnPressCommand
        {
            get
            {
                if (_operationCommand == null)
                    _operationCommand = new OperationUpdater(OperationBtnPress, true);
                return _operationCommand;
            }
            set { _operationCommand = value; }
        }
        #endregion

        #region Command Execute Methods
        private void NumButtonPress(string numberStr)
        {

            if (_isResultCalculated)
            {
                Number = 0;
                _lastOperation = _calculator.Operation;
                _isResultCalculated = false;
            }

            int parsedNum = int.Parse(numberStr);
            AssignNumber(parsedNum);

            if (Display.Contains(".") && _isDotUsed)
            {
                var divider = Math.Pow(10, Display.Length - 1); ;
                Display = (Number / divider).ToString();
            }
            else
                Display = Number.ToString();

            
        }

        private void OperationBtnPress(string operationType)
        {
            if (_calculator.Operation == CalcOperation.UNSET && Number != 0)
                _firstOperand = double.Parse(Display);
            else
                if (!_isResultCalculated && Number != 0)
                _secondOperand = double.Parse(Display);


            if (operationType != "+/-" && operationType != "=" && !_isResultCalculated && operationType != ".")
                Calculate();

            UnPressed();
            _isDotUsed = false;
            switch (operationType)
            {
                case "*":
                    _calculator.Operation = CalcOperation.MULTIPLY;
                    IsMultiplyPressed = true;
                    break;
                case "/":
                    _calculator.Operation = CalcOperation.DIVIDE;
                    IsDividePressed = true;
                    break;
                case "+":
                    _calculator.Operation = CalcOperation.ADD;
                    IsAddPressed = true;
                    break;
                case "-":
                    _calculator.Operation = CalcOperation.SUBTRACT;
                    IsSubtractPressed = true;
                    break;
                case "%":
                    _calculator.Operation = CalcOperation.PERCENTAGE;
                    IsPercentagePressed = true;
                    break;
                case "AC": Clear(); break;
                case ".":
                    _isDotUsed = true;
                    Display = Number + ".";
                    break;
                case "+/-":
                    Number *= -1;
                    Display = Number.ToString();
                    break;
                case "=":
                    UnPressed();
                    if (_lastOperation != CalcOperation.UNSET && _calculator.Operation == CalcOperation.UNSET)
                    {
                        Number = _calculator.CalculateResult(_firstOperand, _secondOperand, _lastOperation);
                        Display = Number.ToString();
                        _firstOperand = Number;
                        _calculator.Operation = CalcOperation.UNSET;
                    }
                    else
                        Calculate();
                    break;
            }


        }
        #endregion


        private void AssignNumber(int parsedNum)
        {
            if (Number == 0)
                Number = parsedNum;
            else if (Number < 0)
                Number = (Number * (-10) + parsedNum) * (-1);
            else if (Number > 0)
                Number = Number * 10 + parsedNum;
        }

        private void UnPressed()
        {
            IsAddPressed = false;
            IsSubtractPressed = false;
            IsDividePressed = false;
            IsMultiplyPressed = false;
            IsPercentagePressed = false;
        }

        private void Clear()
        {
            _firstOperand = 0;
            _secondOperand = 0;
            Number = 0;
            _calculator.Operation = CalcOperation.UNSET;
            _lastOperation = 0;
            _isResultCalculated = false;
            Display = Number.ToString();
        }

        private void Calculate()
        {
            try
            {
                Number = 0;
                if (_calculator.Operation != CalcOperation.UNSET)
                {
                    Number = _calculator.CalculateResult(_firstOperand, _secondOperand, _calculator.Operation);
                    Display = Number.ToString();
                    _lastOperation = _calculator.Operation;
                    _firstOperand = Number;
                    _calculator.Operation = CalcOperation.UNSET;
                    _isResultCalculated = true;
                }
            }
            catch (DivideByZeroException e)
            {
                _dialogService.ShowMessageBox(e.Message);
            }
        }
    }


}
