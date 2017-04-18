using AutomatedValetParking.Entities;
using AutomatedValetParking.Entities.Exception;
using AutomatedValetParking.Services.IManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedValetParking.Services.Manager
{
    /// <summary>
    /// Parking lot manager for managing all activities of parking lot
    /// </summary>
    public class ParkingLotManager : IParkingLotManager
    {
        private readonly IParkingLotFactory _parkingLotFactory;
        private IParkingLot _parkingLot;

        /// <summary>
        /// Instantiates a new Parking lot Manager
        /// </summary>
        /// <param name="parkingLotFactory">Parking lot factory</param>
        public ParkingLotManager(IParkingLotFactory parkingLotFactory)
        {
            _parkingLotFactory = parkingLotFactory;
        }

        /// <summary>
        /// Setups the parking system for the first time
        /// </summary>
        /// <param name="rows">Number of rows in parking lot</param>
        /// <param name="columns">Number of columns in parking lot</param>
        /// <param name="reservedParkingRows">List of reserved parking rows</param>
        public void SetupSystem(int rows, int columns, IEnumerable<int> reservedParkingRows)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new InvalidOperationException("There should be at least one row and column in the parking lot");
            }

            _parkingLot = _parkingLotFactory.CreateParkingLot(rows, columns, reservedParkingRows);
        }

        /// <summary>
        /// Parks a vehicle into the Parking system
        /// </summary>
        /// <param name="parkingType">Type of Parking</param>
        /// <returns>Ticket number</returns>
        public string ParkVehicle(ParkingType parkingType)
        {
            if (_parkingLot == null)
            {
                throw new InvalidOperationException("Parking lot is not opened!!!");
            }

            var availableSpace = GetAvailableSpot(parkingType);

            if (availableSpace == null)
            {
                throw new ParkingSpaceNotAvailableException();
            }

            availableSpace.IsOccupied = true;
            return availableSpace.ToString();
        }

        private IParkingSpace GetAvailableSpot(ParkingType parkingType)
        {
            return _parkingLot?.ParkingSpaces.FirstOrDefault(x => !x.IsOccupied && x.Type == parkingType);
        }

        /// <summary>
        /// Gives the available spaces available
        /// </summary>
        /// <returns>Available parking spaces</returns>
        public int AvailableSpaces()
        {
            return _parkingLot?.ParkingSpaces.Count(x => !x.IsOccupied) ?? 0;
        }

        /// <summary>
        /// Unparks a vehicle in the parking lot
        /// </summary>
        /// <param name="ticketNumber">Ticket number of the Vehicle</param>
        public void UnParkVehicle(string ticketNumber)
        {
            var parkingLot = GetParkingSpaceDetails(ticketNumber);

            if (parkingLot == null)
            {
                throw new InvalidOperationException("No such space available with the specified ticket.");
            }

            parkingLot.IsOccupied = false;
        }

        private IParkingSpace GetParkingSpaceDetails(string ticketNumber)
        {
            if (string.IsNullOrEmpty(ticketNumber))
            {
                throw new ArgumentException("Ticket number cannot be blank");
            }

            return _parkingLot.ParkingSpaces.FirstOrDefault(x => x.ToString().Equals(ticketNumber));
        }
    }
}
