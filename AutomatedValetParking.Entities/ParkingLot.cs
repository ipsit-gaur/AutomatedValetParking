using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedValetParking.Entities
{
    /// <summary>
    /// Entity representing a Parking Lot
    /// </summary>
    public class ParkingLot : IParkingLot
    {
        public List<IParkingSpace> ParkingSpaces { get; set; }

        /// <summary>
        /// Instantiates a new Parking lot
        /// </summary>
        /// <param name="rows">Number of rows in the parking lot</param>
        /// <param name="columns">Number of columns in the parking lot</param>
        /// <param name="reservedRows">Reserved rows</param>
        public ParkingLot(int rows, int columns, IEnumerable<int> reservedRows)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new InvalidOperationException("There should be at least one row and column in the parking lot");
            }

            ParkingSpaces = new List<IParkingSpace>();

            for (var x = 1; x <= rows; x++)
            {
                for (var y = 1; y <= columns; y++)
                {
                    var parkingType = reservedRows != null ? reservedRows.Contains(x) ? ParkingType.Reserved : ParkingType.Standard : ParkingType.Standard;
                    var parkingSpace = new ParkingSpace(x, y, parkingType);

                    ParkingSpaces.Add(parkingSpace);
                }
            }
        }
    }
}
