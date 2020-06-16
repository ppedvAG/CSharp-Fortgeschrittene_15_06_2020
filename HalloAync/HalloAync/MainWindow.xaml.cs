using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(10);

                    if (cts.IsCancellationRequested)
                        break; //close / cleanup

                    //  if (i > 57)
                    //      throw new ExecutionEngineException();

                    Task.Factory.StartNew(() => { pb1.Value = i; }, CancellationToken.None, TaskCreationOptions.None, ts);
                }

                Task.Factory.StartNew(() => b.IsEnabled = true, CancellationToken.None, TaskCreationOptions.None, ts); ;

            }).ContinueWith(t => MessageBox.Show($"Fehler: {t.Exception.InnerException.Message}"), TaskContinuationOptions.OnlyOnFaulted);
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

        CancellationTokenSource cts = null;

        private void Abbrechen(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        private async void StartAsyncAwait(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    pb1.Value = i;

                    await Task.Delay(300, cts.Token);

                    if (cts.IsCancellationRequested)
                        break; //close / cleanup
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Es wurde erfolreich abgebrochen");
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Fehler: {ex.Message}");
            }
        }

        private async void StartAsyncAwaitDB(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            b.IsEnabled = false;

            var conString = "Server=(localdb)\\MSSQLLOCALDB;Database=Northwnd;Trusted_Connection=true";

            try
            {
                using (var con = new SqlConnection(conString))
                {
                    Task.Run(() => Thread.Sleep(5000)).ContinueWith(t => MessageBox.Show(":-)"));

                    await con.OpenAsync();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM Employees;WAITFOR DELAY '00:00:10';";
                        var result = await cmd.ExecuteScalarAsync();
                        MessageBox.Show($"{result} Employees in DB");
                    }

                }//con.Dispose(); //-> con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            b.IsEnabled = !false;

        }

        private async void StartOldAndSlow(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Value: {await GetValueFromOldAndSlowAsync(12)}");
        }

        public Task<long> GetValueFromOldAndSlowAsync(int input)
        {
            return Task.Run(() => GetValueFromOldAndSlow(input));
        }

        public long GetValueFromOldAndSlow(int input)
        {
            Thread.Sleep(5000);
            return 78934567834578934;
        }
    }
}
