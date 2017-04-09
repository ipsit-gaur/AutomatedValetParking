namespace AutomatedValetParking.Entities
{
    /// <summary>
    /// Interface for a Parking space
    /// </summary>
    public interface IParkingSpace
    {
        bool IsOccupied { get; set; }

        ParkingType Type { get; set; }
    }
}
