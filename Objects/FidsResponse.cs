#region
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace FlightStats.Objects {
    [DataContract]
    public class FidsResponse {
        [DataMember(Name = "fidsData")]
        public IList<FidsData> FidsData { get; set; }
    }
}