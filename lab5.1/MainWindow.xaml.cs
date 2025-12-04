using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace BestOil
{
    public partial class MainWindow : Window
    {
        private decimal dailyRevenue = 0;
        private CultureInfo culture = new CultureInfo("uk-UA");

        // Ціни на пальне
        private readonly decimal[] fuelPrices = { 6.40m, 6.20m, 5.80m, 4.50m };

        // Ціни на продукти кафе
        private readonly decimal[] cafePrices = { 4.00m, 5.40m, 7.20m, 4.40m };

        public MainWindow()
        {
            InitializeComponent();
            InitializePrices();
        }

        private void InitializePrices()
        {
            // Встановлюємо початкові ціни
            UpdateFuelPrice();

            // Ініціалізуємо початкові значення
            TxtLiters.Text = "0";
            TxtMoney.Text = "0";
            TxtFuelResult.Text = "0,00";
        }

        private void CmbFuelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbFuelType == null) return;

            UpdateFuelPrice();
            CalculateFuel();
        }

        private void UpdateFuelPrice()
        {
            if (CmbFuelType?.SelectedIndex == null || TxtFuelPrice == null) return;

            int selectedIndex = CmbFuelType.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < fuelPrices.Length)
            {
                TxtFuelPrice.Text = fuelPrices[selectedIndex].ToString("F2", culture) + " грн.";
            }
        }

        private void RbByLiters_Checked(object sender, RoutedEventArgs e)
        {
            if (TxtLiters == null || TxtMoney == null || TxtFuelResultTitle == null || TxtFuelUnit == null) return;

            TxtLiters.IsEnabled = true;
            TxtMoney.IsEnabled = false;
            TxtMoney.Text = "0";
            TxtFuelResultTitle.Text = "До оплати:";
            TxtFuelUnit.Text = "грн.";
            CalculateFuel();
        }

        private void RbByMoney_Checked(object sender, RoutedEventArgs e)
        {
            if (TxtLiters == null || TxtMoney == null || TxtFuelResultTitle == null || TxtFuelUnit == null) return;

            TxtLiters.IsEnabled = false;
            TxtMoney.IsEnabled = true;
            TxtLiters.Text = "0";
            TxtFuelResultTitle.Text = "До видачі:";
            TxtFuelUnit.Text = "л.";
            CalculateFuel();
        }

        private void TxtLiters_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateFuel();
        }

        private void TxtMoney_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateFuel();
        }

        private void CalculateFuel()
        {
            try
            {
                if (CmbFuelType?.SelectedIndex == null || TxtFuelResult == null) return;

                int fuelIndex = CmbFuelType.SelectedIndex;
                if (fuelIndex < 0) return;

                decimal fuelPrice = fuelPrices[fuelIndex];

                if (RbByLiters?.IsChecked == true && TxtLiters != null)
                {
                    if (decimal.TryParse(TxtLiters.Text, out decimal liters))
                    {
                        decimal total = liters * fuelPrice;
                        TxtFuelResult.Text = total.ToString("F2", culture);
                    }
                }
                else if (RbByMoney?.IsChecked == true && TxtMoney != null)
                {
                    if (decimal.TryParse(TxtMoney.Text, out decimal money))
                    {
                        if (fuelPrice > 0)
                        {
                            decimal liters = money / fuelPrice;
                            TxtFuelResult.Text = liters.ToString("F2", culture);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка розрахунку: {ex.Message}");
                TxtFuelResult.Text = "0,00";
            }
        }

        private void CafeItem_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox checkBox)) return;

            TextBox quantityBox = GetQuantityTextBox(checkBox);

            if (quantityBox != null)
            {
                quantityBox.IsEnabled = checkBox.IsChecked == true;
                if (checkBox.IsChecked != true)
                {
                    quantityBox.Text = "0";
                }
            }

            CalculateCafe();
        }

        private TextBox GetQuantityTextBox(CheckBox checkBox)
        {
            if (checkBox == null) return null;

            if (checkBox == CbHotDog) return TxtHotDogQty;
            if (checkBox == CbHamburger) return TxtHamburgerQty;
            if (checkBox == CbFries) return TxtFriesQty;
            if (checkBox == CbCola) return TxtColaQty;
            return null;
        }

        private void CafeQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateCafe();
        }

        private void CalculateCafe()
        {
            try
            {
                if (TxtCafeTotal == null) return;

                decimal total = 0;

                // Хот-дог
                if (CbHotDog?.IsChecked == true && TxtHotDogQty != null &&
                    decimal.TryParse(TxtHotDogQty.Text, out decimal hotDogQty))
                {
                    total += hotDogQty * cafePrices[0];
                }

                // Гамбургер
                if (CbHamburger?.IsChecked == true && TxtHamburgerQty != null &&
                    decimal.TryParse(TxtHamburgerQty.Text, out decimal hamburgerQty))
                {
                    total += hamburgerQty * cafePrices[1];
                }

                // Картопля-фрі
                if (CbFries?.IsChecked == true && TxtFriesQty != null &&
                    decimal.TryParse(TxtFriesQty.Text, out decimal friesQty))
                {
                    total += friesQty * cafePrices[2];
                }

                // Кока-кола
                if (CbCola?.IsChecked == true && TxtColaQty != null &&
                    decimal.TryParse(TxtColaQty.Text, out decimal colaQty))
                {
                    total += colaQty * cafePrices[3];
                }

                TxtCafeTotal.Text = total.ToString("F2", culture) + " грн.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка розрахунку кафе: {ex.Message}");
            }
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            try
            {
                if (TxtFuelDetail == null || TxtCafeDetail == null || TxtTotal == null || TxtDailyRevenue == null) return;

                // Розрахунок пального
                decimal fuelTotal = 0;
                if (RbByLiters?.IsChecked == true && TxtFuelResult != null &&
                    decimal.TryParse(TxtFuelResult.Text, out fuelTotal))
                {
                    // fuelTotal вже отримано
                }
                else if (RbByMoney?.IsChecked == true && TxtMoney != null)
                {
                    if (decimal.TryParse(TxtMoney.Text, out decimal money))
                    {
                        fuelTotal = money;
                    }
                }

                // Розрахунок кафе
                decimal cafeTotal = 0;
                if (TxtCafeTotal != null && decimal.TryParse(TxtCafeTotal.Text.Replace(" грн.", ""), out cafeTotal))
                {
                    // cafeTotal вже отримано
                }

                decimal total = fuelTotal + cafeTotal;

                // Оновлення деталізації
                TxtFuelDetail.Text = fuelTotal.ToString("F2", culture) + " грн.";
                TxtCafeDetail.Text = cafeTotal.ToString("F2", culture) + " грн.";
                TxtTotal.Text = total.ToString("F2", culture) + " грн.";

                // Додаємо до загальної виручки
                dailyRevenue += total;
                TxtDailyRevenue.Text = dailyRevenue.ToString("F2", culture) + " грн.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка розрахунку загальної суми: {ex.Message}");
            }
        }

        private void BtnNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Скидання полів для нового клієнта
                if (TxtLiters != null) TxtLiters.Text = "0";
                if (TxtMoney != null) TxtMoney.Text = "0";

                if (CbHotDog != null) CbHotDog.IsChecked = false;
                if (CbHamburger != null) CbHamburger.IsChecked = false;
                if (CbFries != null) CbFries.IsChecked = false;
                if (CbCola != null) CbCola.IsChecked = false;

                if (TxtHotDogQty != null) TxtHotDogQty.Text = "0";
                if (TxtHamburgerQty != null) TxtHamburgerQty.Text = "0";
                if (TxtFriesQty != null) TxtFriesQty.Text = "0";
                if (TxtColaQty != null) TxtColaQty.Text = "0";

                if (TxtFuelDetail != null) TxtFuelDetail.Text = "0,00 грн.";
                if (TxtCafeDetail != null) TxtCafeDetail.Text = "0,00 грн.";
                if (TxtTotal != null) TxtTotal.Text = "0,00 грн.";

                if (RbByLiters != null) RbByLiters.IsChecked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка скидання: {ex.Message}");
            }
        }

        private void BtnDailyReport_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Загальна виручка за день: {dailyRevenue.ToString("F2", culture)} грн.",
                           "Звіт за день",
                           MessageBoxButton.OK,
                           MessageBoxImage.Information);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MessageBox.Show($"Робочий день завершено!\nЗагальна виручка: {dailyRevenue.ToString("F2", culture)} грн.",
                           "Підсумок дня",
                           MessageBoxButton.OK,
                           MessageBoxImage.Information);
        }
    }
}