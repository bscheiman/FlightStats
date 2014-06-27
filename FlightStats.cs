#region
using System;
using System.Net.Http;
using System.Text;

#endregion

namespace FlightStats {
    public class FlightStats {
        public string AppId { get; set; }
        public string AppKey { get; set; }

        public FlightStats(string appId, string appKey) {
            if (string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("appId");

            if (string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("appKey");

            AppId = appId;
            AppKey = appKey;
        }

        #region Helpers
        internal string Delete(string endpoint) {
            return GetClient().DeleteAsync(GetEndpoint(endpoint)).Result.Content.ReadAsStringAsync().Result;
        }

        internal string Get(string endpoint) {
            return GetClient().GetStringAsync(GetEndpoint(endpoint)).Result;
        }

        internal HttpClient GetClient() {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("", "");
            client.DefaultRequestHeaders.Add("", "");

            return client;
        }

        internal Uri GetEndpoint(string endpoint) {
            return new Uri(string.Format("https://api.flightstats.com/flex/airlines/rest/{}", endpoint));
        }

        internal string Post(string endpoint, object obj = null) {
            obj = obj ?? new object();

            return
                GetClient()
                    .PostAsync(GetEndpoint(endpoint), new StringContent("", Encoding.UTF8, "application/json"))
                    .Result.Content.ReadAsStringAsync()
                    .Result;
        }
        #endregion
    }
}