using System.ComponentModel;
using System.Net;
using System.Windows;

namespace cDownloader2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebClient client = new WebClient();
        string location;
        public MainWindow()
        {
            InitializeComponent();
           
        }

       private void startDownload(string url, string location)

        {
            
            using (var client = new WebClient())
            {

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadChange);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadComplete);
                client.DownloadFileAsync(new System.Uri(url), location);

            }
        }
        private void downloadChange(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress.Value = e.ProgressPercentage;
            DownloadButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            Folder.IsEnabled = false;
            URLBox.IsReadOnly = true;
        }

        private void downloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            if(e.Cancelled == false)
            {
                DownloadBox.Text = "Status: Download Finished";
            }
            else
            {
                DownloadBox.Text = "Status: Download Cancelled";
            }
            URLBox.IsReadOnly = false;
            Progress.Value = 0;
            CancelButton.IsEnabled = false;
            Folder.IsEnabled = true;
            DownloadButton.IsEnabled = true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog browser = new System.Windows.Forms.FolderBrowserDialog();
            
            if(browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                location = browser.SelectedPath;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(URLBox.Text != "Insert URL")
            {
                var ping = new System.Net.NetworkInformation.Ping();

                var result = ping.Send(URLBox.Text);

                if(result.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    if (location != null)
                    {
                        startDownload(URLBox.Text, location);
                    }
                }
                else
                {
                    DownloadBox.Text = "Website is down, exit imminent";
                }
            }
        }

        
    }
}
