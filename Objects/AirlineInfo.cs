#region
using System.Runtime.Serialization;

#endregion

namespace FlightStats.Objects {
    [DataContract]
    public class AirlineInfo {
        [DataMember(Name = "active")]
        public bool Active { get; set; }

        [DataMember(Name = "fs")]
        public string FS { get; set; }

        [DataMember(Name = "iata")]
        public string Iata { get; set; }

        [DataMember(Name = "icao")]
        public string Icao { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}