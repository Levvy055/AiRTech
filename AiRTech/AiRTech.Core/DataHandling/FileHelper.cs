using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech.Core.DataHandling
{
    public static class FileHelper
    {
        public static string GetAboslutePath(Uri uri)
        {
            string u = null;
            if (uri.IsAbsoluteUri)
            {
                u = uri.AbsolutePath;
            }
            else
            {
                Uri ur;
                var baseUri = new Uri(CoreManager.Current.FileHandler.RootAppPath() + "\\");
                if (Uri.TryCreate(baseUri, uri, out ur))
                {
                    u = ur.AbsolutePath;
                }
            }
            return u;
        }
    }
}
