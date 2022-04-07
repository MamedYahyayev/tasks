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
                    Console.WriteLine("I am here");
                    OnPropertyChange(nameof(Display));
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

            if (_isDotUsed)
                Display += numberStr;
            else
                Display = Number.ToString();
        }

        private void OperationBtnPress(string operationType)
        {
            if (Number != 0)
            {
                if (IsOperationUnset())
                    _firstOperand = double.Parse(Display);
                else
                    _secondOperand = double.Parse(Display);
            }

            if (operationType != "+/-" && operationType != "=" && !_isResultCalculated && operationType != "." 
                    && operationType != "AC")
                Calculate();

            UnPressed();

            if (operationType != ".")
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
                    if (!_isDotUsed)
                    {
                        Display = Number + ".";
                        _isDotUsed = true;
                    }
                    break;
                case "+/-":
                    Number *= -1;
                    Display = Number.ToString();
                    break;
                case "=":
                    UnPressed();
                    if (_lastOperation != CalcOperation.UNSET && IsOperationUnset())
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


        private bool IsOperationUnset() => _calculator.Operation == CalcOperation.UNSET;

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
            _lastOperation = CalcOperation.UNSET;
            _isResultCalculated = false;
            Display = Number.ToString();
        }

        private void Calculate()
        {
            try
            {
                Number = 0;
                if (!IsOperationUnset())
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
                _dialogService.ShowErrorMessageBox(e.Message, "Divide By Zero");
            }
        }
    }


}
