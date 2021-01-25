using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TreeViewFileExplorerLibrary;
using TreeViewFileExplorerLibrary.Models;
using TreeViewFileExplorerUI.ViewModels;

namespace TreeViewFileExplorerUI.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        private readonly FileSystemReader _fileSystemReader;
        public ShellView()
        {
            InitializeComponent();
            _fileSystemReader = new FileSystemReader();
        }

        private void Pusk_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            DriveInfo[] drives = DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Fixed).ToArray();
            var hardDrives = new ShellViewModel();

            foreach (var hardDrive in drives)
            {
                var driveInfo = new FileTreeItem
                {
                    Name = hardDrive.Name,
                    SubTrees = _fileSystemReader.GetRootTreeInfo(hardDrive.Name)
                };

                hardDrives.FileSystem.Add(driveInfo);
            }

            WindowTreeView.ItemsSource = hardDrives.FileSystem;
            ((Button)sender).IsEnabled = true;


        }
    }
}
