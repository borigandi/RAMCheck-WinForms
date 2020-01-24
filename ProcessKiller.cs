using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RAMdetect
{
    struct ProcessKiller
    {
        public static void KillSelectedProcess(string selectedProcessName)
        {
            if (GetUserChoice())
            {
                var processList = Process
                                  .GetProcesses()
                                  .Where(x => x.ProcessName == selectedProcessName)
                                  .Select(x => x);

                foreach (var process in processList)
                {
                    process.Kill();
                }
            }
        }
        private static bool GetUserChoice()
        {
            DialogResult result = MessageBox.Show("Ви дійсно хочете завершити цей процес?", "Підтвердіть вибір", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                 MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            return result == DialogResult.Yes ? true : false;
        }
    }
}
