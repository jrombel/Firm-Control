using System.Windows;
using System.IO;
using Microsoft.Win32;
using MahApps.Metro.Controls;

namespace Firm_Control
{
    public partial class Set : MetroWindow
    {
        string pathToBase;
        public Set()
        {
            InitializeComponent();
            GoToNextWindow();
        }
        private void GoToNextWindow()
        {
            FileInfo fi = new FileInfo("src.txt");
            if (File.Exists("src.txt") && fi != null)
            {
                string file;
                StreamReader read = new StreamReader("src.txt");
                pathToBase = read.ReadLine();
                Company.pathToBase = pathToBase;
                if (!File.Exists(pathToBase))
                {
                    read.Close();
                    File.Delete("src.txt");
                }
                else
                {
                    read = new StreamReader(pathToBase);
                    file = read.ReadLine();
                    read.Close();
                    (new Login(file)).Show();
                    this.Close();
                }
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Firm Controle Base (*.fcb)|*.fcb";
            saveFileDialog.FileName = name_tb.Text;
            if (name_tb.Text != "")
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    StreamWriter write = new StreamWriter("src.txt");
                    File.WriteAllText(saveFileDialog.FileName, "empty");
                    write.Write(saveFileDialog.FileName);
                    write.Close();
                }
            }
            else
            {
                MessageBox.Show("Musisz wprowadzić nazwę firmy", "Firm Control", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            GoToNextWindow();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                StreamWriter write = new StreamWriter("src.txt");
                write.Write(openFileDialog.FileName);
                write.Close();
            }
            GoToNextWindow();
        }
    }
}