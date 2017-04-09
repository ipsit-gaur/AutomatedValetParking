namespace AutomatedValetParking.Entities
{
    /// <summary>
    /// Entity representing a Parking space
    /// </summary>
    public class ParkingSpace : IParkingSpace
    {
        private int _locationX;

        private int _locationY;

        public bool IsOccupied { get; set; }

        public ParkingType Type { get; set; }

        /// <summary>
        /// Instantiates a parking space
        /// </summary>
        /// <param name="locationX">X Location of the parking space</param>
        /// <param name="locationY">Y Location of the parking space</param>
        public ParkingSpace(int locationX, int locationY)
        {
            _locationX = locationX;
            _locationY = locationY;
        }

        /// <summary>
        /// Instantiates a Parking space
        /// </summary>
        /// <param name="locationX">X Location of the parking space</param>
        /// <param name="locationY">Y Location of the parking space</param>
        /// <param name="type">Type of parking</param>
        public ParkingSpace(int locationX, int locationY, ParkingType type) : this(locationX, locationY)
        {
            Type = type;
        }

        /// <summary>
        /// Converts a Parking space to a string
        /// </summary>
        /// <returns>Booking number of the space</returns>
        public override string ToString()
        {
            return string.Format("R{0}-{1}", _locationX, _locationY);
        }
    }
}
