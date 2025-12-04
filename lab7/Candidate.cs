using System;
using System.ComponentModel;

namespace JobCandidatesApp
{
    // Модель кандидата з реалізацією INotifyPropertyChanged для автоматичного оновлення UI
    public class Candidate : INotifyPropertyChanged
    {
        private string fullName;
        private DateTime? birthDate;
        private string education;
        private bool knowsEnglish;
        private string englishLevel;
        private bool knowsGerman;
        private string germanLevel;
        private bool knowsFrench;
        private string frenchLevel;
        private bool hasComputerSkills;
        private int workExperience;
        private bool hasRecommendations;

        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для виклику події PropertyChanged
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Властивості з автоматичним сповіщенням про зміни
        public string FullName
        {
            get => fullName;
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public DateTime? BirthDate
        {
            get => birthDate;
            set
            {
                if (birthDate != value)
                {
                    birthDate = value;
                    OnPropertyChanged(nameof(BirthDate));
                    OnPropertyChanged(nameof(BirthYear));
                }
            }
        }

        public int BirthYear => birthDate?.Year ?? 0;

        public DateTime MaxDate => DateTime.Today.AddYears(-18); // Мінімум 18 років

        public string Education
        {
            get => education;
            set
            {
                if (education != value)
                {
                    education = value;
                    OnPropertyChanged(nameof(Education));
                }
            }
        }

        public bool KnowsEnglish
        {
            get => knowsEnglish;
            set
            {
                if (knowsEnglish != value)
                {
                    knowsEnglish = value;
                    OnPropertyChanged(nameof(KnowsEnglish));
                }
            }
        }

        public string EnglishLevel
        {
            get => englishLevel;
            set
            {
                if (englishLevel != value)
                {
                    englishLevel = value;
                    OnPropertyChanged(nameof(EnglishLevel));
                }
            }
        }

        public bool KnowsGerman
        {
            get => knowsGerman;
            set
            {
                if (knowsGerman != value)
                {
                    knowsGerman = value;
                    OnPropertyChanged(nameof(KnowsGerman));
                }
            }
        }

        public string GermanLevel
        {
            get => germanLevel;
            set
            {
                if (germanLevel != value)
                {
                    germanLevel = value;
                    OnPropertyChanged(nameof(GermanLevel));
                }
            }
        }

        public bool KnowsFrench
        {
            get => knowsFrench;
            set
            {
                if (knowsFrench != value)
                {
                    knowsFrench = value;
                    OnPropertyChanged(nameof(KnowsFrench));
                }
            }
        }

        public string FrenchLevel
        {
            get => frenchLevel;
            set
            {
                if (frenchLevel != value)
                {
                    frenchLevel = value;
                    OnPropertyChanged(nameof(FrenchLevel));
                }
            }
        }

        public bool HasComputerSkills
        {
            get => hasComputerSkills;
            set
            {
                if (hasComputerSkills != value)
                {
                    hasComputerSkills = value;
                    OnPropertyChanged(nameof(HasComputerSkills));
                }
            }
        }

        public int WorkExperience
        {
            get => workExperience;
            set
            {
                if (workExperience != value)
                {
                    workExperience = value;
                    OnPropertyChanged(nameof(WorkExperience));
                }
            }
        }

        public bool HasRecommendations
        {
            get => hasRecommendations;
            set
            {
                if (hasRecommendations != value)
                {
                    hasRecommendations = value;
                    OnPropertyChanged(nameof(HasRecommendations));
                }
            }
        }

        // Конструктор за замовчуванням
        public Candidate()
        {
            FullName = "";
            BirthDate = DateTime.Today.AddYears(-25);
            Education = "Середня";
            EnglishLevel = "Володію вільно";
            GermanLevel = "Володію вільно";
            FrenchLevel = "Володію вільно";
        }
    }
}
