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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Downloads downloads = new Downloads();
            downloads.startDownload("");
        }
    }
}
