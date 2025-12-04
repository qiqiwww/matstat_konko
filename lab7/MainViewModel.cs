using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace JobCandidatesApp
{
    // ViewModel для головного вікна
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Candidate> candidates;
        private ObservableCollection<Candidate> allCandidates;
        private Candidate selectedCandidate;
        private int filterIndex;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Колекція кандидатів для відображення (з урахуванням фільтру)
        public ObservableCollection<Candidate> Candidates
        {
            get => candidates;
            set
            {
                if (candidates != value)
                {
                    candidates = value;
                    OnPropertyChanged(nameof(Candidates));
                }
            }
        }

        // Повна колекція всіх кандидатів
        public ObservableCollection<Candidate> AllCandidates
        {
            get => allCandidates;
            set => allCandidates = value;
        }

        // Вибраний кандидат (двостороння прив'язка)
        public Candidate SelectedCandidate
        {
            get => selectedCandidate;
            set
            {
                if (selectedCandidate != value)
                {
                    selectedCandidate = value;
                    OnPropertyChanged(nameof(SelectedCandidate));
                }
            }
        }

        // Індекс фільтру
        public int FilterIndex
        {
            get => filterIndex;
            set
            {
                if (filterIndex != value)
                {
                    filterIndex = value;
                    OnPropertyChanged(nameof(FilterIndex));
                }
            }
        }

        // Конструктор з тестовими даними
        public MainViewModel()
        {
            AllCandidates = new ObservableCollection<Candidate>();

            // Додаємо тестових кандидатів
            AllCandidates.Add(new Candidate
            {
                FullName = "Іванов Іван Іванович",
                BirthDate = new System.DateTime(1990, 5, 15),
                Education = "Вища",
                KnowsEnglish = true,
                EnglishLevel = "Володію вільно",
                HasComputerSkills = true,
                WorkExperience = 5,
                HasRecommendations = true
            });

            AllCandidates.Add(new Candidate
            {
                FullName = "Петренко Марія Василівна",
                BirthDate = new System.DateTime(1995, 8, 22),
                Education = "Середня спеціальна",
                KnowsGerman = true,
                GermanLevel = "Читаю та перекладаю зі словником",
                HasComputerSkills = true,
                WorkExperience = 3,
                HasRecommendations = false
            });

            AllCandidates.Add(new Candidate
            {
                FullName = "Сидоренко Олександр Петрович",
                BirthDate = new System.DateTime(1988, 12, 10),
                Education = "Вища",
                KnowsEnglish = true,
                EnglishLevel = "Читаю та перекладаю зі словником",
                KnowsFrench = true,
                FrenchLevel = "Володію вільно",
                HasComputerSkills = true,
                WorkExperience = 8,
                HasRecommendations = true
            });

            Candidates = new ObservableCollection<Candidate>(AllCandidates);
            FilterIndex = 0;
        }

        // Метод додавання нового кандидата
        public void AddCandidate()
        {
            var newCandidate = new Candidate
            {
                FullName = "Новий кандидат"
            };

            AllCandidates.Add(newCandidate);
            ApplyFilter(FilterIndex);
            SelectedCandidate = newCandidate;
        }

        // Метод видалення кандидата
        public void DeleteCandidate(Candidate candidate)
        {
            if (candidate != null)
            {
                AllCandidates.Remove(candidate);
                ApplyFilter(FilterIndex);
            }
        }

        // Застосування фільтру
        public void ApplyFilter(int filterIndex)
        {
            switch (filterIndex)
            {
                case 0: // Усі кандидати
                    Candidates = new ObservableCollection<Candidate>(AllCandidates);
                    break;
                case 1: // З вищою освітою
                    Candidates = new ObservableCollection<Candidate>(
                        AllCandidates.Where(c => c.Education == "Вища"));
                    break;
                case 2: // Володіють комп'ютером
                    Candidates = new ObservableCollection<Candidate>(
                        AllCandidates.Where(c => c.HasComputerSkills));
                    break;
                case 3: // Зі знанням англійської
                    Candidates = new ObservableCollection<Candidate>(
                        AllCandidates.Where(c => c.KnowsEnglish));
                    break;
                case 4: // З рекомендаціями
                    Candidates = new ObservableCollection<Candidate>(
                        AllCandidates.Where(c => c.HasRecommendations));
                    break;
            }
        }
    }
}
