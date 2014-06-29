#region
using System;
using System.Collections.Generic;
using System.Linq;
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

        public AirlineInfo GetAirlineInfo(string airline) {
            return Get(string.Format("airlines/rest/v1/json/icao/{0}", airline)).FromJson<AirlineResponse>().Airlines.First();
        }

        public IList<FidsData> GetArrivalsForAirport(string airport) {
            return Get(string.Format("fids/rest/v1/json/{0}/arrivals", airport), new {
                requestedFields = "airlineCode,flightNumber,city,currentTime,gate,remarks",
                sortFields = "currentTime",
                excludeCargoOnlyFlights = true,
                lateMinutes = 15,
            }).FromJson<FidsResponse>().FidsData;
        }

        public IList<FidsData> GetDeparturesForAirport(string airport) {
            return Get(string.Format("fids/rest/v1/json/{0}/departures", airport), new {
                requestedFields = "airlineCode,flightNumber,city,currentTime,gate,remarks",
                sortFields = "currentTime",
                excludeCargoOnlyFlights = true,
                lateMinutes = 15,
            }).FromJson<FidsResponse>().FidsData;
        }

        #region Helpers
        internal string Delete(string endpoint) {
            return GetClient().DeleteAsync(GetEndpoint(endpoint)).Result.Content.ReadAsStringAsync().Result;
        }

        internal string Get(string endpoint, object obj = null) {
            return GetClient().GetStringAsync(GetEndpoint(endpoint, obj)).Result;
        }

        internal HttpClient GetClient() {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("appId", AppId);
            client.DefaultRequestHeaders.Add("appKey", AppKey);

            return client;
        }

        internal Uri GetEndpoint(string endpoint, object obj = null) {
            obj = obj ?? new object();

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