using AutomatedValetParking.Entities;
using AutomatedValetParking.Services.IManager;
using System.Collections.Generic;

namespace AutomatedValetParking.Services.Manager
{
    /// <summary>
    /// Factory representation of the Parking lot factory
    /// </summary>
    public class ParkingLotFactory : IParkingLotFactory
    {
        /// <summary>
        /// Creates a parking lot 
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="columns">Number of columns</param>
        /// <param name="reservedRows">Reserved rows</param>
        /// <returns></returns>
        public IParkingLot CreateParkingLot(int rows, int columns, IEnumerable<int> reservedRows)
        {
            return new ParkingLot(rows, columns, reservedRows);
        }
    }
}
