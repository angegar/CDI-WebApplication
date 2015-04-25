using System;
using System.Globalization;
using System.Linq;
using GoogleAPI.Maps.Services;

namespace GoogleAPI.Maps.Tests {
    class Class1 {
        public void GetAddressByPostalCodeTest() {
            string address = GetAddress("04055-110");
        }

        public void GetPostalCodeByAddressTest() {
            string postalCode = GetPostalCode(
                "Av Capitão Casa - São Bernardo do Campo - SP - Brazil");
        }

        private string GetAddress(string postalCode) {
            var target = new Geocoding();
            var addressLocation = target.GetByAddress(postalCode).
                Results[0].Geometry.Location;
            var addressInformation = target.GetByCardinalDirection(
                String.Format("{0},{1}",
                    addressLocation.Latitude.
                        ToString(new CultureInfo("en-US")),
                    addressLocation.Longitude.
                        ToString(new CultureInfo("en-US"))));
            var address = addressInformation.Results[0].FormattedAddress;
            return address;
        }

        private string GetPostalCode(string address) {
            var target = new Geocoding();
            var addressLocation = target.GetByAddress(address).
                Results[0].Geometry.Location;
            var addressInformation = target.GetByCardinalDirection(
                String.Format("{0},{1}",
                    addressLocation.Latitude.
                        ToString(new CultureInfo("en-US")),
                    addressLocation.Longitude.
                        ToString(new CultureInfo("en-US"))));
            return addressInformation.Results[0].
                AddressComponents.Where(s => 
                    s.GeoTypes.Contains("postal_code")).
                        FirstOrDefault().LongName;
        }
    }
}
