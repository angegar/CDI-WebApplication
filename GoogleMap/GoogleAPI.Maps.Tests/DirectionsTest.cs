using System.Linq;
using GoogleAPI.Maps.Model.Directions;
using GoogleAPI.Maps.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GoogleAPI.Maps.Tests
{
    
    
    /// <summary>
    ///This is a test class for DirectionsTest and is intended
    ///to contain all DirectionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DirectionsTest {


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
        ///A test for GetRoutes
        ///</summary>
        [TestMethod()]
        public void GetRoutesTest() {
            Directions target = new Directions(); // TODO: Initialize to an appropriate value
            string origin = "09571-300"; // TODO: Initialize to an appropriate value
            string destination = "09812-000"; // TODO: Initialize to an appropriate value

            Response actual = target.GetRoutes(origin, destination);            
            string res = string.Empty;

            actual.Routes.FirstOrDefault().
                Legs.FirstOrDefault().
                Steps.ToList().
                    ForEach(x => res += x.HtmlInstructions + "<BR />");

            Assert.IsTrue(true);           
        }
    }
}
