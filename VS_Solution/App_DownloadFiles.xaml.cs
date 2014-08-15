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
using System.Windows.Shapes;
using System.Net;
using System.Windows.Threading;

namespace VS_Solution
{
    /// <summary>
    /// Interaction logic for App_DownloadFiles.xaml
    /// </summary>
    public partial class App_DownloadFiles : Window
    {
        Queue<AD> fr = new Queue<AD>();
        int from;
        int to;
        int count;
        string OU;
        string name;

        public App_DownloadFiles()
        {
            InitializeComponent();
            
        }

        private void UI_Start_Click(object sender, RoutedEventArgs e)
        {
            string url = UI_Url.Text;
            string replace = UI_Replace.Text;
            from = Convert.ToInt32(UI_From.Text);
            to = Convert.ToInt32(UI_To.Text);
            OU = UI_Out.Text;
            name = UI_Name.Text;
            for (int i = from; i < to + 1; i++)
            {
                fr.Enqueue(new AD(url.Replace(replace,i.ToString()),i));
            }
            Asyncdownload();
        }

        private void Asyncdownload()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                UI_Done.Content = count + " of " + (to - from + 1) + " Completed";
            }));
            if (count < to - from + 1)
            {
                WebClient client = new WebClient();
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    UI_PB.Value = 0;
                }));
                AD item = fr.Dequeue();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadComplete);
                client.DownloadFileAsync(new Uri(item.Url),OU + @"\" + name + " " + item.Number + System.IO.Path.GetExtension(item.Url));
            }
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            int pgdone = e.ProgressPercentage;
            Application.Current.Dispatcher.Invoke(new Action(() => 
            {
                UI_PB.Value = pgdone;
            }));
        }

        private void DownloadComplete(object sender,System.ComponentModel.AsyncCompletedEventArgs e)
        {
            count++;
            Asyncdownload();
        }
    }
}
