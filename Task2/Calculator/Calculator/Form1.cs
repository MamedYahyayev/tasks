using System.Globalization;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private CircularButton activeCircularBtn;
        private Computing computing;
        private Computing.Operation activeOperation;
        public Form1()
        {
            InitializeComponent();
            computing = new Computing();
            activeCircularBtn = new CircularButton();
            activeOperation = Computing.Operation.UNKNOWN;
        }

        private void equalBtn_Click(object sender, EventArgs e)
        {
            revertOperationBtnColor();
            activeCircularBtn = equalBtn;
            changeOperationBtnColor(equalBtn);

            if (inputArea.Text != null)
            {
                computing.Number2 = Convert.ToDouble(inputArea.Text);

                switch (activeOperation)
                {
                    case Computing.Operation.ADD:
                        inputArea.Text = computing.add(computing.Number1, computing.Number2).ToString();
                        break;
                    case Computing.Operation.SUBTRACT:
                        inputArea.Text = computing.subtract(computing.Number1, computing.Number2).ToString();
                        break;
                    case Computing.Operation.MULTIPLY:
                        inputArea.Text = computing.multiply(computing.Number1, computing.Number2).ToString();
                        break;
                    case Computing.Operation.DIVIDE:
                        try
                        {
                            inputArea.Text = computing.divide(computing.Number1, computing.Number2).ToString();
                        }
                        catch (DivideByZeroException)
                        {
                            MessageBox.Show("You cannot divide by zero", "Divide by 0",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case Computing.Operation.PERCENTAGE:
                        inputArea.Text = computing.percentage(computing.Number1, computing.Number2).ToString();
                        break;
                    case Computing.Operation.UNKNOWN:
                        MessageBox.Show("Choose one of the operations", "Operation",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        Console.WriteLine("Operation doesn't exist");
                        break;
                }

                revertOperationBtnColor();
                activeOperation = Computing.Operation.UNKNOWN;
            }

        }

        private void plusBtn_Click(object sender, EventArgs e)
        {
            revertOperationBtnColor();
            activeCircularBtn = plusBtn;
            changeOperationBtnColor(plusBtn);

            assignNumAndOperation(Computing.Operation.ADD);

        }


        private void minusBtn_Click(object sender, EventArgs e)
        {
            revertOperationBtnColor();
            activeCircularBtn = minusBtn;
            changeOperationBtnColor(minusBtn);

            assignNumAndOperation(Computing.Operation.SUBTRACT);

        }


        private void multiplyBtn_Click(object sender, EventArgs e)
        {
            revertOperationBtnColor();
            activeCircularBtn = multiplyBtn;
            changeOperationBtnColor(multiplyBtn);

            assignNumAndOperation(Computing.Operation.MULTIPLY);

        }

        private void divideBtn_Click(object sender, EventArgs e)
        {
            revertOperationBtnColor();
            activeCircularBtn = divideBtn;
            changeOperationBtnColor(divideBtn);

            assignNumAndOperation(Computing.Operation.DIVIDE);
        }

        private void percentageBtn_Click(object sender, EventArgs e)
        {
            assignNumAndOperation(Computing.Operation.PERCENTAGE);
        }


        private void changeOperationBtnColor(CircularButton circularButton)
        {
            circularButton.BackColor = Color.White;
            circularButton.ForeColor = Color.FromArgb(254, 149, 4);
        }

        private void revertOperationBtnColor()
        {
            activeCircularBtn.BackColor = Color.FromArgb(254, 149, 4);
            activeCircularBtn.ForeColor = Color.White;
        }

        private void number7Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number7Btn.Text);
        }

        private void number8Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number8Btn.Text);
        }

        private void number9Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number9Btn.Text);
        }

        private void number4Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number4Btn.Text);
        }

        private void number5Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number5Btn.Text);
        }

        private void number6Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number6Btn.Text);
        }

        private void number1Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number1Btn.Text);
        }

        private void number2Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number2Btn.Text);

        }

        private void number3Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number3Btn.Text);

        }

        private void number0Btn_Click(object sender, EventArgs e)
        {
            typeNumber(number0Btn.Text);
        }

        private void commaBtn_Click(object sender, EventArgs e)
        {
            if (!isExceedCharCount()) inputArea.Text += commaBtn.Text;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            inputArea.Text = "";
        }

        private void plusMinusBtn_Click(object sender, EventArgs e)
        {
            if (inputArea.Text != "" && inputArea.Text != null)
            {
                string num = inputArea.Text;
                int hasMinus = num.IndexOf("-");
                if (hasMinus == -1) inputArea.Text = "-" + inputArea.Text;
                else inputArea.Text = num.Replace("-", "");
            }
        }

        private bool isExceedCharCount()
        {
            var length = inputArea.Text.Length;
            if (length >= 10)
            {
                MessageBox.Show("Max Character", "Character Exceed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private void assignNumAndOperation(Computing.Operation operation)
        {
            if (inputArea.Text != null && inputArea.Text != "")
            {
                computing.Number1 = Convert.ToDouble(inputArea.Text);
                activeOperation = operation;
                inputArea.Text = "";
            }
        }

        private void typeNumber(string number)
        {
            if (!isExceedCharCount())
            {
                if (inputArea.Text.Length == 1 && inputArea.Text.StartsWith("0")) inputArea.Text = number;
                else inputArea.Text += number;

            }
        }

    }
}