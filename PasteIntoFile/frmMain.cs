using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteIntoFile
{
    public partial class frmMain : Form
    {
       

        public string CurrentLocation { get; set; }

        public bool IsText { get; set; }

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(string location)
        {
            InitializeComponent();
            this.CurrentLocation = location;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Classes\Directory\Background\shell\Paste Into File\command", "", null) == null)
            {
                if (MessageBox.Show("Seems that you are running this application for the first time,\nDo you want to Register it with your system Context Menu ?", "Paste Into File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.RegisterApp();
                }
            }

            txtFilename.Text = DateTime.Now.ToString(((string)PasteIntoFile.Properties.Settings.Default["filenameFormat"]).Replace("%application%", "'" + Program.GetSourceName() + "'"));

            if (Clipboard.ContainsText())
            {
                txtCurrentLocation.Text = CurrentLocation ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                lblType.Text = "Text File";
                comExt.Items.Remove("png");
                comExt.Items.Remove("jpg");
                comExt.Items.Remove("bmp");
                comExt.Items.Remove("gif");
                comExt.Items.Remove("ico");
                comExt.SelectedItem = (string)PasteIntoFile.Properties.Settings.Default["defaultTextType"];
                IsText = true;
                txtContent.Text = Clipboard.GetText();
            }
            else if (Clipboard.ContainsImage())
            {
                txtCurrentLocation.Text = CurrentLocation ?? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                lblType.Text = "Image";
                comExt.Items.Remove("txt");
                comExt.Items.Remove("html");
                comExt.Items.Remove("js");
                comExt.Items.Remove("css");
                comExt.Items.Remove("csv");
                comExt.Items.Remove("json");
                comExt.Items.Remove("cs");
                comExt.Items.Remove("cpp");
                comExt.Items.Remove("java");
                comExt.Items.Remove("php");
                comExt.SelectedItem = (string)PasteIntoFile.Properties.Settings.Default["defaultImageType"];
                imgContent.Image = Clipboard.GetImage();
            }
            else
            {
                lblType.Text = "Unknown File";
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string location = txtCurrentLocation.Text;
            if (IsText)
            {
                Program.SaveTextToFile(txtContent.Text, txtFilename.Text, location);
                this.Text += " : File Saved :)";
            }
            else
            {
                Program.SaveImageToFile(imgContent.Image, txtFilename.Text, location);
                this.Text += " : Image Saved :)";
            }

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                Environment.Exit(0);
            });
        }

        private void btnBrowseForFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a folder for saving this file ";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtCurrentLocation.Text = fbd.SelectedPath;
            }
        }

        private void lblWebsite_Click(object sender, EventArgs e)
        {
            Process.Start("http://eslamx.com");
        }

        private void lblMe_Click(object sender, EventArgs e)
        {
            Process.Start("http://twitter.com/EslaMx7");
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            string msg = "Paste Into File helps you paste any text or images in your system clipboard into a file directly instead of creating new file yourself";
            msg += "\n--------------------\nTo Register the application to your system Context Menu run the program as Administrator with this argument : /reg";
            msg += "\nto Unregister the application use this argument : /unreg\n";
            msg += "\n--------------------\nSend Feedback to : eslamx7@gmail.com\n\nThanks :)";
            MessageBox.Show(msg, "Paste As File Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtFilename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSave_Click(sender, null);
            }
        }

        private void BtnOption_Click(object sender, EventArgs e)
        {
            PasteIntoFile.Options options = new PasteIntoFile.Options();
            options.Show();
            options.SetDesktopLocation(DesktopLocation.X, DesktopLocation.Y);
        }
    }
}
