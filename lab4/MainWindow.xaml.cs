using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTask
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Пункт 1 
        private void BtnPrivit_Click(object sender, RoutedEventArgs e)
        {
            LblStatus.Content = "Привіт";
        }

        private void BtnDoPobachennya_Click(object sender, RoutedEventArgs e)
        {
            LblStatus.Content = "До побачення";
        }

        //Пункт 2 
        private void BtnSkhovaty_Click(object sender, RoutedEventArgs e)
        {
            TxtBlockExample.Visibility = Visibility.Collapsed;
        }

        private void BtnPokazaty_Click(object sender, RoutedEventArgs e)
        {
            TxtBlockExample.Visibility = Visibility.Visible;
        }

        //Пункт 3 
        private void BtnHideTextBox_Click(object sender, RoutedEventArgs e)
        {
            TxtBoxInput.Visibility = Visibility.Collapsed;
        }

        private void BtnShowTextBox_Click(object sender, RoutedEventArgs e)
        {
            TxtBoxInput.Visibility = Visibility.Visible;
        }

        private void BtnClearTextBox_Click(object sender, RoutedEventArgs e)
        {
            TxtBoxInput.Clear();
        }

        //Пункт 4 
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Visibility = Visibility.Collapsed;

            // Ціль гри: приховати всі кнопки
            if (AllGameButtonsHidden())
            {
                MessageBox.Show("Вітаємо! Ви приховали всі кнопки!");
            }
        }

        private bool AllGameButtonsHidden()
        {
            foreach (Button btn in GamePanel.Children)
            {
                if (btn.Visibility == Visibility.Visible)
                    return false;
            }
            return true;
        }

        //Пункт 5 
        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TxtPounds.Text, out double pounds))
            {
                double kg = pounds * 0.45359237;
                TxtKgResult.Text = $"{kg:F2} кг";
            }
            else
            {
                TxtKgResult.Text = "Некоректне число";
            }
        }
    }
}
