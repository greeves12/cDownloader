using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;

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
         
                
                string filename = System.IO.Path.GetFileName(url);

               
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadChange);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadComplete);
                client.DownloadFileAsync(new Uri(url), location + Path.DirectorySeparatorChar + filename);

                DownloadButton.IsEnabled = false;
                Cancel.IsEnabled = true;
                Folder.IsEnabled = false;
                URLBox.IsReadOnly = true;
            
        }
        private void downloadChange(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = (bytesIn / totalBytes) * 100;
            Progress.Value = int.Parse(Math.Truncate(percentage).ToString());

            doMath(bytesIn);

            
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
            Cancel.IsEnabled = false;
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


        void doMath(double number)
        {
            double nums;
            if(number <= 1024)
            {
                nums = number;
                Math.Round(nums, 2);
            }else if(number > 1024)
            {
                //kb
                nums = number / 1024;
                Math.Round(nums, 2);
            }else if(number > 1048576)
            {
                //Mb
               nums= number / 1048576;
                Math.Round(nums, 2);
            }else if(number > 1073741824)
            {
                //Gb
                nums = number / 1073741824;
                Math.Round(nums, 2);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            client.CancelAsync();
            client.Dispose();
        }
    }
}
