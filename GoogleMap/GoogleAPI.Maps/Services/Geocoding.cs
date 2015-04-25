using System.ServiceModel;
using System.ServiceModel.Channels;
using GoogleAPI.Maps.Model.Geocoding;

namespace GoogleAPI.Maps.Services {
    public partial class Geocoding : ClientBase<IGeocoding>, IGeocoding {
        public Geocoding()
        {            
        }

        public Geocoding(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public Geocoding(
         string endpointConfigurationName,
        string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public Geocoding(string endpointConfigurationName,
        EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public Geocoding(Binding binding,
        EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public Response GetByAddress(string address) {
            return base.Channel.GetByAddress(address);
        }

        public Response GetByCardinalDirection(string latlng) {
            return base.Channel.GetByCardinalDirection(latlng);
        }
    }
}
