using System;
using System.Linq;
using System.Collections.Generic;

namespace FerryBooking.Core
{
    public class ScheduledRoutes
    {
        private readonly ScheduledRoutesService _scheduledRoutesService;
        
        private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
        private readonly string _newLine = Environment.NewLine;
        private const string Indentation = "    ";

        public ScheduledRoutes(Route route)
        {
            Route = route;
            Passengers = new List<Passenger>();
            _scheduledRoutesService = new ScheduledRoutesService(this);
        }

        public Route Route { get; private set; }
        public Vessel Vessel { get; private set; }
        public List<Passenger> Passengers { get; private set; }

        public void AddPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
        }

        public void SetVesselForRoute(Vessel vessel)
        {
            Vessel = vessel;
        }
        
        public string GetSummary()
        {
            double costOfJourney = _scheduledRoutesService.CalculateTotalCost();
            double revenueFromJourney = _scheduledRoutesService.CalculateTotalRevenue();
            int totalLoyaltyPointsAccrued = _scheduledRoutesService.CalculateTotalLoyaltyPointsAccrued();
            int totalLoyaltyPointsRedeemed = _scheduledRoutesService.CalculateTotalLoyaltyPointsRedeemed();
            int totalExpectedBaggage = _scheduledRoutesService.CalculateTotalExpectedBaggage();

            string result = "Journey summary for " + Route.Title;

            result += _verticalWhiteSpace;
            
            result += "Total passengers: " + Passengers.Count();
            result += _newLine;
            result += Indentation + "General sales: " + Passengers.Count(p => p.Type == PassengerType.General);
            result += _newLine;
            result += Indentation + "Loyalty member sales: " + Passengers.Count(p => p.Type == PassengerType.LoyaltyMember);
            result += _newLine;
            result += Indentation + "Carrier employee comps: " + Passengers.Count(p => p.Type == PassengerType.CarrierEmployee);
            
            result += _verticalWhiteSpace;
            result += "Total expected baggage: " + totalExpectedBaggage;

            result += _verticalWhiteSpace;

            result += "Total revenue from route: " + revenueFromJourney;
            result += _newLine;
            result += "Total costs from route: " + costOfJourney;
            result += _newLine;

            result += _scheduledRoutesService.GenerateProfitSummary(revenueFromJourney, costOfJourney);

            result += _verticalWhiteSpace;

            result += "Total loyalty points given away: " + totalLoyaltyPointsAccrued + _newLine;
            result += "Total loyalty points redeemed: " + totalLoyaltyPointsRedeemed + _newLine;

            result += _verticalWhiteSpace;
            
            if (_scheduledRoutesService.CanProceedWithRoute())
                result += "THIS ROUTE MAY PROCEED";
            else
                result += "ROUTE MAY NOT PROCEED";

            return result;
        }
    }
}
