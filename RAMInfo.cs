using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
using System.Threading.Tasks;

namespace RAMdetect
{
    class RAMInfo : IMemoryInfoAsync
    {
        private Form1 Form1Ref { get; }

        public RAMInfo(Form1 form1)
        {
            Form1Ref = form1;
        }

        

        public async Task GetRAMInfoSelectedProcessAsync(string selectedProcessName)
        {
            Tuple<double, double, double> results = new Tuple<double, double, double>(0, 0, 0);

            do
            {
                Task task = Task.Run(() =>
                {
                    var used = GetRAMInfoSelectedProcess(selectedProcessName);
                    results = CalculateRAMValue(used);
                });

                await task;

                ShowRAMInfo(results);
            } while (Form1.IsCircle);
        }

        public async Task GetRAMInfoAllProcessesAsync()
        {
            Tuple<double, double, double> results = new Tuple<double, double, double>(0, 0, 0);

            do
            {
                Task task = Task.Run(() =>
                {
                    var used = GetRAMInfoAllProcesses();
                    results = CalculateRAMValue(used);
                });

                await task;

                ShowRAMInfo(results);
            } while (Form1.IsCircle);
        }

        private long GetRAMInfoSelectedProcess(string selectedProcessName)
        {
            long usedMemoryOnBytes = default;
            IEnumerable<Process> processList = Process
                                               .GetProcesses()
                                               .Where(x => x.ProcessName == selectedProcessName)
                                               .Select(x => x);

            foreach (var process in processList)
            {
                usedMemoryOnBytes += process.WorkingSet64;
            }

            return usedMemoryOnBytes;

            //PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", p.ProcessName);
            //PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", p.ProcessName);
        }

        private long GetRAMInfoAllProcesses()
        {
            long usedMemoryOnBytes = default;
            var processList = Process.GetProcesses();

            foreach (Process process in processList)
            {
                usedMemoryOnBytes += process.WorkingSet64;
            }

            return usedMemoryOnBytes;
            //ShowRAMInfo(usedMemoryOnBytes);
        }

        private Tuple<double, double, double> CalculateRAMValue(long usedMemoryOnBytes)
        {
            ComputerInfo computerInfo = new ComputerInfo();

            double usedMemory = Math.Round(usedMemoryOnBytes / Math.Pow(1024, 2), 0);
            double freeMemory = Math.Round(computerInfo.AvailablePhysicalMemory / Math.Pow(1024, 2), 0);
            double totalMemory = Math.Round(computerInfo.TotalPhysicalMemory / Math.Pow(1024, 2), 0);

            return new Tuple<double, double, double>(totalMemory, usedMemory, freeMemory);
        }

        private void ShowRAMInfo(Tuple<double, double, double> results)
        {
            Form1Ref.listBox1.Items.Clear();
            Form1Ref.listBox1.Items.Add($"TOTAL: {results.Item1} MB"); // Total Memory
            Form1Ref.listBox1.Items.Add($"USED: {results.Item2} MB"); // Used Memory
            Form1Ref.listBox1.Items.Add($"FREE: {results.Item3} MB"); // Free Memory
        }
    }
}
