using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static bool fileModified = false;
        public static string filepath = null;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new CommandContext()
            {
                objForWrapKey = this.txtBox,
                objForWrapKey2 = this.MenuItemWrap,
                objForSaveAsKey = this.txtBox,
                objForSaveAsKey2 = this.MainWindowName
            };
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CommandBindingNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBindingNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New MenuItem Clicked!");
        }

        private void CommandBindingOpen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBindingOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Stream st;
            OpenFileDialog d1 = new OpenFileDialog();
            d1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (d1.ShowDialog() == true)
            {
                txtBox.Text = File.ReadAllText(d1.FileName);
                //MainWindowName.Title = "Notepad | " + d1.SafeFileName;
                MainWindowName.Title = "Notepad | " + d1.FileName;
                fileModified = false;
                filepath = d1.FileName;
            }
        }

        private void CommandBindingSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBindingSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (filepath == null)
            {
                Stream st;
                SaveFileDialog d1 = new SaveFileDialog();
                if (d1.ShowDialog() == true)
                {
                    File.WriteAllText(d1.FileName, txtBox.Text);
                    MainWindowName.Title = "Notepad | " + d1.FileName;
                    fileModified = false;
                    filepath = d1.FileName;
                }
            }
            else
            {
                File.WriteAllText(filepath, txtBox.Text);
                MainWindowName.Title = "Notepad | " + filepath;
                fileModified = false;
            }
        }

        private void txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fileModified == false)
            {
                fileModified = true;
                MainWindowName.Title = MainWindowName.Title + "* (unsaved changes)";
            }
        }
    }
}
