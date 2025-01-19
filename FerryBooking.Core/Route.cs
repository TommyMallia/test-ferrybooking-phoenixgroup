namespace FerryBooking.Core
{
    public class Route
    {
        private readonly string _origin;
        private readonly string _destination;

        public Route(string origin, string destination)
        {
            _origin = origin;
            _destination = destination;
        }

        public string Title { get { return _origin + " to " + _destination; } }
        public double BasePrice { get; set; }
        public double BaseCost { get; set; }
        public int LoyaltyPointsGained { get; set; }
        public double MinimumTakeOffPercentage { get; set; }        
    }
}
