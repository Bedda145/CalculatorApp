using System.Globalization;

namespace CalculatorApp2
{
    public partial class MainPage : ContentPage
    {
        double storedValue = 0;
        string currentOperator = string.Empty;
        bool isNewEntry = true;

        public MainPage()
        {
            InitializeComponent();
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            Button pressed = sender as Button;
            string input = pressed.Text;

            switch (input)
            {
                case "C":
                    ClearAll();
                    break;
                case "⌫":
                    Backspace();
                    break;
                case "+":
                case "-":
                case "x":
                case "÷":
                    ProcessOperator(input);
                    break;
                case "=":
                    Calculate();
                    break;
                case "±":
                    ToggleSign();
                    break;
                case "%":
                    Percentage();
                    break;
                default:
                    ProcessDigit(input);
                    break;
            }
        }

        void ProcessDigit(string digit)
        {
            if (isNewEntry || DisplayEntry.Text == "0")
            {
                DisplayEntry.Text = digit;
                isNewEntry = false;
            }
            else
            {
                DisplayEntry.Text += digit;
            }
        }

        void ProcessOperator(string op)
        {
            try
            {
                double currentValue = double.Parse(DisplayEntry.Text, CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(currentOperator))
                {
                    storedValue = PerformCalculation(storedValue, currentValue, currentOperator);
                    DisplayEntry.Text = storedValue.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    storedValue = currentValue;
                }
                currentOperator = op;
                isNewEntry = true;
            }
            catch (Exception)
            {
                DisplayEntry.Text = "Error";
            }
        }

        void Calculate()
        {
            try
            {
                if (string.IsNullOrEmpty(currentOperator))
                    return;

                double currentValue = double.Parse(DisplayEntry.Text, CultureInfo.InvariantCulture);
                double result = PerformCalculation(storedValue, currentValue, currentOperator);

                DisplayEntry.Text = result.ToString(CultureInfo.InvariantCulture);
                storedValue = result;
                currentOperator = string.Empty;
                isNewEntry = true;
            }
            catch (Exception)
            {
                DisplayEntry.Text = "Error";
            }
        }

        double PerformCalculation(double a, double b, string op)
        {
            if (op == "+")
                return a + b;
            if (op == "-")
                return a - b;
            if (op == "x")
                return a * b;
            if (op == "÷")
                return a / b;
            return b;
        }

        void ClearAll()
        {
            DisplayEntry.Text = "0";
            storedValue = 0;
            currentOperator = string.Empty;
            isNewEntry = true;
        }

        void Backspace()
        {
            if (!isNewEntry && DisplayEntry.Text.Length > 0)
            {
                DisplayEntry.Text = DisplayEntry.Text.Substring(0, DisplayEntry.Text.Length - 1);
                if (DisplayEntry.Text == string.Empty)
                {
                    DisplayEntry.Text = "0";
                    isNewEntry = true;
                }
            }
        }

        void ToggleSign()
        {
            try
            {
                double currentValue = double.Parse(DisplayEntry.Text, CultureInfo.InvariantCulture);
                currentValue = -currentValue;
                DisplayEntry.Text = currentValue.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
               
            }
        }

        void Percentage()
        {
            try
            {
                double currentValue = double.Parse(DisplayEntry.Text, CultureInfo.InvariantCulture);
                currentValue /= 100;
                DisplayEntry.Text = currentValue.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
