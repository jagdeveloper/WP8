using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models
{
    public class FlickrCamerasEventArgs : EventArgs
    {
        public IList<Camera> Results { get; private set; }

        public FlickrCamerasEventArgs(IList<Camera> results)
        {
            this.Results = results;
        }
    }
}
