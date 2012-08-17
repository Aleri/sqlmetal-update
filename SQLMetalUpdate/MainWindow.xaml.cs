using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;

namespace SQLMetalUpdate
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FileManager fileManager;

        public MainWindow()
        {
            InitializeComponent();
            fileManager = new FileManager();
            LoadSettings();
        }

        /// <summary>
        ///   Load settings from disk and display them to the user.
        /// </summary>
        private void LoadSettings()
        {
            fileManager.Load();
            if (fileManager.Settings.FileToBeEdited != null) txtFileToUpdate.Text = fileManager.Settings.FileToBeEdited;
            txtLineToInsertAt.Text = fileManager.Settings.LineToEdit.ToString();
            if (fileManager.Settings.SQLMetalFile != null) txtSQLMetal.Text = fileManager.Settings.SQLMetalFile;
            if (fileManager.Settings.TextToInsert != null) txtTextToInsert.Text = fileManager.Settings.TextToInsert;
        }

        /// <summary>
        ///   Runs the file chosen by the user and redirects output to the output window.
        /// </summary>
        private void RunProcess()
        {
            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = txtSQLMetal.Text;
            p.Start();
            txtOutput.Text += p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            RunProcess();
            fileManager.InsertLineInFile(txtFileToUpdate.Text, txtTextToInsert.Text, int.Parse(txtLineToInsertAt.Text));
            txtOutput.Text += "\n > Lines inserted into file successfully";
        }

        /// <summary>
        ///   Save what is written into text fields to file.
        /// </summary>
        private void SaveSettings()
        {
            fileManager.Settings.FileToBeEdited = txtFileToUpdate.Text;
            fileManager.Settings.SQLMetalFile = txtSQLMetal.Text;
            fileManager.Settings.LineToEdit = int.Parse(txtLineToInsertAt.Text);
            fileManager.Settings.TextToInsert = txtTextToInsert.Text;
            fileManager.Save(fileManager.Settings);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }

        private void btnBrowseFileToUpdate_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                txtFileToUpdate.Text = ofd.FileName;
            }
        }
    }
}