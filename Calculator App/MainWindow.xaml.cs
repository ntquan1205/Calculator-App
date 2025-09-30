using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentInput = "0";
        private string currentOperation = "";
        private double firstNumber = 0;
        private bool isNewCalculation = true;
        private bool isOperationJustPressed = false;
        public MainWindow()
        {
            InitializeComponent();
            ResultTextBlock.Text = currentInput;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content?.ToString() ?? "";

            if (currentInput.Length >= 12 && !isNewCalculation && !isOperationJustPressed)
                return;

            if (isNewCalculation || currentInput == "0" || isOperationJustPressed)
            {
                currentInput = digit;
                isNewCalculation = false;
                isOperationJustPressed = false;
            }
            else
            {
                currentInput += digit;
            }

            ResultTextBlock.Text = currentInput;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operation = button.Content?.ToString() ?? "";

            if (!isOperationJustPressed)
            {
                if (!string.IsNullOrEmpty(currentOperation))
                {
                    CalculateResult();
                }
                else
                {
                    firstNumber = double.Parse(currentInput);
                }
            }

            currentOperation = operation;
            isOperationJustPressed = true;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentOperation) && !isOperationJustPressed)
            {
                CalculateResult();
                currentOperation = "";
                isNewCalculation = true;
            }
        }

        private void CalculateResult()
        {
            double secondNumber = double.Parse(currentInput);
            double result = 0;

            switch (currentOperation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "×":
                    result = firstNumber * secondNumber;
                    break;
                case "÷":
                    if (secondNumber != 0)
                        result = firstNumber / secondNumber;
                    else
                        MessageBox.Show("Cannot divide by zero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            if (Math.Abs(result) > 999999999 || Math.Abs(result) < 0.000001 && result != 0)
            {
                currentInput = result.ToString("E6");
            }
            else
            {
                string resultString = result.ToString();
                if (resultString.Contains('.') && resultString.Length > 10)
                {
                    currentInput = Math.Round(result, 8).ToString();
                }
                else
                {
                    currentInput = resultString;
                }
            }

            ResultTextBlock.Text = currentInput;
            firstNumber = result;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            currentOperation = "";
            firstNumber = 0;
            isNewCalculation = true;
            isOperationJustPressed = false;
            ResultTextBlock.Text = currentInput;
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput.Length >= 12 && !isNewCalculation && !isOperationJustPressed)
                return;

            if (isOperationJustPressed)
            {
                currentInput = "0.";
                isOperationJustPressed = false;
            }
            else if (!currentInput.Contains('.'))
            {
                isNewCalculation = false;
                currentInput += ".";
            }

            ResultTextBlock.Text = currentInput;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationJustPressed && currentInput.Length > 0 && !isNewCalculation)
            {
                currentInput = currentInput[..^1];
                if (string.IsNullOrEmpty(currentInput) || currentInput == "-")
                {
                    currentInput = "0";
                }
                ResultTextBlock.Text = currentInput;
            }
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationJustPressed && !isNewCalculation)
            {
                double number = double.Parse(currentInput);
                number /= 100;
                currentInput = number.ToString();
                ResultTextBlock.Text = currentInput;
            }
        }

        private void PlusMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationJustPressed && !isNewCalculation && currentInput != "0")
            {
                currentInput = currentInput.StartsWith('-') ? currentInput[1..] : "-" + currentInput;
                ResultTextBlock.Text = currentInput;
            }
        }

    }
}