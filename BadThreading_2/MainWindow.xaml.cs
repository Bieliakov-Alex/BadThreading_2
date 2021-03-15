using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BadThreading_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private delegate void OneArgDelegate(String arg);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowMessage()
        {
            HelloTextBlock.Text = "Job Done!";
        }

        private void DoJobClick(object sender, RoutedEventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            //System.Threading.Thread my_thread = new System.Threading.Thread(new System.Threading.ThreadStart(ShowMessage));
            //my_thread.Start();

            /*BeginInvoke*/
            //Приоритеты: https://docs.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherpriority?view=net-5.0
            //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new OneArgDelegate(SetResultTextBlock), "Done");


            /*SynchronizationContext*/
            var sync_context = System.Threading.SynchronizationContext.Current;

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                sync_context.Post(delegate
                {
                    HelloTextBlock.Text = "Done!";
                }, null);
            });
            /*System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                HelloTextBlock.Text = "Done!";
            });*/

            //Dispatcher.CheckAccess() - возвращает false, если находится не в основном UI-потоке.
        }

        /*private void SetResultTextBlock(String s)
        {
            HelloTextBlock.Text = s;
        }*/
    }
}
