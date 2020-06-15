using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var client =new  ServiceReference1.BLZServicePortTypeClient("BLZServiceSOAP12port_http");
            var result = client.getBank("67290100");

            label1.Text = result.bezeichnung;
        }
    }
}
