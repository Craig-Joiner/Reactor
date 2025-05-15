using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Reactor.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public UtilityViewModel UtilityVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            UtilityVM = new UtilityViewModel();
            CurrentView = UtilityVM;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}


