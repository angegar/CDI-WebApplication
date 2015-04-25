// Wrapper to Google Directions API
// More information at:
// http://code.google.com/intl/en-US/apis/maps/documentation/directions/

// This wrapper was created by Evandro Venancio
// http://evenancio.wordpress.com
// Download this component at
// http://www.codeplex.com

using System.ServiceModel;
using System.ServiceModel.Web;
using GoogleAPI.Maps.Model.Directions;

namespace GoogleAPI.Maps.Services {
    /// <summary>
    /// The Google Directions API is a service that calculates directions between locations using an HTTP request. 
    /// Directions may specify origins, destinations and waypoints either as text strings (e.g. "Chicago, IL" or 
    /// "Darwin, NT, Australia") or as latitude/longitude coordinates. The Directions API can return multi-part 
    /// directions using a series of waypoints.
    /// 
    /// This service is generally designed for calculating directions for static (known in advance) addresses 
    /// for placement of application content on a map; this service is not designed to respond in real time 
    /// to user input, for example. For dynamic directions calculations (for example, within a user interface element), 
    /// consult the documentation for the JavaScript API V3 Directions Service.
    /// 
    /// Calculating directions is a time and resource intensive task. Whenever possible, calculate known 
    /// addresses ahead of time (using the service described here) and store your results in a temporary 
    /// cache of your own design.
    /// </summary>
    [ServiceContract]
    public interface IDirections {
        /// <summary>
        /// </summary>
        /// <param name="origin">The address or textual latitude/longitude value from which you wish to calculate directions.</param>
        /// <param name="destination">The address or textual latitude/longitude value from which you wish to calculate directions.</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "json?origin={origin}&destination={destination}&sensor=true")]
        Response GetRoutes(string origin, string destination);
    }
}
