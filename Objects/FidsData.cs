#region
using System.Runtime.Serialization;

#endregion

namespace FlightStats.Objects {
    [DataContract]
    public class FidsData {
        [DataMember(Name = "airlineCode")]
        public string AirlineCode { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "currentTime")]
        public string CurrentTime { get; set; }

        [DataMember(Name = "flightNumber")]
        public string FlightNumber { get; set; }

        [DataMember(Name = "gate")]
        public string Gate { get; set; }

        [DataMember(Name = "remarks")]
        public string Remarks { get; set; }
    }
}