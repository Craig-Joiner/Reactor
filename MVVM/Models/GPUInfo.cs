namespace Reactor.Models
{
    public class GpuInfo
    {
        public string Name { get; set; }
        public float UsagePercentage { get; set; }  // Optional, depending on how deep you go
        public float MemoryTotalMB { get; set; }
        public float MemoryUsedMB { get; set; }
    }
}
