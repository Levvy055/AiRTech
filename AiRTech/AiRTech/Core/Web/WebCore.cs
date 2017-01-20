using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Views.Other;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace AiRTech.Core.Web
{
    public class WebCore
    {
        private bool _connected;
        #region Http Adresses
        private const string BaseUrl = "https://airtech.grmdev.eu/f_api/";
        //private const string BaseUrl = "http://localhost:8080/airtech_server/api/";
        private const string FnDefDir = "defs/";
        private const string FnDefs = "defs_*.json";
        private const string FnFmlsDir = "fmls/";
        private const string FnFmls = "fmls_*.json";
        private const string FnImgDir = "images/";
        private const string FnLinker = "linker.json";
        #endregion

        public WebCore()
        {
            Online();
        }

        public async Task<List<Definition>> GetDefinitionList(SubjectType subjectType)
        {
            var pathToDefs = FnDefDir+FnDefs.Replace("*", subjectType.ToString().ToLower());
            var list = await GetData<List<Definition>>(pathToDefs);
            if (list == null)
            {
                return null;
            }
            foreach (var def in list)
            {
                def.LinkDeserializedComponents(subjectType);
            }
            return list;
        }

        private async Task<T> GetData<T>(string path)
        {
            using (var client = Connection)
            {
                try
                {
                    var resp = await client.GetAsync(path);
                    resp.EnsureSuccessStatusCode();
                    var respBody = await resp.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<T>(respBody);
                    return obj;
                }
                catch (JsonException e)
                {
                    HandleWebException(e, "Błędna odpowiedź serwera! Spróbuj ponownie później.");
                }
                catch (Exception e)
                {
                    HandleWebException(e);
                }

            }
            return default(T);
        }

        public async Task<bool> Online()
        {
            if (_connected)
            {
                return _connected;
            }
            var internetStatus = CrossConnectivity.Current.IsConnected;
            if (!internetStatus)
            {
                DialogManager.ShowWarningDialog("Brak dostępu do internetu!",
                    "Włącz INTERNETY! czyt. włącz transmisje danych.");
                return false;
            }
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("http://www.google.com");
            if (!isReachable)
            {
                DialogManager.ShowWarningDialog("Brak dostępu do internetu!",
                    "Połącz się z INTERNETEM!");
                return false;
            }
            using (var client = Connection)
            {
                try
                {
                    var r = await client.GetStringAsync("index.html");
                    _connected = !string.IsNullOrWhiteSpace(r) && r.StartsWith("<!DOCTYPE html>");
                    if (!_connected)
                    {
                        DialogManager.ShowWarningDialog("Brak dostępu do serwera!",
                    "Serwer jest wyłączony, bądź nie odpowiada.");
                    }
                }
                catch (Exception e)
                {
                    HandleWebException(e);
                }
            }
            return _connected;
        }

        public void HandleWebException(Exception e, string msg = null)
        {
            Debug.WriteLine(e);
            _connected = false;
            if (msg == null)
            {
                msg = "Problem podczas próby pobrania zawartości online.";
            }
            DialogManager.ShowWarningDialog("Błąd pobierania!", msg);
        }

        private HttpClient Connection
        {
            get
            {
                var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(7),
                    BaseAddress = new Uri(BaseUrl)
                };
                httpClient.DefaultRequestHeaders.Clear();
                return httpClient;
            }
        }
    }
}
