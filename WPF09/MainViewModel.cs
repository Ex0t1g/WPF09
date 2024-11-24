

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace WPF09
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<City> _cities;
        private City _selectedCity;
        public ICommand AddCityCommand { get; }
        public ICommand DeleteCityCommand { get; }
        public ObservableCollection<Country> Countries { get; set; }
        public ObservableCollection<City> Cities
        {
            get => _cities;
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }
        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }
        public MainViewModel()
        {
            Cities = new ObservableCollection<City>
            {
                new City { ID = 1, Name = "Москва", Country = "Россия" },
                new City { ID = 2, Name = "Нью-Йорк", Country = "США" }
            };

            Countries = new ObservableCollection<Country>
            {
                new Country { Name = "Россия" },
                new Country { Name = "США" },
                new Country { Name = "Канада" }
            };

            AddCityCommand = new RelayCommand(AddCity);
            DeleteCityCommand = new RelayCommand(DeleteCity, CanDeleteCity);
        }
        private void AddCity(object parameter)
        {
            var newId = Cities.Any() ? Cities.Max(c => c.ID) + 1 : 1; 
            Cities.Add(new City { ID = newId, Name = "Новый Город", Country = "Россия" }); 
        }

        private bool CanDeleteCity(object parameter)
        {
            return SelectedCity != null; 
        }

        private void DeleteCity(object parameter)
        {
            if (SelectedCity != null)
            {
                Cities.Remove(SelectedCity);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
