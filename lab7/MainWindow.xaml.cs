using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace JobCandidatesApp
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Створюємо ViewModel і встановлюємо як DataContext
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        // Додавання нового кандидата
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddCandidate();
        }

        // Видалення вибраного кандидата
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedCandidate != null)
            {
                var result = MessageBox.Show(
                    $"Ви впевнені, що хочете видалити кандидата '{viewModel.SelectedCandidate.FullName}'?",
                    "Підтвердження видалення",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    viewModel.DeleteCandidate(viewModel.SelectedCandidate);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть кандидата для видалення.",
                    "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Збереження змін
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedCandidate != null)
            {
                // Валідація даних
                if (string.IsNullOrWhiteSpace(viewModel.SelectedCandidate.FullName))
                {
                    MessageBox.Show("Будь ласка, введіть П.І.П. кандидата.",
                        "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!viewModel.SelectedCandidate.BirthDate.HasValue)
                {
                    MessageBox.Show("Будь ласка, виберіть дату народження.",
                        "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Перевірка віку (мінімум 18 років)
                var age = System.DateTime.Today.Year - viewModel.SelectedCandidate.BirthDate.Value.Year;
                if (viewModel.SelectedCandidate.BirthDate.Value.AddYears(age) > System.DateTime.Today)
                {
                    age--;
                }

                if (age < 18)
                {
                    MessageBox.Show("Кандидат повинен бути не молодшим 18 років.",
                        "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBox.Show("Дані успішно збережено!",
                    "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть кандидата.",
                    "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Зміна фільтру
        private void FilterComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.ApplyFilter(viewModel.FilterIndex);
            }
        }

        // Валідація введення тільки цифр (для стажу роботи)
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}