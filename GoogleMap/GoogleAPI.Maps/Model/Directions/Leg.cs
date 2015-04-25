using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model.Directions {
    /// <summary>
    /// Each element in the legs array specifies a single leg of the journey from the 
    /// origin to the destination in the calculated route. For routes that contain no waypoints, 
    /// the route will consist of a single "leg," but for routes that define one or more waypoints, 
    /// the route will consist of one or more legs, corresponding to the specific legs of the journey.
    /// </summary>
    [DataContract]
    public class Leg {
        /// <summary>
        /// indicates the total distance covered by this leg, as a field with the following elements:
        /// "value" indicates the distance in meters
        /// "text" contains a human-readable representation of the distance, displayed in units as used at the origin 
        /// (or as overridden within the units parameter in the request), in the language specified in the request. 
        /// (For example, miles and feet will be used for any origin within the United States.) Note that regardless 
        /// of what unit system is displayed as text, the distance.value field always contains a value expressed in meters.
        /// These fields may be absent if the distance is unknown.
        /// </summary>
        [DataMember(Name = "distance")]
        public TextValue Distance { get; set; }
        /// <summary>
        /// indicates the total duration of this leg, as a field with the following elements:
        /// "value" indicates the duration in seconds.
        /// "text" contains a human-readable representation of the duration.
        /// These fields may be absent if the duration is unknown.
        /// </summary>
        [DataMember(Name = "duration")]
        public TextValue Duration { get; set; }
        /// <summary>
        /// Contains the human-readable address (typically a street address) reflecting the end_location of this leg.
        /// </summary>
        [DataMember(Name = "end_address")]
        public string EndAddress { get; set; }
        /// <summary>
        /// May be different than the provided destination of this leg if, for example, a road is not near the destination.
        /// </summary>
        [DataMember(Name = "end_location")]
        public CardinalDirection EndLocation { get; set; }
        /// <summary>
        /// Contains the human-readable address (typically a street address) reflecting the start_location of this leg.
        /// </summary>
        [DataMember(Name = "start_address")]
        public string StartAddress { get; set; }
        /// <summary>
        /// May be different than the provided origin of this leg if, for example, a road is not near the origin.
        /// </summary>
        [DataMember(Name = "start_location")]
        public CardinalDirection StartLocation { get; set; }
        /// <summary>
        /// Contains an array of steps denoting information about each separate step of the leg of the journey.
        /// </summary>
        [DataMember(Name = "steps")]
        public Step[] Steps { get; set; }
    }
}
