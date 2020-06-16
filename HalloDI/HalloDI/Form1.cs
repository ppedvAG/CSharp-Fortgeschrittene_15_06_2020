using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloDI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.DataBindings.Add("Text", trackBar1, "Value", true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MachLaut(trackBar1.Value, new ConsolenBeeper());
        }

        public void MachLaut(int value, ISoundDevice soundDevice)
        {
            if (value < 50)
                throw new ArgumentException("Wenig als 50 Hz geht nicht");

            if (value > 2000)
                throw new ArgumentException("Mehr als 2000 Hz geht nicht");

            soundDevice.MakeBeep(value, 700);
            soundDevice.MakeBeep(value, 700);

            //Console.Beep(value, 700);
            //Console.Beep(value, 700);
        }
    }

    public interface ISoundDevice
    {
        void MakeBeep(int hz, int dur);
    }

    public class ConsolenBeeper : ISoundDevice
    {
        public void MakeBeep(int hz, int dur)
        {
            Console.Beep(hz, dur);
        }
    }
}
