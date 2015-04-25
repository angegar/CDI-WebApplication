using System;
using System.Globalization;
using System.Linq;
using GoogleAPI.Maps.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogleAPI.Maps.Tests
{    
    /// <summary>
    ///This is a test class for GeocodingTest and is intended
    ///to contain all GeocodingTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GeocodingTest {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetByAddress
        ///</summary>
        [TestMethod()]
        public void GetByAddressTest() {
            string address = "Av Capitão Casa, 910, São Bernardo do Campo, SP";
            
            var target = new Geocoding();
            var actual = target.GetByAddress(address);
            var brazil = actual.Results[0].AddressComponents.Where(s => s.LongName == "Brazil");
            Assert.IsTrue(brazil.Count() == 1);
        }

        /// <summary>
        ///A test for GetByAddress
        ///</summary>
        [TestMethod()]
        public void GetByPostalCodeTest() {
            string address = "09571-300";

            var target = new Geocoding();
            var actual = target.GetByAddress(address);
            var brazil = actual.Results[0].AddressComponents.Where(s => s.LongName == "Brazil");
            Assert.IsTrue(brazil.Count() == 1);
        }

        [TestMethod()]
        public void GetByCardinalDirectionTest() {
            string address = "-23.7263917,-46.5620729";

            var target = new Geocoding();
            var actual = target.GetByCardinalDirection(address);
            var brazil = actual.Results[0].AddressComponents.Where(s => s.LongName == "Brazil");
            Assert.IsTrue(brazil.Count() == 1);
        }

        [TestMethod()]
        public void GetPostalCodeByAddressTest() {
            string postalCode = GetPostalCode("Av Capitão Casa - São Bernardo do Campo - SP - Brazil");
            Console.WriteLine();
        }

        [TestMethod()]
        public void GetAddressByPostalCodeTest() {
            string address = GetAddress("04055-110");
            Console.WriteLine();
        }

        private string GetAddress(string postalCode) {
            var target = new Geocoding();
            var addressLocation = target.GetByAddress(postalCode).Results[0].Geometry.Location;
            var addressInformation = target.GetByCardinalDirection(
                String.Format("{0},{1}",
                    addressLocation.Latitude.ToString(new CultureInfo("en-US")),
                    addressLocation.Longitude.ToString(new CultureInfo("en-US"))));
            var address = addressInformation.Results[0].FormattedAddress;
            return address;
        }

        private string GetPostalCode(string address) {
            var target = new Geocoding();
            var addressLocation = target.GetByAddress(address).Results[0].Geometry.Location;
            var addressInformation = target.GetByCardinalDirection(
                String.Format("{0},{1}",
                    addressLocation.Latitude.ToString(new CultureInfo("en-US")),
                    addressLocation.Longitude.ToString(new CultureInfo("en-US"))));
            return addressInformation.Results[0].AddressComponents.Where(s => s.GeoTypes.Contains("postal_code")).FirstOrDefault().LongName;            
        }
    }
}
