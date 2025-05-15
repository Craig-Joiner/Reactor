using Reactor.Models;
using System.Diagnostics;
using System.IO;
using System.Management;
namespace Reactor.Services
{
    class SystemInfoService
    {
        private PerformanceCounter _cpuCounter;

        public SystemInfoService()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _cpuCounter.NextValue(); // First call always returns 0, prime it
        }

        public CpuInfo GetCpuInfo()
        {
            return new CpuInfo
            {
                Name = GetCpuName(),
                UsagePercentage = _cpuCounter.NextValue(),
                CoreCount = Environment.ProcessorCount
            };
        }

        private string GetCpuName()
        {
            string name = string.Empty;
            using (var searcher = new ManagementObjectSearcher("select Name from Win32_Processor"))
            {
                foreach (var item in searcher.Get())
                {
                    name = item["Name"]?.ToString();
                    break;
                }
            }
            return name;
        }
        public RamInfo GetRamInfo()
        {
            var searcher = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem");

            foreach (var item in searcher.Get())
            {
                float totalMemoryKB = float.Parse(item["TotalVisibleMemorySize"].ToString());
                float freeMemoryKB = float.Parse(item["FreePhysicalMemory"].ToString());

                return new RamInfo
                {
                    TotalMemory = totalMemoryKB / 1024, // MB
                    UsedMemory = (totalMemoryKB - freeMemoryKB) / 1024 // MB
                };
            }

            return null; // Fallback
        }
        //public static GpuInfo GetGpuInfo()
        //{
        //    var gpuInfo = new GpuInfo();

        //    using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
        //    {
        //        foreach (var obj in searcher.Get())
        //        {
        //            gpuInfo.Name = obj["Name"]?.ToString();
        //            gpuInfo.MemoryTotalMB = obj["AdapterRAM"] != null
        //                ? Convert.ToSingle((ulong)obj["AdapterRAM"] / (1024 * 1024))
        //                : 0;

        //            // You might not get memory used this way — we'll set 0 for now
        //            gpuInfo.MemoryUsedMB = 0;

        //            break; // Just grab the first GPU
        //        }
        //    }

        //    return gpuInfo;
        //}
        public StorageInfo GetStorageInfo()
        {
            var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.IsReady && d.DriveType == DriveType.Fixed);
            if (drive != null)
            {
                var total = drive.TotalSize;
                var free = drive.TotalFreeSpace;

                return new StorageInfo
                {
                    DriveName = drive.Name,
                    TotalSpace = total,
                    FreeSpace = free,
                    UsagePercentage = (1 - (free / (double)total)) * 100
                };
            }

            return null;
        }
    }
}
