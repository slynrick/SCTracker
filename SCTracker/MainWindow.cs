using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using SCTracker.Utils;

namespace SCTracker
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string OpenAVMDlg()
        {
            OpenFileDialog FileDlg = new OpenFileDialog
            {
                Filter = "AVM file (*.avm)|*.avm",
                FilterIndex = 1,
                Multiselect = false,
                RestoreDirectory = true
            };

            if (FileDlg.ShowDialog() == DialogResult.OK)
            {
                return FileDlg.FileName;
            }

            return "";
        }

        private void ReadAVM(String filename)
        {
            if (filename.Length == 0)
                return;
            SCReader x = new SCReader(filename);
            ScriptText.Text = x.ReadAllScript();
            OpenedFile.Text = x.ToString();
        }

        private void ScriptText_TextChanged(object sender, EventArgs e)
        {
            if (ScriptText.TextLength % 2 != 0)
                TrackerOutputText.Text += "Not Hex String (Inappropriate Length)\n".CreateLogMessage(Helper.MessageClass.Error);
            if (!ScriptText.Text.IsHex())
                TrackerOutputText.Text += "Not Hex String (Inappropriate Characters)\n".CreateLogMessage(Helper.MessageClass.Error);
        }

        private void OpenSCMenuItem_Click(object sender, EventArgs e)
        {
            ReadAVM(OpenAVMDlg());

            
        }

        private void CheckOpcodesMenuItem_Click(object sender, EventArgs e)
        {
            // set it reaonly to not change the script
            ScriptText.ReadOnly = true;

            new Thread(() => {
                this.Invoke((MethodInvoker)delegate ()
                {
                    TrackerOutputText.Text += ("Running CheckOpcodes with Thread " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n").CreateLogMessage(Helper.MessageClass.Information);
                    // runs the checkcode here
                });
            }).Start();
            TrackerOutputText.Text += "CheckOpcodes Finishes \n".CreateLogMessage(Helper.MessageClass.Information);

            // put it back to be changed
            ScriptText.ReadOnly = false;
        }
    }
}
