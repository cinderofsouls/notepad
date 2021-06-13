using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

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

    public class CommandContext
    {
        public object objForWrapKey { get; set; }
        public object objForWrapKey2 { get; set; }
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
    }
}
