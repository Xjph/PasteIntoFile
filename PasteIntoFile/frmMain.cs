﻿using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PasteAsFile
{
    public partial class frmMain : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardOwner();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private Process ClipboardProcess()
        {
            uint processId;
            GetWindowThreadProcessId(GetClipboardOwner(), out processId);
            return Process.GetProcessById((int)processId);
        }
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

            Process clipboardProcess = ClipboardProcess();
            
            txtFilename.Text = 
                (clipboardProcess.ProcessName == "svchost" && Clipboard.ContainsImage()? 
                "Screenclip" : string.IsNullOrWhiteSpace(clipboardProcess.MainModule.FileVersionInfo.FileDescription) ? 
                    clipboardProcess.ProcessName : clipboardProcess.MainModule.FileVersionInfo.FileDescription)
                + DateTime.Now.ToString(format: " - yyyy-MM-dd HH-mm-ss");

            if (Clipboard.ContainsText())
            {
                txtCurrentLocation.Text = CurrentLocation ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                lblType.Text = "Text File";
                comExt.SelectedItem = "txt";
                IsText = true;
                txtContent.Text = Clipboard.GetText();
                return;
            }

            if (Clipboard.ContainsImage())
            {
                txtCurrentLocation.Text = CurrentLocation ?? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                lblType.Text = "Image";
                comExt.SelectedItem = "png";
                imgContent.Image = Clipboard.GetImage();
                return;
            }

            lblType.Text = "Unknown File";
            btnSave.Enabled = false;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string location = txtCurrentLocation.Text;
            location = location.EndsWith("\\") ? location : location + "\\";
            string filename = txtFilename.Text + "." + comExt.SelectedItem.ToString();
            if (IsText)
            {

                File.WriteAllText(location + filename, txtContent.Text, Encoding.UTF8);
                this.Text += " : File Saved :)";
            }
            else
            {
                switch (comExt.SelectedItem.ToString())
                {
                    case "png":
                        imgContent.Image.Save(location + filename, ImageFormat.Png);
                        break;
                    case "ico":
                        imgContent.Image.Save(location + filename, ImageFormat.Icon);
                        break;
                    case "jpg":
                        imgContent.Image.Save(location + filename, ImageFormat.Jpeg);
                        break;
                    case "bmp":
                        imgContent.Image.Save(location + filename, ImageFormat.Bmp);
                        break;
                    case "gif":
                        imgContent.Image.Save(location + filename, ImageFormat.Gif);
                        break;
                    default:
                        imgContent.Image.Save(location + filename, ImageFormat.Png);
                        break;
                }

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
    }
}
