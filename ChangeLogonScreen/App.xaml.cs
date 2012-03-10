using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;

namespace ChangeLogonScreen
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Images (.jpg)|*.jpg";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;

                FileInfo fileInfo = new FileInfo(filename);

                if (System.IO.Path.GetExtension(filename).ToUpper() != ".JPG")
                {
                    MessageBox.Show("Wrong image extension. Should be .jpg.", "Wrong extension", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (fileInfo.Length > 256 * 1024)
                {
                    MessageBox.Show("File too large. Max size is 256 KB", "File too large", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string destPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"oobe\info\backgrounds");
                    string name = "backgroundDefault.jpg";

                    if (Directory.Exists(destPath))
                    {
                        byte[] bytes = File.ReadAllBytes(filename);
                        File.WriteAllBytes(System.IO.Path.Combine(destPath, name), bytes);

                        MessageBox.Show("Logon screen background has been changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }

            Application.Current.Shutdown();
        }
    }
}
