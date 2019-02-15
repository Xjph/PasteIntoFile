using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace PasteIntoFile
{
	static class Program
	{
        /// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (args.Length>0)
			{
				if (args[0] == "/reg")
				{
					RegisterApp();
				}
				else if (args[0] == "/unreg")
				{
					UnRegisterApp();
				}
                else if ((bool)PasteIntoFile.Properties.Settings.Default["noUI"])
                {
                    if (Clipboard.ContainsText())
                    {
                        SaveTextToFile(Clipboard.GetText(), DateTime.Now.ToString(Properties.Settings.Default.filenameFormat.Replace("%application%", "'"+GetSourceName()+"'")), args[0]);
                    }
                    else if (Clipboard.ContainsImage())
                    {
                        SaveImageToFile(Clipboard.GetImage(), DateTime.Now.ToString(Properties.Settings.Default.filenameFormat.Replace("%application%", "'" + GetSourceName() + "'")), args[0]);
                    }
                }
                else
                {
                    Application.Run(new frmMain(args[0]));
                }
			}
			else
			{
				Application.Run(new frmMain());
			}
			
		}

        public static string GetSourceName()
        {
            Process clipboardProcess = ClipboardProcess();

            if (clipboardProcess.ProcessName == "Idle")
            {
                return "";
            }
            else
            {
                return (clipboardProcess.ProcessName == "svchost" && Clipboard.ContainsImage() ?
                    "Screenclip" : string.IsNullOrWhiteSpace(clipboardProcess.MainModule.FileVersionInfo.FileDescription) ?
                        clipboardProcess.ProcessName : clipboardProcess.MainModule.FileVersionInfo.FileDescription);
            }
        }

        public static void SaveTextToFile(string textData, string filename, string location)
        {
            location = location.EndsWith("\\") ? location : location + "\\";
            
            File.WriteAllText(location + filename + "." + Properties.Settings.Default.defaultTextType, textData, Encoding.UTF8);
        }

        public static void SaveImageToFile(System.Drawing.Image imageData, string filename, string location)
        {
            location = location.EndsWith("\\") ? location : location + "\\";
            filename += "." + Properties.Settings.Default.defaultImageType;
            switch (Properties.Settings.Default.defaultImageType)
            {
                case "png":
                    imageData.Save(location + filename, ImageFormat.Png);
                    break;
                case "ico":
                    imageData.Save(location + filename, ImageFormat.Icon);
                    break;
                case "jpg":
                    imageData.Save(location + filename, ImageFormat.Jpeg);
                    break;
                case "bmp":
                    imageData.Save(location + filename, ImageFormat.Bmp);
                    break;
                case "gif":
                    imageData.Save(location + filename, ImageFormat.Gif);
                    break;
                default:
                    imageData.Save(location + filename, ImageFormat.Png);
                    break;
            }
        }

		public static void UnRegisterApp()
		{
			try
			{
				var key = OpenDirectoryKey().OpenSubKey(@"Background\shell", true);
				key.DeleteSubKeyTree("Paste Into File");

				key = OpenDirectoryKey().OpenSubKey("shell", true);
				key.DeleteSubKeyTree("Paste Into File");

				MessageBox.Show("Application has been Unregistered from your system", "Paste Into File", MessageBoxButtons.OK, MessageBoxIcon.Information);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\nPlease run the application as Administrator !", "Paste Into File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				
			}
		}

		public static void RegisterApp()
		{
			try
			{
				var key = OpenDirectoryKey().CreateSubKey(@"Background\shell").CreateSubKey("Paste Into File");
				key = key.CreateSubKey("command");
				key.SetValue("", Application.ExecutablePath + " \"%V\"");

				key = OpenDirectoryKey().CreateSubKey("shell").CreateSubKey("Paste Into File");
				key = key.CreateSubKey("command");
				key.SetValue("", Application.ExecutablePath + " \"%1\"");
				MessageBox.Show("Application has been registered with your system", "Paste Into File", MessageBoxButtons.OK, MessageBoxIcon.Information);

			}
			catch (Exception ex)
			{
				//throw;
				MessageBox.Show(ex.Message + "\nPlease run the application as Administrator !", "Paste As File", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public static void RestartApp()
		{
			ProcessStartInfo proc = new ProcessStartInfo();
			proc.UseShellExecute = true;
			proc.WorkingDirectory = Environment.CurrentDirectory;
			proc.FileName = Application.ExecutablePath;
			proc.Verb = "runas";

			try
			{
				Process.Start(proc);
			}
			catch
			{
				// The user refused the elevation.
				// Do nothing and return directly ...
				return;
			}
			Application.Exit();
		}

		static RegistryKey OpenDirectoryKey()
		{
			return Registry.CurrentUser.CreateSubKey(@"Software\Classes\Directory");
		}

        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardOwner();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        static private Process ClipboardProcess()
        {
            GetWindowThreadProcessId(GetClipboardOwner(), out uint processId);
            return Process.GetProcessById((int)processId);
        }
    }
}
