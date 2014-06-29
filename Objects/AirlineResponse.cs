#region
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace FlightStats.Objects {
    [DataContract]
    public class AirlineResponse {
        [DataMember(Name = "airlines")]
        public IList<AirlineInfo> Airlines { get; set; }
    }
}