// Wrapper to Google Directions API
// More information at:
// http://code.google.com/intl/en-US/apis/maps/documentation/geocoding/

// This wrapper was created by Evandro Venancio
// http://evenancio.wordpress.com
// Download this component at
// http://www.codeplex.com

using System.ServiceModel;
using System.ServiceModel.Web;
using GoogleAPI.Maps.Model.Geocoding;

namespace GoogleAPI.Maps.Services {
    /// <summary>
    /// Geocoding is the process of converting addresses (like "1600 Amphitheatre Parkway, 
    /// Mountain View, CA") into geographic coordinates (like latitude 37.423021 and 
    /// longitude -122.083739), which you can use to place markers or position the map. 
    /// The Google Geocoding API provides a direct way to access a geocoder via an HTTP request. Additionally, 
    /// the service allows you to perform the converse operation (turning coordinates into addresses); 
    /// this process is known as "reverse geocoding."
    /// </summary>
    [ServiceContract]
    public interface IGeocoding {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address">The address that you want to geocode</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate="json?address={address}&sensor=true")]
        Response GetByAddress(string address);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="latlng">The textual latitude/longitude value for which you wish to obtain the closest, human-readable address.</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "json?latlng={latlng}&sensor=true")]
        Response GetByCardinalDirection(string latlng);
    }
}
