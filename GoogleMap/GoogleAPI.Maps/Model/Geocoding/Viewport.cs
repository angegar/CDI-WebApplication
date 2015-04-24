using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model.Geocoding {
    /// <summary>
    /// contains the recommended viewport for displaying the returned result, 
    /// specified as two latitude,longitude values defining the southwest and 
    /// northeast corner of the viewport bounding box. Generally the viewport 
    /// is used to frame a result when displaying it to a user.
    /// </summary>
    [DataContract]
    public class Viewport {
        [DataMember(Name = "northeast")]
        public CardinalDirection Northeast { get; set; }

        [DataMember(Name = "southwest")]
        public CardinalDirection Southwest { get; set; }
    }
}
