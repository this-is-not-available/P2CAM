using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2CAM
{
    public partial class OptionsForm : Form
    {
        private AssetBrowser _parentForm;

        public OptionsForm(AssetBrowser parent)
        {
            _parentForm = parent;
            InitializeComponent();
        }

        public void Init()
        {
            P2InstallDirectory.Text = Program.options.Portal2_Dir;
        }

        public void Btn_LocateP2InstallDir_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                // OK button was pressed.
                if (result == DialogResult.OK)
                {
                    P2InstallDirectory.Text = fbd.SelectedPath;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        public void BtnOptionsSave_Click(object sender, EventArgs e)
        {
            // Validate p2dir
            if (!SteamUtils.ValidatePortal2Directory(P2InstallDirectory.Text))
            {
                DialogResult result = MessageBox.Show("This folder couldn't be recognized as a valid Portal 2 install directory. Continue?", "Invalid folder", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    return;
            }

            Program.options.Portal2_Dir = P2InstallDirectory.Text;
            _parentForm!.LoadAssets();
        }
    }
}
