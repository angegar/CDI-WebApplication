using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model.Directions {
    /// <summary>
    /// When the Directions API returns results, it places them within a (JSON) routes array. 
    /// Even if the service returns no results (such as if the origin and/or destination doesn't exist) 
    /// it still returns an empty routes array. (XML responses consist of zero or more "route" elements.)
    /// Each element of the routes array contains a single result from the specified origin and destination. 
    /// This route may consist of one or more legs depending on whether any waypoints were specified. 
    /// As well, the route also contains copyright and warning information which must be displayed to 
    /// the user in addition to the routing information.
    /// </summary>
    [DataContract]
    public class Routes {
        /// <summary>
        /// Contains the viewport bounding box of this route.
        /// </summary>
        [DataMember(Name="bounds")]
        public Bounds Bounds { get; set; }
        /// <summary>
        /// Contains the copyrights text to be displayed for this route. 
        /// You must handle and display this information yourself.
        /// </summary>
        [DataMember(Name = "copyrights")]
        public string Copyrights { get; set; }
        /// <summary>
        /// Contains an array which contains information about a leg of the route, between two locations 
        /// within the given route. A separate leg will be present for each waypoint or destination specified. 
        /// (A route with no waypoints will contain exactly one leg within the legs array.) Each leg consists 
        /// of a series of steps.
        /// </summary>
        [DataMember(Name = "legs")]
        public Leg[] Legs { get; set; }
        /// <summary>
        /// Contains an object holding an array of encoded points and levels that represent an 
        /// approximate (smoothed) path of the resulting directions.
        /// </summary>
        [DataMember(Name = "overview_polyline")]
        public Polyline OverviewPolyline { get; set; }
        /// <summary>
        /// Contains a short textual description for the route, suitable for naming 
        /// and disambiguating the route from alternatives.
        /// </summary>
        [DataMember(Name = "summary")]
        public string Summary { get; set; }
    }
}
