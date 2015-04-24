using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model {
    [DataContract]
    public class Bounds {
        [DataMember(Name = "northeast")]
        public CardinalDirection Northeast { get; set; }

        [DataMember(Name = "southwest")]
        public CardinalDirection Southwest { get; set; }
    }
}
