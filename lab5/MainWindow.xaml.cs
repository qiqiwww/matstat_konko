using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WpfCalculator
{
    public partial class MainWindow : Window
    {
        private decimal currentNumber = 0;
        private decimal previousNumber = 0;
        private string operation = "";
        private bool isNewNumber = true;
        private bool hasDecimal = false;
        private CultureInfo culture = CultureInfo.InvariantCulture;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обробник для цифрових кнопок
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Content.ToString();

            if (DisplayCurrent.Text == "0" || isNewNumber)
            {
                DisplayCurrent.Text = number;
                isNewNumber = false;
                hasDecimal = false;
            }
            else
            {
                // Перевірка на максимальну довжину числа
                if (DisplayCurrent.Text.Length < 15)
                {
                    DisplayCurrent.Text += number;
                }
            }
        }

        // Обробник для десяткової крапки
        private void BtnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (!hasDecimal)
            {
                if (isNewNumber)
                {
                    DisplayCurrent.Text = "0.";
                    isNewNumber = false;
                }
                else
                {
                    DisplayCurrent.Text += ".";
                }
                hasDecimal = true;
            }
        }

        // Обробник для арифметичних операцій
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (!isNewNumber && operation != "")
            {
                CalculateResult();
            }

            if (decimal.TryParse(DisplayCurrent.Text, NumberStyles.Any, culture, out decimal number))
            {
                previousNumber = number;
                operation = button.Content.ToString();
                DisplayOperation.Text = $"{FormatNumber(previousNumber)} {operation}";
                isNewNumber = true;
                hasDecimal = false;
            }
        }

        // Обробник для кнопки "="
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (!isNewNumber && operation != "")
            {
                CalculateResult();
            }
        }

        // Обробник для кнопки "CE" (Очистити поточне число)
        private void BtnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            DisplayCurrent.Text = "0";
            isNewNumber = true;
            hasDecimal = false;
        }

        // Обробник для кнопки "C" (Повне очищення)
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DisplayCurrent.Text = "0";
            DisplayOperation.Text = "";
            previousNumber = 0;
            currentNumber = 0;
            operation = "";
            isNewNumber = true;
            hasDecimal = false;
        }

        // Обробник для кнопки (Видалити останній символ)
        private void BtnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (!isNewNumber && DisplayCurrent.Text.Length > 1)
            {
                string currentText = DisplayCurrent.Text;

                if (currentText.EndsWith("."))
                {
                    hasDecimal = false;
                }

                string newText = currentText.Substring(0, currentText.Length - 1);

                // Якщо після видалення залишилась лише мінус, або текст порожній
                if (newText == "-" || string.IsNullOrEmpty(newText))
                {
                    DisplayCurrent.Text = "0";
                    isNewNumber = true;
                }
                else
                {
                    DisplayCurrent.Text = newText;
                }
            }
            else
            {
                DisplayCurrent.Text = "0";
                isNewNumber = true;
                hasDecimal = false;
            }
        }
       
        

        // Основна функція для обчислення результатів
        private void CalculateResult()
        {
            if (decimal.TryParse(DisplayCurrent.Text, NumberStyles.Any, culture, out currentNumber))
            {
                decimal result = 0;
                bool error = false;

                try
                {
                    switch (operation)
                    {
                        case "+":
                            result = previousNumber + currentNumber;
                            break;
                        case "-":
                            result = previousNumber - currentNumber;
                            break;
                        case "×":
                            result = previousNumber * currentNumber;
                            break;
                        case "/":
                            if (currentNumber == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            result = previousNumber / currentNumber;
                            break;
                    }

                    // Перевірка на переповнення
                    if (result > decimal.MaxValue || result < decimal.MinValue)
                    {
                        throw new OverflowException();
                    }

                    DisplayCurrent.Text = FormatNumber(result);
                    DisplayOperation.Text = $"{FormatNumber(previousNumber)} {operation} {FormatNumber(currentNumber)} =";
                    operation = "";
                    isNewNumber = true;
                    hasDecimal = result != Math.Floor(result);
                }
                catch (DivideByZeroException)
                {
                    DisplayCurrent.Text = "Ділення на 0";
                    error = true;
                }
                catch (OverflowException)
                {
                    DisplayCurrent.Text = "Переповнення";
                    error = true;
                }

                if (error)
                {
                    operation = "";
                    isNewNumber = true;
                }
            }
        }

        // Метод для форматування чисел
        private string FormatNumber(decimal number)
        {
            // Для цілих чисел відображати без десяткової частини
            if (number == Math.Floor(number) && number <= 999999999999999 && number >= -999999999999999)
            {
                return number.ToString("F0", culture);
            }

            // Для десяткових чисел - обмежити кількість знаків після коми
            string formatted = number.ToString("G15", culture);

            // Видалення зайвих нулів в кінці
            if (formatted.Contains("."))
            {
                formatted = formatted.TrimEnd('0').TrimEnd('.');
            }

            return formatted;
        }
    }
}