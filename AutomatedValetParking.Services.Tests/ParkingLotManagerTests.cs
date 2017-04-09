using AutomatedValetParking.Entities.Exception;
using AutomatedValetParking.Services.IManager;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AutomatedValetParking.Services.Tests
{
    [TestClass]
    public class ParkingLotManagerTests : BaseTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            LoadUnityConfiguration();
        }

        [TestMethod]
        [Description("Verifies that parking system setups successfully")]
        public void SystemSetupSuccessful()
        {
            var manager = Container.Resolve<IParkingLotManager>();
            manager.SetupSystem(3, 4, null);

            Assert.AreEqual(3 * 4, manager.AvailableSpaces());
        }

        [TestMethod]
        [Description("Verifies that parking system cannot setup with invalid rows")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SystemSetupUnSuccessfulWithInvalidRows()
        {
            var manager = Container.Resolve<IParkingLotManager>();
            manager.SetupSystem(-1, 4, null);
        }

        [TestMethod]
        [Description("Verifies that parking system cannot setup with invalid columns")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SystemSetupUnSuccessfulWithInvalidColumns()
        {
            var manager = Container.Resolve<IParkingLotManager>();
            manager.SetupSystem(3, 0, null);
        }

        [TestMethod]
        [Description("Verifies that a vehicle parked produces correct booking id")]
        public void VerifyBookingIdTest()
        {
            var manager = Container.Resolve<IParkingLotManager>();
            manager.SetupSystem(4, 4, null);

            var ticketId = manager.ParkVehicle(Entities.ParkingType.Standard);

            Assert.AreEqual("R1-1", ticketId);
        }

        [TestMethod]
        [Description("Verifies that no reserved parking is available when reserved was not configured")]
        [ExpectedException(typeof(ParkingSpaceNotAvailableException))]
        public void VerifyNoReservedBookingTest()
        {
            var manager = Container.Resolve<IParkingLotManager>();
            manager.SetupSystem(4, 4, null);

            var ticketId = manager.ParkVehicle(Entities.ParkingType.Reserved);
        }

        [TestMethod]
        [Description("Verifies that no standard parking is available when standard parking is not configured")]
        [ExpectedException(typeof(ParkingSpaceNotAvailableException))]
        public void VerifyNoStandardBookingTest()
        {
            var manager = Container.Resolve<IParkingLotManager>();
            manager.SetupSystem(2, 2, new List<int> { 1, 2 });

            var ticketId = manager.ParkVehicle(Entities.ParkingType.Standard);
        }
    }
}
