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
using System.Windows.Forms;

namespace cDownloader2
{
    /// <summary>
    /// Interaction logic for LinksWindow.xaml
    /// </summary>
    
    public partial class LinksWindow : Window
    {
        public LinksWindow()
        {
            InitializeComponent();
        }
        
        private string path;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(URLbox.Text.Length == 0)
            {
                System.Windows.MessageBox.Show("You must enter a URL");
                return;
            }
            Downloads downloads = new Downloads();
            string url = URLbox.Text;
            downloads.pingWebsite(url,path);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            FolderBrowserDialog browser = new FolderBrowserDialog();
            if(browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = browser.SelectedPath;
            }
        }

        private void URLbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
