using Reactor.Models;
using Reactor.MVVM.ViewModel;
using Reactor.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Reactor.MVVM.View
{

    public partial class UtilityView : UserControl
    {
        private UtilityViewModel _viewModel;
        private RamInfo _ramInfo = new RamInfo();
        public RamInfo RamInfo => _ramInfo;

        private DispatcherTimer _refreshTimer;
        private CpuInfo _cpuInfo = new CpuInfo();

        public UtilityView()
        {
            InitializeComponent();
            _viewModel = new UtilityViewModel();
            DataContext = _viewModel;
            LoadStorageData();
            StartRefreshTimer();
            //StartGpuMonitor();
            StartRamMonitor();
            StartCpuMonitor();
        }


        public CpuInfo CpuInfo => _cpuInfo;
        private void StartCpuMonitor()
        {

            var systemInfoService = new SystemInfoService();
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            timer.Tick += (s, e) =>
            {
                var info = systemInfoService.GetCpuInfo();
                _viewModel.CpuInfo.UsagePercentage = info.UsagePercentage;
                _viewModel.CpuInfo.Name = info.Name;
                _viewModel.CpuInfo.CoreCount = info.CoreCount;
            };
            timer.Start();
        }
        private void CpuTimer_Tick(object sender, EventArgs e)
        {
            var systemInfoService = new SystemInfoService();
            var cpuData = systemInfoService.GetCpuInfo();
            CpuUsage.DataContext = cpuData;

            //var pulse = (Storyboard)FindResource("PulseAnimation"); //Need to research this mode on how to use it maybe in an update
            //Storyboard.SetTarget(pulse, CpuBar);
            //pulse.Begin();
        }
        private void StartRamMonitor()
        {
            var systemInfoService = new SystemInfoService();

            DispatcherTimer ramTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };

            ramTimer.Tick += (s, e) =>
            {
                var info = systemInfoService.GetRamInfo(); // returns a RamInfo object

                _viewModel.RamInfo.TotalMemory = info.TotalMemory;
                _viewModel.RamInfo.UsedMemory = info.UsedMemory;
            };

            ramTimer.Start();
        }
        private GpuInfo _gpuInfo = new GpuInfo();

        //private void StartGpuMonitor()
        //{
        //    DispatcherTimer gpuTimer = new DispatcherTimer
        //    {
        //        Interval = TimeSpan.FromSeconds(2)
        //    };

        //    gpuTimer.Tick += (s, e) =>
        //    {
        //        var info = SystemInfoService.GetGpuInfo();
        //        /* info.UsagePercentage = SystemInfoService.GetGpuUsage();*/

        //        _gpuInfo.Name = info.Name;
        //        _gpuInfo.MemoryTotalMB = info.MemoryTotalMB;
        //        _gpuInfo.MemoryUsedMB = info.MemoryUsedMB;
        //        _gpuInfo.UsagePercentage = info.UsagePercentage;
        //    };

        //    gpuTimer.Start();
        //}
        private void LoadStorageData()
        {
            var systemInfoService = new SystemInfoService();
            var storageData = systemInfoService.GetStorageInfo();

            if (storageData != null)
            {
                // Make sure DataContext is your ViewModel and update its StorageInfo
                if (DataContext is UtilityViewModel vm)
                {
                    vm.StorageInfo = storageData;
                }
                else
                {
                    MessageBox.Show("ViewModel not found. DataContext is not set properly.");
                }
            }
            else
            {
                MessageBox.Show("No drive data was found.");
            }
        }

        private void StartRefreshTimer()
        {
            _refreshTimer = new DispatcherTimer();
            _refreshTimer.Interval = TimeSpan.FromSeconds(10); // every 10 seconds
            _refreshTimer.Tick += (s, e) => LoadStorageData();
            _refreshTimer.Start();
        }
    }
}
