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
        public RelayCommand ExitCommand { get; set; }

        public MainWindowViewModel()
        {
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
        
        private void ExitApp()
        {
            Application.Current.Shutdown();
        }
    }
}
