using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RAMdetect
{
    public partial class Form1 : Form
    {
        private Facade InfoGetter { get; }
        public static bool IsCircle { get; private set; } = false;

        public Form1()
        {
            InitializeComponent();
            InfoGetter = new Facade(this);
            LaunchHelper.AddProcessesToComboBoxAsync(this);
        }

        private void CheckSelectedProcessRAM_Button_Click(object sender, EventArgs e)
        {
            try
            {
                InfoGetter.GetRAMInfoSelectedProcess(comboBox1.SelectedItem.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Ви не обрали процес!", "УВАГА");
            }
        }

        private void CheckAllProcessesRAM_Button_Click(object sender, EventArgs e)
        {
            InfoGetter.GetRAMInfoAllProcesses();
        }

        private void KillSelectedProcess_Button_Click(object sender, EventArgs e)
        {
            try
            {
                InfoGetter.KillSelectedProcess(comboBox1.SelectedItem.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Ви не обрали процес!", "УВАГА");
            }
        }

        private void RefreshProcessesList_Button_Click(object sender, EventArgs e)
        {
            LaunchHelper.AddProcessesToComboBoxAsync(this);
        }

        private void CircleProcessesInfo_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (circleProcessesInfo_CheckBox.CheckState == CheckState.Checked)
                IsCircle = true;
            else
                IsCircle = false;
        }
    }
}
