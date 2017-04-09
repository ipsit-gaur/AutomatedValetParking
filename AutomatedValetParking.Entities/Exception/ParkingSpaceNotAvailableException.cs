namespace AutomatedValetParking.Entities.Exception
{
    /// <summary>
    /// An exception which is raised when there is no parking space available
    /// </summary>
    public class ParkingSpaceNotAvailableException : System.Exception
    {
        public override string Message
        {
            get
            {
                return "Parking space is not available";
            }
        }
    }
}
