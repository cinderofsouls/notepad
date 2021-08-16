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
        public RelayCommand ToggleWrapCommand { get; set; }

        private bool fileModified = false;
        private string filePath = "";

        public MainWindowViewModel()
        {
            NewCommand = new RelayCommand(NewFile);
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            SaveAsCommand = new RelayCommand(SaveFileAs);
            ExitCommand = new RelayCommand(ExitApp);

            ToggleWrapCommand = new RelayCommand(ToggleWrap);
        }

        public void OpenStartupFile()
        {
            StartupEventArgs e = (StartupEventArgs)Application.Current.Resources["CommandLineArguments"];
            if (e == null)
            {
                return;
            }
            if (e.Args.Length == 1)
            {
                if (e.Args[0] != "")
                {
                    filePath = e.Args[0];
                    OpenFile(filePath);
                }
            }
        }

        private string windowTitle = "Notepad";

        public string WindowTitle
        {
            get { return windowTitle; }
            set {
                windowTitle = value;
                RaisePropertyChanged();
            }
        }


        private string mainText = "";

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

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
                filePath = dialog.FileName;
            else
                return;
            
            MainText = File.ReadAllText(filePath);
            fileModified = false;
            UpdateWindowTitle();
        }

        private void OpenFile(string path)
        {
            MainText = File.ReadAllText(path);
            fileModified = false;
            UpdateWindowTitle();
        }

        private void SaveFile()
        {
            if (filePath == "")
            {
                SaveFileAs();
                return;
            }

            File.WriteAllText(filePath, MainText);
            fileModified = false;
            UpdateWindowTitle();
        }

        private void SaveFileAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dialog.ShowDialog() == false)
                return;

            filePath = dialog.FileName;
            File.WriteAllText(filePath, MainText);
            fileModified = false;
            UpdateWindowTitle();
        }

        private void ExitApp()
        {
            Application.Current.Shutdown();
        }

        private void ToggleWrap()
        {
            WordWrap = !WordWrap;
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

        public bool TryClose()
        {
            if (fileModified)
            {
                MessageBoxResult result = PromptUnsavedChanges();
                if (result == MessageBoxResult.No) 
                {
                    return true;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    SaveFile();
                    MessageBox.Show(filePath + " saved.", "Info", MessageBoxButton.OK, MessageBoxImage.None);
                    return true;
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        private MessageBoxResult PromptUnsavedChanges()
        {
            return MessageBox.Show("Would you like to save your changes?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        }
    }
}
