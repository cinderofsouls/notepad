using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            Console.WriteLine(e);
            Application.Current.Resources.Add("CommandLineArguments", e);
            MainWindowViewModel vm = (MainWindowViewModel)wnd.DataContext;
            vm.OpenStartupFile();
            wnd.Show();
        }
    }
}
