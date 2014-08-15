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

namespace VS_Solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UI_SelectSolution.Items.Add("Download multiple files of site or server");

        }

        private void UI_OpenSelected_Click(object sender, RoutedEventArgs e)
        {
            string Selected = UI_SelectSolution.SelectedItem.ToString();
            if (Selected == "Download multiple files of site or server")
            {
                App_DownloadFiles app_df = new App_DownloadFiles();
                app_df.Show();

            }
        }
    }
}
