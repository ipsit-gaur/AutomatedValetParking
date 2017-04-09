using AutomatedValetParking.Entities;
using System.Collections.Generic;

namespace AutomatedValetParking.Services.IManager
{
    /// <summary>
    /// Interface for Parking Lot factory
    /// </summary>
    public interface IParkingLotFactory
    {
        /// <summary>
        /// Creates a parking lot 
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="columns">Number of columns</param>
        /// <param name="reservedRows">Reserved rows</param>
        /// <returns></returns>
        IParkingLot CreateParkingLot(int rows, int columns, IEnumerable<int> reservedRows);
    }
}
