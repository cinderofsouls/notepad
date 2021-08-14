using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Notepad
{
    class MainWindowViewModel : ViewModelBase
    {
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


    }
}
