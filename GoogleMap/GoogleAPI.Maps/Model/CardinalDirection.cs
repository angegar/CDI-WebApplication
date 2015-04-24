using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model {
    [DataContract]
    public class CardinalDirection {
        [DataMember(Name = "lat")]
        public Double Latitude { get; set; }

        [DataMember(Name = "lng")]
        public Double Longitude { get; set; }
    }
}
