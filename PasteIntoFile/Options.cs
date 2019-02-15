using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteIntoFile
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            ComboText.Text = Properties.Settings.Default.defaultTextType;
            ComboImage.Text = Properties.Settings.Default.defaultImageType;
            CbxNoUI.Checked = Properties.Settings.Default.noUI;
            TextboxFormat.Text = Properties.Settings.Default.filenameFormat;
            
        }

        private void BtnCancel_Click(object sender, EventArgs e) => Close();

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.defaultTextType = ComboText.Text;
            Properties.Settings.Default.defaultImageType = ComboImage.Text;
            Properties.Settings.Default.noUI = CbxNoUI.Checked;
            Properties.Settings.Default.filenameFormat = TextboxFormat.Text;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}