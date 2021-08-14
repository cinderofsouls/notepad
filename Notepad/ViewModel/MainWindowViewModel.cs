using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

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

        private string mainText = "test default text";

        public string MainText
        {
            get { return mainText; }
            set {
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

        }

        private void OpenFile()
        {

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
    }
}
