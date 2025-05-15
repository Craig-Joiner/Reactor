using System.ComponentModel;

namespace Reactor.Models
{
    public class RamInfo : INotifyPropertyChanged
    {
        private float _totalMemory;
        private float _usedMemory;

        public float TotalMemory
        {
            get => _totalMemory;
            set
            {
                if (_totalMemory != value)
                {
                    _totalMemory = value;
                    OnPropertyChanged(nameof(TotalMemory));
                    OnPropertyChanged(nameof(FreeMemory));
                    OnPropertyChanged(nameof(UsagePercentage));
                }
            }
        }

        public float UsedMemory
        {
            get => _usedMemory;
            set
            {
                if (_usedMemory != value)
                {
                    _usedMemory = value;
                    OnPropertyChanged(nameof(UsedMemory));
                    OnPropertyChanged(nameof(FreeMemory));
                    OnPropertyChanged(nameof(UsagePercentage));
                }
            }
        }

        public float FreeMemory => TotalMemory - UsedMemory;
        public float UsagePercentage => TotalMemory == 0 ? 0 : (UsedMemory / TotalMemory) * 100;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}