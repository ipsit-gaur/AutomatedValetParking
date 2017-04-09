using System.Collections.Generic;

namespace AutomatedValetParking.Entities
{
    /// <summary>
    /// Interface for a Parking lot
    /// </summary>
    public interface IParkingLot
    {
        List<IParkingSpace> ParkingSpaces { get; set; }
    }
}
