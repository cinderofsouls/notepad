using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notepad.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        bool fileModified = false;

        private string textData;
        public string TextData
        {
            get { return textData; }
            set {
                if (textData != value)
                {
                    textData = value;
                    fileModified = true;
                }
            }
        }

        private string originalWindowTitle = "Notepad";
        private string windowTitle = null;

        public string WindowTitle
        {
            get {
                return windowTitle;
            }

            set
            {
                if (fileModified == false && value == null)
                {
                    windowTitle = originalWindowTitle;
                }
                else if (fileModified == true && value == null)
                {
                    windowTitle = originalWindowTitle + "* (unsaved changes)";
                }
                else if (fileModified == false && value != null)
                {
                    windowTitle = originalWindowTitle + " | " + value;
                }
                else if (fileModified == true && value != null)
                {
                    windowTitle = originalWindowTitle + " | " + value + "* (unsaved changes)";
                }
                else
                {
                    windowTitle = originalWindowTitle;
                }
                OnPropertyRaised("WindowTitle");
            }
        }

        public string FileName { get; set; }

        private ICommand _messageBoxTestCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand MessageBoxTestCommand
        {
            get
            {
                return _messageBoxTestCommand ?? (_messageBoxTestCommand = new CommandHandler(() => MessageBoxTest(), true));
            }
        }

        public void MessageBoxTest()
        {
            MessageBox.Show("thing working");
        }
    }
}
