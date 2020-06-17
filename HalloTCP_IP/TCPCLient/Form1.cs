using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPCLient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Loopback, _kportNumber);
            ConnectedEndPoint server = ConnectedEndPoint.Connect(remoteEndPoint, (c, s) => Debug.WriteLine(s));

            //_StartUserInput(server);
            server.WriteLine(textBox1.Text);
            await _SafeWaitOnServerRead(server);
        }


        private const int _kportNumber = 1;



        private void _StartUserInput(ConnectedEndPoint server)
        {
            // Get user input in a new thread, so main thread can handle waiting
            // on connection.
            new Thread(() =>
            {
                try
                {
                    string line;

                    while ((line = textBox1.Text) != "")
                    {
                        server.WriteLine(line);
                    }

                    server.Shutdown();
                }
                catch (IOException e)
                {
                    Debug.WriteLine($"Server {server.RemoteEndPoint} IOException: {e.Message}");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Unexpected server exception: {e}");
                    Environment.Exit(1);
                }
            })
            {
                // Setting IsBackground means this thread won't keep the
                // process alive. So, if the connection is closed by the server,
                // the main thread can exit and the process as a whole will still
                // be able to exit.
                IsBackground = true
            }.Start();
        }

        private static async Task _SafeWaitOnServerRead(ConnectedEndPoint server)
        {
            try
            {
                await server.ReadTask;
            }
            catch (IOException e)
            {
                Debug.WriteLine($"Server {server.RemoteEndPoint} IOException: {e.Message}");
            }
            catch (Exception e)
            {
                // Should never happen. It's a bug in this code if it does.
                Debug.WriteLine($"Unexpected server exception: {e}");
            }
        }


    }
}
