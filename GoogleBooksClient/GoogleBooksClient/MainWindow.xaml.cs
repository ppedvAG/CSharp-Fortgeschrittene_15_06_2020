using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Xml.Serialization;

namespace GoogleBooksClient
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

        private async void Search(object sender, RoutedEventArgs e)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={tb1.Text}";

            var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            jsonTb.Text = json;

            //var results = JsonConvert.DeserializeObject<BookResults>(json);

            var results = System.Text.Json.JsonSerializer.Deserialize<BookResults>(json); //ab core 3.0

            myGrid.ItemsSource = results.items.Select(x => x.volumeInfo).ToList();
        }

        private void SaveAsJSON(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog() { Filter = "JSON Datei|*.json|Alle Dateien|*.*" };

            if (dlg.ShowDialog().Value)
            {
                var json = JsonConvert.SerializeObject(myGrid.ItemsSource);
                File.WriteAllText(dlg.FileName, json);
            }

        }

        private void OpenJSON(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Filter = "JSON Datei|*.json|Alle Dateien|*.*" };
            if (dlg.ShowDialog().Value)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<Volumeinfo>>(File.ReadAllText(dlg.FileName));
                myGrid.ItemsSource = result;
            }
        }

        private void SaveAsXML(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog() { Filter = "XML Datei|*.xml|Alle Dateien|*.*" };

            if (dlg.ShowDialog().Value)
            {
                using (var sw = new StreamWriter(dlg.FileName))
                {
                    var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                    serial.Serialize(sw, myGrid.ItemsSource);
                }
            }
        }

        private void OpenXML(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Filter = "XML Datei|*.xml|Alle Dateien|*.*" };

            if (dlg.ShowDialog().Value)
            {
                using (var sr = new StreamReader(dlg.FileName))
                {
                    var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                    myGrid.ItemsSource = (List<Volumeinfo>)serial.Deserialize(sr);
                }
            }
        }

        private void SaveAsBin(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog() { Filter = "DAT Datei|*.dat|Alle Dateien|*.*" };

            if (dlg.ShowDialog().Value)
            {
                using (var sw = new FileStream(dlg.FileName, FileMode.Create))
                {
                    var serial = new BinaryFormatter();
                    serial.Serialize(sw, myGrid.ItemsSource);
                }
            }
        }

        private void OpenDat(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Filter = "DAT Datei|*.dat|Alle Dateien|*.*" };

            if (dlg.ShowDialog().Value)
            {
                using (var sr = new FileStream(dlg.FileName, FileMode.Open))
                {
                    var serial = new BinaryFormatter();
                    myGrid.ItemsSource = (IEnumerable<Volumeinfo>)serial.Deserialize(sr);
                }
            }
        }

        private void SerTupel(object sender, RoutedEventArgs e)
        {
            List<Tuple<string, DateTime, int, Tuple<string, string>>> dataNein = new List<Tuple<string, DateTime, int, Tuple<string, string>>>();//nein
            List<DateTime> data = new List<DateTime>();
            data.Add(DateTime.Now.AddDays(0));
            data.Add(DateTime.Now.AddDays(1));
            data.Add(DateTime.Now.AddDays(2));
            data.Add(DateTime.Now.AddDays(3));
            var query = from d in data
                        where d.Day > 15
                        orderby d.Month
                        select new { Tag = d.Day, Jahr = d.Year, Monat = d.Month, text = "Der steht immer hier" };


            string ausgabeTage = string.Join(", ", data.OrderByDescending(x => x.Day).Select(x => x.ToString("dddd")));


            //var t = (12, "lalal", DateTime.Now);
            var t = (z: 12, text: "lalal", Heute: DateTime.Now);
            var anoClass = new { Zahl = 12, Text = "lala", Heute = DateTime.Now };

            //var json = System.Text.Json.JsonSerializer.Serialize(t);
            var json = System.Text.Json.JsonSerializer.Serialize(anoClass);
            File.WriteAllText("tupel.json", json);
        }
    }
}
