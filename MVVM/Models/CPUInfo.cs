using System.ComponentModel;

namespace Reactor.Models
{
    public class CpuInfo : INotifyPropertyChanged
    {
        private string _name;
        private float _usagePercentage;
        private int _coreCount;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public float UsagePercentage
        {
            get => _usagePercentage;
            set
            {
                if (_usagePercentage != value)
                {
                    _usagePercentage = value;
                    OnPropertyChanged(nameof(UsagePercentage));
                }
            }
        }
        public int CoreCount
        {
            get => _coreCount;
            set
            {
                if (_coreCount != value)
                {
                    _coreCount = value;
                    OnPropertyChanged(nameof(CoreCount));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
