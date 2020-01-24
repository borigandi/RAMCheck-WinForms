namespace RAMdetect
{
    class Facade
    {
        private IMemoryInfoAsync RAMInfo { get; }

        public Facade(Form1 form)
        {
            RAMInfo = new RAMInfo(form);
        }

        public void GetRAMInfoSelectedProcess(string selectedProcessName)
        {
            RAMInfo.GetRAMInfoSelectedProcessAsync(selectedProcessName);
        }
        public void GetRAMInfoAllProcesses()
        {
            RAMInfo.GetRAMInfoAllProcessesAsync();
        }
        public void KillSelectedProcess(string selectedProcessName)
        {
            ProcessKiller.KillSelectedProcess(selectedProcessName);
        }
    }
}
