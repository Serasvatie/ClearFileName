using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClearFileNameWPF
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _directory;
        public string Directory
        {
            get
            {
                return _directory;
            }
            set
            {
                _directory = value;
                this.OnPropertyChanged("Directory");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Directory = "Select Path...";
            this.DataContext = this;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            if (!FileBox.IsChecked.Value && !DirectoryBox.IsChecked.Value)
                System.Windows.MessageBox.Show("Check File or Directory !");

            Factory factory = new Factory();
            Factory.target cible = new Factory.target();
            if (FileBox.IsChecked.Value)
            {
                cible = Factory.target.FILE;
                if (DirectoryBox.IsChecked.Value)
                    cible = Factory.target.ALL;
            }
            if (DirectoryBox.IsChecked.Value)
                cible = Factory.target.DIRECTORY;
            factory.InitAndDoIt(Directory, RecursiveBox.IsChecked.Value, cible);
        }

        private void ChooseDirectory(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            Directory = dialog.SelectedPath;
        }
    }
}
