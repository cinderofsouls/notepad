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
            //MessageBox.Show("New MenuItem Clicked!");
            if (fileModified == true)
            {
                if (MessageBox.Show("Save changes before creating new file?", "Unsaved Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (filepath == null)
                    {
                        Stream st;
                        SaveFileDialog d1 = new SaveFileDialog();
                        d1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
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

                    txtBox.Text = "";
                    MainWindowName.Title = "Notepad";
                    filepath = null;
                    fileModified = false;
                }
                else
                {
                    txtBox.Text = "";
                    MainWindowName.Title = "Notepad";
                    filepath = null;
                    fileModified = false;
                }
            }
            else
            {
                txtBox.Text = "";
                MainWindowName.Title = "Notepad";
                filepath = null;
                fileModified = false;
            }
        }

        private void CommandBindingOpen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBindingOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (fileModified == true)
            {
                if (MessageBox.Show("Save changes before opening file?", "Unsaved Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (filepath == null)
                    {
                        Stream st;
                        SaveFileDialog d1 = new SaveFileDialog();
                        d1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
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
                    OpenFileDialog d2 = new OpenFileDialog();
                    d2.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    if (d2.ShowDialog() == true)
                    {
                        txtBox.Text = File.ReadAllText(d2.FileName);
                        MainWindowName.Title = "Notepad | " + d2.FileName;
                        fileModified = false;
                        filepath = d2.FileName;
                    }
                }
                else
                {
                    OpenFileDialog d2 = new OpenFileDialog();
                    d2.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    if (d2.ShowDialog() == true)
                    {
                        txtBox.Text = File.ReadAllText(d2.FileName);
                        MainWindowName.Title = "Notepad | " + d2.FileName;
                        fileModified = false;
                        filepath = d2.FileName;
                    }
                }
            }
            else
            {
                OpenFileDialog d2 = new OpenFileDialog();
                d2.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (d2.ShowDialog() == true)
                {
                    txtBox.Text = File.ReadAllText(d2.FileName);
                    MainWindowName.Title = "Notepad | " + d2.FileName;
                    fileModified = false;
                    filepath = d2.FileName;
                }
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
                d1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
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

        protected override void OnClosing(CancelEventArgs e)
        {
            //if (MessageBox.Show("confirm?", "Unsaved Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            //{
            //    e.Cancel = true;
            //}
            if (fileModified == true)
            {
                var msgResult = MessageBox.Show("Save changes before closing?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (msgResult == MessageBoxResult.Yes)
                {
                    if (filepath == null)
                    {
                        Stream st;
                        SaveFileDialog d1 = new SaveFileDialog();
                        d1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
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
                else if (msgResult == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
                base.OnClosing(e);
        }

        private void txtBox_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1");
            if ((Application.Current as App).fileName != null)
            {
                MessageBox.Show("2");
                MessageBox.Show((Application.Current as App).fileName);
                MessageBox.Show(File.ReadAllText((Application.Current as App).fileName));
                txtBox.Text = File.ReadAllText((Application.Current as App).fileName);
            }
        }
    }
}
