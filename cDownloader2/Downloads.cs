using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cDownloader2
{
    class Downloads
    {
        //Change to boolean later
        public void startDownload(string url)
        {
            Uri pingConnection = new Uri("https://www.apple.com");
            WebRequest request = WebRequest.Create(pingConnection);
            WebResponse response = request.GetResponse();

            Stream objStream = response.GetResponseStream();
            StreamReader objReader = new StreamReader(objStream, true);

            string finalResponse = objReader.ReadToEnd();
            Debug.Print(finalResponse);
        }
    }
}
