using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SCS_Extractor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            string binary = "\""+AppDomain.CurrentDomain.BaseDirectory + "scs_extractor.exe\"";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\program files (x86)";
            openFileDialog1.Filter = "SCS Archive(s) (*.SCS)|*.SCS|All files (*.*)|*.*";
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select SCS Archive(s) to extract...";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    string FileExt = System.IO.Path.GetFileName(file);
                    int idx = FileExt.LastIndexOf('.');
                    string FileOnly = ""; if (idx >= 0) FileOnly = FileExt.Substring(0, idx);
                    string PathOnly = System.IO.Path.GetDirectoryName(file);
                    string cmdline = "\""+file+ "\" \"" + PathOnly+"\\"+FileOnly+"\"";
                    //MessageBox.Show(binary + " "+cmdline, "SCS Extractor GUI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process process = new Process() {
                        StartInfo = new ProcessStartInfo(binary, cmdline) {
                            WindowStyle = ProcessWindowStyle.Normal, WorkingDirectory = PathOnly
                        }
                    };
                    process.Start();
                }
            }
         }
    }
}
