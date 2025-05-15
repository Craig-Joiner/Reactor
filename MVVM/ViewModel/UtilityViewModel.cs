using Reactor.Models;
using System.ComponentModel;

namespace Reactor.MVVM.ViewModel
{
    public class UtilityViewModel : INotifyPropertyChanged
    {
        private CpuInfo _cpuInfo;
        public CpuInfo CpuInfo
        {
            get => _cpuInfo;
            set
            {
                if (_cpuInfo != value)
                {
                    _cpuInfo = value;
                    OnPropertyChanged(nameof(CpuInfo));
                }
            }
        }

        private RamInfo _ramInfo;
        public RamInfo RamInfo
        {
            get => _ramInfo;
            set
            {
                if (_ramInfo != value)
                {
                    _ramInfo = value;
                    OnPropertyChanged(nameof(RamInfo));
                }
            }
        }

        private StorageInfo _storageInfo;
        public StorageInfo StorageInfo
        {
            get => _storageInfo;
            set
            {
                if (_storageInfo != value)
                {
                    _storageInfo = value;
                    OnPropertyChanged(nameof(StorageInfo));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public UtilityViewModel()
        {
            // Initialize with default or fetched values
            CpuInfo = new CpuInfo();
            RamInfo = new RamInfo();
            StorageInfo = new StorageInfo();

            // TODO: Start your monitoring or loading logic here
            // e.g., LoadStorageData(), StartCpuMonitor(), etc.
        }

        // Optional: Methods to update your info dynamically
    }
}