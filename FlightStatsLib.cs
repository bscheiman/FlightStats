#region
using System;
using System.Net.Http;
using System.Text;
using bscheiman.Common.Extensions;
using FlightStats.Objects;
using ServiceStack.Text;

#endregion

namespace FlightStats {
    public class FlightStatsLib {
        public string AppId { get; set; }
        public string AppKey { get; set; }

        public FlightStatsLib(string appId, string appKey) {
            if (string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("appId");

            if (string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("appKey");

            AppId = appId;
            AppKey = appKey;
        }

        public FidsResponse GetArrivalsForAirport(string airport) {
            return Get(string.Format("fids/rest/v1/json/{0}/arrivals", airport), new {
                requestedFields = "airlineCode,flightNumber,city,currentTime,gate,remarks",
                sortFields = "currentTime",
                excludeCargoOnlyFlights = true,
                lateMinutes = 15,
            }).FromJson<FidsResponse>();
        }

        public FidsResponse GetDeparturesForAirport(string airport) {
            return Get(string.Format("fids/rest/v1/json/{0}/departures", airport), new {
                requestedFields = "airlineCode,flightNumber,city,currentTime,gate,remarks",
                sortFields = "currentTime",
                excludeCargoOnlyFlights = true,
                lateMinutes = 15,
            }).FromJson<FidsResponse>();
        }

        #region Helpers
        internal string Delete(string endpoint) {
            return GetClient().DeleteAsync(GetEndpoint(endpoint)).Result.Content.ReadAsStringAsync().Result;
        }

        internal string Get(string endpoint, object obj = null) {
            Console.WriteLine(obj.ToQueryString());

            return GetClient().GetStringAsync(GetEndpoint(endpoint, obj)).Result;
        }

        internal HttpClient GetClient() {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("appId", AppId);
            client.DefaultRequestHeaders.Add("appKey", AppKey);

            return client;
        }

        internal Uri GetEndpoint(string endpoint, object obj = null) {
            return new Uri(string.Format("https://api.flightstats.com/flex/{0}?{1}", endpoint, obj.ToQueryString(readAndWrite: false)));
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