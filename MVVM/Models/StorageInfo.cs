using System.ComponentModel;

namespace Reactor.Models
{
    public class StorageInfo : INotifyPropertyChanged
    {
        private double _usagePercentage;

        public string DriveName { get; set; }
        public long TotalSpace { get; set; }
        public long FreeSpace { get; set; }

        public double UsagePercentage
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

        // INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}