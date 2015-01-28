using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;
using System.Xml.Serialization;

namespace ClearFileNameWPF
{
    public enum target { FILE = 1, DIRECTORY = 2, ALL = 0 };

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<string> list_extension;
        private string _directory;
        private string _extension;
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
        public string Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
                this.OnPropertyChanged("Extension");
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
            Extension = "";
            if (File.Exists("LastPath.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(string));
                using (StreamReader rd = new StreamReader("LastPath.xml"))
                {
                    Directory = xs.Deserialize(rd) as string;
                }
            }
            if (File.Exists("Extension.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<string>));
                using (StreamReader rd = new StreamReader("Extension.xml"))
                {
                    list_extension = new ObservableCollection<string>(xs.Deserialize(rd) as ObservableCollection<string>);
                }
            }
            list_extension = new ObservableCollection<string>();
            listExtension.ItemsSource = list_extension;
            this.DataContext = this;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(string));
            using (StreamWriter wr = new StreamWriter("LastPath.xml"))
            {
                xs.Serialize(wr, Directory);
            }
            xs = new XmlSerializer(typeof(ObservableCollection<String>));
            using (StreamWriter wr = new StreamWriter("Extension.xml"))
            {
                xs.Serialize(wr, list_extension);
            }
        }

        private void AddExtension(object sender, RoutedEventArgs e)
        {
            list_extension.Add(Extension);
        }

        private void RemoveExtension(object sender, RoutedEventArgs e)
        {
            int pos = listExtension.SelectedIndex;
            if (pos != -1)
            {
                list_extension.RemoveAt(pos);
            }
        }
        
        private void ChooseDirectory(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = Directory;
            dialog.ShowDialog();
            Directory = dialog.SelectedPath;
        }

        private void OrderExtension(object sender, RoutedEventArgs e)
        {
            list_extension = new ObservableCollection<string>(list_extension.OrderBy(i =>i));
        }

//FAIRE ASYNC TASK ET LOGFILE

        private async void DoRemove(object sender, RoutedEventArgs e)
        {
            string ret = await gotoremove();
            if (ret != "\n")
            {
                System.Windows.Forms.MessageBox.Show("Error detected during the action of removing files !\n Check Log file !");

            }
        }

        private async Task<string> gotoremove()
        {
            FactoryRemove remove = new FactoryRemove(Directory, RecursiveBoxRemove.IsChecked.Value, new List<string>(list_extension));
            remove.Run();
            return ret ;
        }
    }
}
