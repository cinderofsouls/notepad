using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;

namespace notepad
{
    public class ExitKey : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //MessageBox.Show("Ctrl+Shift+X");
            Application.Current.Shutdown();
        }
    }

    public class WrapKey : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public object UIElement { get; set; }
        public object UIElement2 { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ((TextBox)UIElement).TextWrapping = ((TextBox)UIElement).TextWrapping == TextWrapping.Wrap ? TextWrapping.NoWrap : TextWrapping.Wrap;
            ((MenuItem)UIElement2).IsChecked = ((MenuItem)UIElement2).IsChecked == true ? false : true;
        }
    }

    public class SaveAsKey : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public object UIElement { get; set; }
        public object UIElement2 { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Stream st;
            SaveFileDialog d1 = new SaveFileDialog();
            if (d1.ShowDialog() == true)
            {
                File.WriteAllText(d1.FileName, ((TextBox)UIElement).Text);
                MainWindow.filepath = d1.FileName;
                ((MainWindow)UIElement2).Title = "Notepad | " + d1.FileName;
                MainWindow.fileModified = false;
            }
        }
    }

    public class CommandContext
    {
        public object objForWrapKey { get; set; }
        public object objForWrapKey2 { get; set; }
        public object objForSaveAsKey { get; set; }
        public object objForSaveAsKey2 { get; set; }
        public ICommand ExitCommand
        {
            get
            {
                return new ExitKey();
            }
        }

        public ICommand WrapCommand
        {
            get
            {
                return new WrapKey()
                {
                    UIElement = objForWrapKey,
                    UIElement2 = objForWrapKey2
                };
            }
        }

        public ICommand SaveAsCommand
        {
            get
            {
                return new SaveAsKey()
                {
                    UIElement = objForSaveAsKey,
                    UIElement2 = objForSaveAsKey2
                };
            }
        }
    }
}
