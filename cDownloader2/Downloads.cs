using System.IO;
using System.Net;

namespace cDownloader2
{
    class Downloads
    {
        //Change to boolean later
        public void pingWebsite(string url, string path)
        {
            var ping = new System.Net.NetworkInformation.Ping();

            var result = ping.Send(url);

            if(result.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                startDownload(url,path);
            }
        }


        public void startDownload(string url, string path)
        {
            using(WebClient wc = new WebClient())
            {
                var data = wc.DownloadData(url);
                
                if (!string.IsNullOrEmpty(wc.ResponseHeaders["Content-Disposition"]))
                {
                    wc.DownloadFileAsync(new System.Uri(url), path + "\"" + wc.ResponseHeaders["Content-Disposition"].Substring(wc.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 9).Replace("\"", ""));
                    
                }

                
                
            }

            byte[] size = new WebClient().DownloadData(url);
        }
    }
}
