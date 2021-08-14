using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace Notepad
{
    class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand NewCommand { get; set; }
        public RelayCommand OpenCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand SaveAsCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }

        public MainWindowViewModel()
        {
            NewCommand = new RelayCommand(NewFile);
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            SaveAsCommand = new RelayCommand(SaveFileAs);
            ExitCommand = new RelayCommand(ExitApp);
        }

        private bool fileModified = false;
        private string filePath = "";

        private string windowTitle = "Notepad";

        public string WindowTitle
        {
            get { return windowTitle; }
            set {
                windowTitle = value;
                RaisePropertyChanged();
            }
        }


        private string mainText = "test default text";

        public string MainText
        {
            get { return mainText; }
            set
            {
                if (mainText != value)
                {
                    fileModified = true;
                    UpdateWindowTitle();
                }
                mainText = value;
                RaisePropertyChanged();
            }
        }

        private bool wordWrap = false;

        public bool WordWrap
        {
            get { return wordWrap; }
            set {
                wordWrap = value;
                RaisePropertyChanged();
            }
        }
        
        private void NewFile()
        {
            if (fileModified)
            {
                MessageBoxResult result = PromptUnsavedChanges();
                if (result == MessageBoxResult.Yes)
                {
                    SaveFile();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            MainText = "";
            filePath = "";
            fileModified = false;
            UpdateWindowTitle();
        }

        private void OpenFile()
        {
            if (fileModified)
            {
                MessageBoxResult result = PromptUnsavedChanges();
                if (result == MessageBoxResult.Yes)
                {
                    SaveFile();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            if (filePath == "")
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (dialog.ShowDialog() == true)
                    filePath = dialog.FileName;
                else
                    return;
            }
            MainText = File.ReadAllText(filePath);
            fileModified = false;
            UpdateWindowTitle();
        }

        private void SaveFile()
        {

        }

        private void SaveFileAs()
        {

        }

        private void ExitApp()
        {
            Application.Current.Shutdown();
        }

        private void UpdateWindowTitle()
        {
            if (fileModified == true && filePath == "")
            {
                WindowTitle = "Notepad* (unsaved changes)";
            }
            else if (fileModified == true && filePath != "")
            {
                WindowTitle = "Notepad | " + filePath + "* (unsaved changes)";
            }
            else if (fileModified == false && filePath != "")
            {
                WindowTitle = "Notepad | " + filePath;
            }
            else
            {
                WindowTitle = "Notepad";
            }
        }

        private MessageBoxResult PromptUnsavedChanges()
        {
            return MessageBox.Show("Would you like to save your changes?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        }
    }
}
