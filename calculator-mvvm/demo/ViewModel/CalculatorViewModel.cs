using demo.Command;
using demo.Model;
using demo.Service;
using ReactiveUI;
using System;

namespace demo.ViewModel
{
    public class CalculatorViewModel : ReactiveObject
    {
        private readonly Calculator _calculator = new Calculator();
        private readonly IDialogService _dialogService;

        private double _firstOperand;
        private double _secondOperand;
        private CalcOperation _lastOperation = CalcOperation.UNSET;
        private bool _isResultCalculated = false;

        public CalculatorViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        #region Property For Decimal Number Calculation

        private bool _isDotUsed = false;

        #endregion

        #region Public and Private Properties for Display

        private string _display = string.Empty;

        public string Display
        {
            get => _display;
            set => this.RaiseAndSetIfChanged(ref _display, value);
        }

        #endregion

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
            get => this._isAddPressed;
            set => this.RaiseAndSetIfChanged(ref _isAddPressed, value);
        }

        public bool IsSubtractPressed
        {
            get => _isSubtractPressed;
            set => this.RaiseAndSetIfChanged(ref _isSubtractPressed, value);
        }

        public bool IsMultiplyPressed
        {
            get => _isMultiplyPressed;
            set => this.RaiseAndSetIfChanged(ref _isMultiplyPressed, value);
        }

        public bool IsDividePressed
        {
            get => _isDividePressed;
            set => this.RaiseAndSetIfChanged(ref _isDividePressed, value);
        }

        public bool IsPercentagePressed
        {
            get => _isPercentagePressed;
            set => this.RaiseAndSetIfChanged(ref _isPercentagePressed, value);
        }
        #endregion

        #region Command Execute Methods

        private void NumButtonPress(string numberStr)
        {
            if (_isResultCalculated)
            {
                _calculator.Number = 0;
                _lastOperation = _calculator.Operation;
                _isResultCalculated = false;
            }

            int parsedNum = int.Parse(numberStr);
            AssignNumber(parsedNum);

            if (_isDotUsed)
                Display += numberStr;
            else
                Display = _calculator.Number.ToString();
        }

        private void OperationBtnPress(string operationType)
        {
            if (_calculator.Number != 0)
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
                        Display = _calculator.Number + ".";
                        _isDotUsed = true;
                    }
                    break;
                case "+/-":
                    Display = (_calculator.Number *= -1).ToString();
                    break;
                case "=":
                    UnPressed();
                    if (_lastOperation != CalcOperation.UNSET && IsOperationUnset())
                    {
                        _calculator.Number = _calculator.CalculateResult(_firstOperand, _secondOperand, _lastOperation);
                        Display = _calculator.Number.ToString();
                        _firstOperand = _calculator.Number;
                        _calculator.Operation = CalcOperation.UNSET;
                    }
                    else
                        Calculate();
                    break;
            }


        }

        #endregion

        #region Reactive Commands

        private VoidReactiveCommand<string> _numCommand;
        public VoidReactiveCommand<string> NumButtonPressCommand =>
            _numCommand ?? VoidReactiveCommand<string>.Create(NumButtonPress);

        private VoidReactiveCommand<string> _operationCommand;
        public VoidReactiveCommand<string> OperationBtnPressCommand =>
            _operationCommand ?? VoidReactiveCommand<string>.Create(OperationBtnPress);
        
        #endregion

        #region Additional Helper Methods

        private bool IsOperationUnset() => _calculator.Operation == CalcOperation.UNSET;

        private void AssignNumber(int parsedNum)
        {
            if (_calculator.Number == 0)
                _calculator.Number = parsedNum;
            else if (_calculator.Number < 0)
                _calculator.Number = (_calculator.Number * (-10) + parsedNum) * (-1);
            else if (_calculator.Number > 0)
                _calculator.Number = _calculator.Number * 10 + parsedNum;
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
            _calculator.Number = 0;
            _calculator.Operation = CalcOperation.UNSET;
            _lastOperation = CalcOperation.UNSET;
            _isResultCalculated = false;
            Display = _calculator.Number.ToString();
        }

        private void Calculate()
        {
            try
            {
                _calculator.Number = 0;
                if (!IsOperationUnset())
                {
                    _calculator.Number = _calculator.CalculateResult(_firstOperand, _secondOperand, _calculator.Operation);
                    Display = _calculator.Number.ToString();
                    _lastOperation = _calculator.Operation;
                    _firstOperand = _calculator.Number;
                    _calculator.Operation = CalcOperation.UNSET;
                    _isResultCalculated = true;
                }
            }
            catch (DivideByZeroException e)
            {
                _dialogService.ShowErrorMessageBox(e.Message, "Divide By Zero");
            }
        }

        #endregion
    }
}
