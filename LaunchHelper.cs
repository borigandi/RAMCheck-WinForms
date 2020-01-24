using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RAMdetect
{
    static class LaunchHelper
    {
        public static async void AddProcessesToComboBoxAsync(Form1 form1)
        {
            Process[] allProcesses = Process.GetProcesses();
            form1.label1.Text = $"Processes: {allProcesses.Count()}";

            List<string> processesNames = await Task.Run(() =>
            {

                HashSet<string> combineProcessesNames = new HashSet<string>();

                foreach (Process process in allProcesses)
                {
                    combineProcessesNames.Add(process.ProcessName);
                }

                return combineProcessesNames.ToList();

            });

            form1.comboBox1.Items.Clear();
            foreach (string processName in processesNames)
            {
                form1.comboBox1.Items.Add(processName);
            }
        }
    }
}
