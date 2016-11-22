using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AiRTech.Core.Web
{
    public class WebCore
    {
        private const string BaseUrl = "https://airtech.grmdev.eu/";

        public WebCore()
        {

        }

        private async Task<bool> IsConnected()
        {
            var r=await Client.GetStringAsync("status");
            if (string.IsNullOrWhiteSpace(r))
            {
                return false;
            }
            return r.Equals("Working");
        }

        public bool Connected => IsConnected().Result;

        public HttpClient Client
        {
            get
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri(BaseUrl)
                };
                return client;
            }
        }
    }
}
