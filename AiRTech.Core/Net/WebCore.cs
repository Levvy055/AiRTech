using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AiRTech.Core.Subjects;
using AiRTech.Core.Subjects.Def;
using AiRTech.Core.Subjects.Formul;
using Newtonsoft.Json;
using System.Net.Http;
using Plugin.Connectivity;

namespace AiRTech.Core.Net
{
    public class WebCore
    {
        private bool _connected;
        #region Http Adresses
        public const string BaseUrl = "https://airtech.grmdev.eu/f_api/";
        //public const string BaseUrl = "http://localhost:8080/airtech_server/api/";
        public const string FnDefDir = "defs";
        public const string FnDefs = "defs_*.json";
        public const string FnFmlsDir = "fmls/";
        public const string FnFmls = "fmls_*.json";
        public const string FnImgDir = "images";
        public const string FnLinker = "linker.json";
        #endregion

        public WebCore()
        {
            var t=Online();
        }

        public async Task<List<Definition>> GetDefinitionList(SubjectType subjectType)
        {
            var pathToDefs = Path.Combine(FnDefDir, FnDefs.Replace("*", subjectType.ToString().ToLower()));
            var list = await GetData<List<Definition>>(pathToDefs);
            return list;
        }

        public async Task<List<Formula>> GetFormulaList(SubjectType subjectType)
        {
            var pathToFmls = Path.Combine(FnFmlsDir, FnFmls.Replace("*", subjectType.ToString().ToLower()));
            var list = await GetData<List<Formula>>(pathToFmls);
            if (list == null)
            {
                return null;
            }
            foreach (var f in list)
            {
                f.LinkDeserializedComponents(subjectType);
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
                    if (resp.StatusCode == HttpStatusCode.OK
                        || resp.StatusCode == HttpStatusCode.NotModified
                        || resp.StatusCode == HttpStatusCode.Found)
                    {
                        var respBody = await resp.Content.ReadAsStringAsync();
                        var obj = JsonConvert.DeserializeObject<T>(respBody);
                        return obj;
                    }
                }
                catch (JsonSerializationException e)
                {
                    HandleWebException(e, "Błędna odpowiedź serwera! Spróbuj ponownie później.");
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

        public async Task<bool> GetImage(string path, Stream fileStream)
        {
            using (var client = Connection)
            {
                try
                {
                    var resp = await client.GetAsync(path).ConfigureAwait(false);
                    //resp.EnsureSuccessStatusCode();
                    if (resp.StatusCode == HttpStatusCode.OK
                        || resp.StatusCode == HttpStatusCode.NotModified
                        || resp.StatusCode == HttpStatusCode.Found)
                    {
                        await resp.Content.CopyToAsync(fileStream);
                        return true;
                    }
                }
                catch (HttpRequestException)
                {
                }
                return false;
            }
        }

        public async Task<bool> Online()
        {
            if (_connected)
            {
                return _connected;
            }
            try
            {
                var internetStatus = CrossConnectivity.Current.IsConnected;
                if (!internetStatus)
                {
                    CoreManager.Current.App.DialogManager.ShowWarningDialog("Brak dostępu do internetu!",
                        "Włącz INTERNETY! czyt. włącz transmisje danych.");
                    return false;
                }
                var isReachable = await CrossConnectivity.Current.IsRemoteReachable("http://www.google.com");
                if (!isReachable)
                {
                    CoreManager.Current.App.DialogManager.ShowWarningDialog("Brak dostępu do internetu!",
                        "Połącz się z INTERNETEM!");
                    return false;
                }
                using (var client = Connection)
                {
                    var r = await client.GetStringAsync("index.html");
                    _connected = !string.IsNullOrWhiteSpace(r) && r.StartsWith("<!DOCTYPE html>");
                    if (!_connected)
                    {
                        CoreManager.Current.App.DialogManager.ShowWarningDialog("Brak dostępu do serwera!",
                    "Serwer jest wyłączony, bądź nie odpowiada.");
                    }
                }
            }
            catch (Exception e)
            {
                HandleWebException(e);
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
            CoreManager.Current.App.DialogManager.ShowWarningDialog("Błąd pobierania!", msg);
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
