using System.ServiceModel;
using System.ServiceModel.Channels;
using GoogleAPI.Maps.Model.Directions;

namespace GoogleAPI.Maps.Services {
    public class Directions : ClientBase<IDirections>, IDirections {
        public Directions()
        {            
        }

        public Directions(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public Directions(
         string endpointConfigurationName,
        string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public Directions(string endpointConfigurationName,
        EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public Directions(Binding binding,
        EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public Response GetRoutes(string origin, string destination) {
            return base.Channel.GetRoutes(origin, destination);
        }
    }
}
