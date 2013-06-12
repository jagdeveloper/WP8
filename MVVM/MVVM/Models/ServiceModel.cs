using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVVM.Models
{
    public class ServiceModel
    {
        public event EventHandler<FlickrCamerasEventArgs> GetCamerasCompleted;

        public void GetCameras()
        {
            string uri = " http://api.flickr.com/services/rest/?method=flickr.cameras.getBrands&api_key=8c75d8ee06ebf35dfe21ddee167d2257&format=rest";

            WebClient client = new WebClient();
            client.DownloadStringCompleted += (s, a) =>
            {
                if (a.Error == null && !a.Cancelled)
                {
                    var result = a.Result;

                    //LINQ  para XML
                    var doc = XDocument.Parse(result);

                    var query = from c in doc.Descendants("brand")
                                select new Camera() { Name = c.Attribute("name").Value };

                    var results = query.ToList();

                    if (GetCamerasCompleted != null)
                    {
                        GetCamerasCompleted(this, new FlickrCamerasEventArgs(results));
                    }

                }
            };
            client.DownloadStringAsync(new Uri(uri, UriKind.Absolute));
        }
    }
}
