using System;

namespace FerryBooking.Core
{
    public class ScheduledRoutesService
    {
        private readonly ScheduledRoutes _scheduledRoutes;

        public ScheduledRoutesService(ScheduledRoutes scheduledRoutes)
        {
            _scheduledRoutes = scheduledRoutes;
        }

        public double CalculateTotalCost()
        {
            return _scheduledRoutes.Passengers.Count * _scheduledRoutes.Route.BaseCost;
        }

        public double CalculateTotalRevenue()
        {
            double totalRevenue = 0;
            foreach (var passenger in _scheduledRoutes.Passengers)
            {
                if (passenger.Type == PassengerType.LoyaltyMember && passenger.IsUsingLoyaltyPoints)
                {
                    totalRevenue += 0;
                }
                else if (passenger.Type != PassengerType.CarrierEmployee)
                {
                    totalRevenue += _scheduledRoutes.Route.BasePrice;
                }
            }

            return totalRevenue;
        }

        public bool CanProceedWithRoute()
        {
            return CalculateTotalRevenue() > CalculateTotalCost() && _scheduledRoutes.Passengers.Count < _scheduledRoutes.Vessel.NumberOfSeats && _scheduledRoutes.Passengers.Count / (double)_scheduledRoutes.Vessel.NumberOfSeats > _scheduledRoutes.Route.MinimumTakeOffPercentage;
        }

        public string GenerateProfitSummary(double profitFromJourney, double costOfJourney)
        {
            double profitSurplus = profitFromJourney - costOfJourney;
            return (profitSurplus > 0 ? "Route generating profit of: " : "Route losing money of: ") + profitSurplus;
        }

        public int CalculateTotalLoyaltyPointsAccrued()
        {
            int accruedPoints = 0;

            foreach (var passenger in _scheduledRoutes.Passengers)
            {
                if (passenger.Type == PassengerType.LoyaltyMember && !passenger.IsUsingLoyaltyPoints)
                {
                    accruedPoints += _scheduledRoutes.Route.LoyaltyPointsGained;
                }
            }

            return accruedPoints;
        }

        public int CalculateTotalLoyaltyPointsRedeemed()
        {
            int redeemedPoints = 0;

            foreach (var passenger in _scheduledRoutes.Passengers)
            {
                if (passenger.Type == PassengerType.LoyaltyMember && passenger.IsUsingLoyaltyPoints)
                {
                    redeemedPoints += Convert.ToInt32(Math.Ceiling(_scheduledRoutes.Route.BasePrice));
                }
            }

            return redeemedPoints;
        }

        public int CalculateTotalExpectedBaggage()
        {
            int totalBaggage = 0;
            foreach (var passenger in _scheduledRoutes.Passengers)
            {
                switch (passenger.Type)
                {
                    case PassengerType.General:
                        totalBaggage++;
                        break;
                    case PassengerType.LoyaltyMember:
                        totalBaggage += 2;
                        break;
                    case PassengerType.CarrierEmployee:
                        totalBaggage++;
                        break;
                }
            }
            return totalBaggage;
        }
    }
}