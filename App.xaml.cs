using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace notepad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private void AppStartup(object sender, StartupEventArgs e)
        //{
        //    MainWindow wnd = new MainWindow();
        //    if (e.Args.Length == 1)
        //    {
        //        wnd.Title = "Notepad | " + e;
        //        //wnd.txtBox.Text = 
        //        File.WriteAllText(e.Args[0], wnd.txtBox.Text);
        //    }
        //    MessageBox.Show(e.Args[0]);
        //    wnd.Show();
        //}
        public string fileName { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length == 1)
            {
                this.fileName = e.Args[0];
            }
            
            base.OnStartup(e);
        }
    }
}
