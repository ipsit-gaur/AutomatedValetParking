using AutomatedValetParking.Entities;
using System.Collections.Generic;

namespace AutomatedValetParking.Services.IManager
{
    /// <summary>
    /// Manages all the parking services
    /// </summary>
    public interface IParkingLotManager
    {
        /// <summary>
        /// Setups the parking system for the first time
        /// </summary>
        /// <param name="rows">Number of rows in parking lot</param>
        /// <param name="columns">Number of columns in parking lot</param>
        /// <param name="reservedParkingRows">List of reserved parking rows</param>
        void SetupSystem(int rows, int columns, IEnumerable<int> reservedParkingRows);

        /// <summary>
        /// Parks a vehicle into the Parking system
        /// </summary>
        /// <param name="parkingType">Type of Parking</param>
        /// <returns>Ticket number</returns>
        string ParkVehicle(ParkingType parkingType);

        /// <summary>
        /// Gives the available spaces available
        /// </summary>
        /// <returns>Available parking spaces</returns>
        int AvailableSpaces();
    }
}
