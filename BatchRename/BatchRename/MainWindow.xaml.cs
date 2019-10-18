using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //hộp thoại chọn file add vào list
        OpenFileDialog openFileDialog = new OpenFileDialog();

        class File
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }

        BindingList<File> listFiles = new BindingList<File>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            filesListView.ItemsSource = listFiles;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //set tính năng chọn nhiều files
            openFileDialog.Multiselect = true;
            //đường dẫn đầu tiên mà hộp thoại hiện lên
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
       
            if (openFileDialog.ShowDialog() == true)
            {
                //add từng tên file vào trong listFiles
                foreach (string fullFileName in openFileDialog.FileNames)
                {
                    //tạo 1 file mới
                    File file = new BatchRename.MainWindow.File();
                    //tách fullFileName thành name và path
                    file.Name = System.IO.Path.GetFileName(fullFileName);
                    file.Path = System.IO.Path.GetDirectoryName(fullFileName);
                    //add file mới tạo vào list
                    listFiles.Add(file);
                }
                    
            }
        }

        /*        private void startButton_Click(object sender, RoutedEventArgs e)
                {
                    foreach(var file in listFiles)
                    {
                        //File.Move(fileName, "b.txt");
                    }
                }
         */
    }

}
