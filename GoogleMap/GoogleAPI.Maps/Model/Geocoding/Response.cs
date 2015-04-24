using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model.Geocoding {
    [DataContract]
    public class Response {
        /// <summary>
        /// Contains an array of geocoded address information and geometry information.
        /// </summary>
        [DataMember(Name = "results")]
        public Results[] Results { get; set; }
        /// <summary>        
        /// Contains metadata on the request.
        /// 
        /// The "status" field within the Geocoding response object contains the status of the request, and may contain debugging information to help you track down why Geocoding is not working. The "status" field may contain the following values:
        /// "OK" indicates that no errors occurred; the address was successfully parsed and at least one geocode was returned.
        /// "ZERO_RESULTS" indicates that the geocode was successful but returned no results. This may occur if the geocode was passed a non-existent address or a latlng in a remote location.
        /// "OVER_QUERY_LIMIT" indicates that you are over your quota.
        /// "REQUEST_DENIED" indicates that your request was denied, generally because of lack of a sensor parameter.
        /// "INVALID_REQUEST" generally indicates that the query (address or latlng) is missing.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
