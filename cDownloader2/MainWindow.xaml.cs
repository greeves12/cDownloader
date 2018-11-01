using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;

namespace cDownloader2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebClient client = new WebClient();
        string location;
        double bytes;
        public MainWindow()
        {
            InitializeComponent();
           
        }

       private void startDownload(string url, string location)

        {
            HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string disposition = response.Headers["Content-Disposition"];
            string filename = disposition.Substring(disposition.IndexOf("filename=") + 10).Replace("\"", "");
            Dir.Text = filename;
            response.Close();

            

            using (var client = new WebClient())
            {
                /*  var request = WebRequest.Create(url);
                  var response = request.GetResponse();
                  var contentDisposition = response.Headers["Content-Disposition"];
                  const string contentFileNamePortion = "filename=";
                  var fileNameStartIndex = contentDisposition.IndexOf(contentFileNamePortion, StringComparison.InvariantCulture) + contentFileNamePortion.Length;
                  var originalFileNameLength = contentDisposition.Length - fileNameStartIndex;
                  var originalFileName = contentDisposition.Substring(fileNameStartIndex, originalFileNameLength);
                  client.UseDefaultCredentials = true;
                  */


                /*client.DownloadFileAsync(new Uri(url), location + Path.DirectorySeparatorChar + filename);
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadChange);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadComplete);*/
                
                

            }
        }
        private void downloadChange(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = (bytesIn / totalBytes) * 100;
            Progress.Value = int.Parse(Math.Truncate(percentage).ToString());

            doMath(bytesIn);

            DownloadButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            Folder.IsEnabled = false;
            URLBox.IsReadOnly = true;
        }

        private void downloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            if(e.Cancelled == false)
            {
                Status.Text = "Status: Download Finished";
            }
            else
            {
                Status.Text = "Status: Download Cancelled";
            }
            URLBox.IsReadOnly = false;
            Progress.Value = 0;
            CancelButton.IsEnabled = false;
            Folder.IsEnabled = true;
            DownloadButton.IsEnabled = true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
                 
               

                if(saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    location = saveFileDialog.SelectedPath;
                    Dir.Text = location;
                }
            }
            catch 
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(URLBox.Text != string.Empty)
            {
               
              
                    if (location != string.Empty)
                    {
                    Status.Text = "Status: Downloading...";
                        startDownload(URLBox.Text, location);
                    }
                
                
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            client.CancelAsync();
            
        }

        void doMath(double number)
        {
            if(number <= 1024)
            {
                bytes = number;
            }else if(number > 1024)
            {
                bytes = number / 1024;
            }
        }
    }
}
