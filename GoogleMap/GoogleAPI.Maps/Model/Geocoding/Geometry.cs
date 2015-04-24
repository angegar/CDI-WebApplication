using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model.Geocoding {
    [DataContract]
    public class Geometry {
        /// <summary>
        /// (optionally returned) stores the bounding box which can fully contain the returned result. 
        /// Note that these bounds may not match the recommended viewport. (For example, 
        /// San Francisco includes the Farallon islands, which are technically part of the city, 
        /// but probably should not be returned in the viewport.)
        /// </summary>
        [DataMember(Name = "bounds")]
        public Bounds Bounds { get; set; }
        /// <summary>
        /// contains the geocoded latitude,longitude value. For normal address lookups, this field is typically the most important.
        /// </summary>
        [DataMember(Name = "location")]
        public CardinalDirection Location { get; set; }
        /// <summary>
        /// stores additional data about the specified location. The following values are currently supported:
        /// "ROOFTOP" indicates that the returned result is a precise geocode for which we have location information accurate down to street address precision.
        /// "RANGE_INTERPOLATED" indicates that the returned result reflects an approximation (usually on a road) interpolated between two precise points (such as intersections). Interpolated results are generally returned when rooftop geocodes are unavailable for a street address.
        /// "GEOMETRIC_CENTER" indicates that the returned result is the geometric center of a result such as a polyline (for example, a street) or polygon (region).
        /// "APPROXIMATE" indicates that the returned result is approximate.
        /// </summary>
        [DataMember(Name = "location_type")]
        public string LocationType { get; set; }
        /// <summary>
        /// contains the recommended viewport for displaying the returned result, specified as two latitude,
        /// longitude values defining the southwest and northeast corner of the viewport bounding box. 
        /// Generally the viewport is used to frame a result when displaying it to a user.
        /// </summary>
        [DataMember(Name = "viewport")]
        public Viewport Viewport { get; set; }
    }
}
