using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HalloAync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartOhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(1000);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(100);
                    this.Dispatcher.Invoke(() => { pb1.Value = i; /*Thread.Sleep(100);*/ });
                }
                this.Dispatcher.Invoke(() => ((Button)sender).IsEnabled = !false);
            });

        }

        private void StartTaskMitTS(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            b.IsEnabled = false;
            var ts = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(10);

                    Task.Factory.StartNew(() => { pb1.Value = i; Thread.Sleep(50); }, CancellationToken.None, TaskCreationOptions.None, ts);
                }

                Task.Factory.StartNew(() => b.IsEnabled = true, CancellationToken.None, TaskCreationOptions.None, ts); ;

            });
        }

        private void LadeVonDB(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            b.IsEnabled = false;
            var ts = TaskScheduler.FromCurrentSynchronizationContext();

            pb1.IsIndeterminate = true;
            Task.Run(() =>
            {
                var conString = "Server=(localdb)\\MSSQLLOCALDB;Database=Northwnd;Trusted_Connection=true";
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Employees";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Thread.Sleep(400);
                        var name = reader["LastName"].ToString();
                        Task.Factory.StartNew(() =>
                        {
                            lb.Items.Add(name);
                        }, CancellationToken.None, TaskCreationOptions.None, ts);
                    }
                }


                Task.Factory.StartNew(() => { pb1.IsIndeterminate = false; pb1.Value = 100; }, CancellationToken.None, TaskCreationOptions.None, ts);
                Task.Factory.StartNew(() => b.IsEnabled = true, CancellationToken.None, TaskCreationOptions.None, ts);

            });
        }
    }
}
