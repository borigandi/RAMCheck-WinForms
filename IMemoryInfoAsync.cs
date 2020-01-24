using System.Threading.Tasks;

namespace RAMdetect
{
    interface IMemoryInfoAsync
    {
        Task GetRAMInfoSelectedProcessAsync(string selectedProcessName);
        Task GetRAMInfoAllProcessesAsync();
    }
}
