using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model.Directions {
    [DataContract]
    public class Polyline {
        [DataMember(Name = "levels")]
        public string Text { get; set; }
        [DataMember(Name = "points")]
        public string Value { get; set; }
    }
}
