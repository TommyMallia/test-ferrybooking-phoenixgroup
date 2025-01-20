﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace FerryBooking.Core
{
    public class ScheduledRoutes
    {
        private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
        private readonly string _newLine = Environment.NewLine;
        private const string Indentation = "    ";

        public ScheduledRoutes(Route route)
        {
            Route = route;
            Passengers = new List<Passenger>();
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
            double costOfJourney = Passengers.Count * Route.BaseCost;
            double profitFromJourney = CalculateProfit();
            int totalLoyaltyPointsAccrued = 0;
            int totalLoyaltyPointsRedeemed = 0;
            int totalExpectedBaggage = 0;
            int seatsTaken = 0;

            string result = "Journey summary for " + Route.Title;

            foreach (var passenger in Passengers)
            {
                switch (passenger.Type)
                {
                    case(PassengerType.General):
                        {
                            totalExpectedBaggage++;
                            break;
                        }
                    case(PassengerType.LoyaltyMember):
                        {
                            if (passenger.IsUsingLoyaltyPoints)
                            {
                                int loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(Route.BasePrice));
                                passenger.LoyaltyPoints -= loyaltyPointsRedeemed;
                                totalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                            }
                            else
                            {
                                totalLoyaltyPointsAccrued += Route.LoyaltyPointsGained;                
                            }
                            totalExpectedBaggage += 2;
                            break;
                        }
                    case(PassengerType.CarrierEmployee):
                        {
                            totalExpectedBaggage += 1;
                            break;
                        }
                }
                seatsTaken++;
            }

            result += _verticalWhiteSpace;
            
            result += "Total passengers: " + seatsTaken;
            result += _newLine;
            result += Indentation + "General sales: " + Passengers.Count(p => p.Type == PassengerType.General);
            result += _newLine;
            result += Indentation + "Loyalty member sales: " + Passengers.Count(p => p.Type == PassengerType.LoyaltyMember);
            result += _newLine;
            result += Indentation + "Carrier employee comps: " + Passengers.Count(p => p.Type == PassengerType.CarrierEmployee);
            
            result += _verticalWhiteSpace;
            result += "Total expected baggage: " + totalExpectedBaggage;

            result += _verticalWhiteSpace;

            result += "Total revenue from route: " + profitFromJourney;
            result += _newLine;
            result += "Total costs from route: " + costOfJourney;
            result += _newLine;

            result += GenerateProfitSummary(profitFromJourney, costOfJourney);

            result += _verticalWhiteSpace;

            result += "Total loyalty points given away: " + totalLoyaltyPointsAccrued + _newLine;
            result += "Total loyalty points redeemed: " + totalLoyaltyPointsRedeemed + _newLine;

            result += _verticalWhiteSpace;
            
            double profitSurplus = profitFromJourney - costOfJourney; //wanted to take this out but ran out of time with the refactor

            if (profitSurplus > 0 && 
                seatsTaken < Vessel.NumberOfSeats && 
                seatsTaken / (double)Vessel.NumberOfSeats > Route.MinimumTakeOffPercentage)
                result += "THIS ROUTE MAY PROCEED";
            else
                result += "ROUTE MAY NOT PROCEED";

            return result;
        }

        private double CalculateProfit()
        {
            double totalProfit = 0;
            foreach (var passenger in Passengers)
            {
                if (passenger.Type == PassengerType.LoyaltyMember && passenger.IsUsingLoyaltyPoints)
                {
                    totalProfit += 0;
                }
                else if (passenger.Type != PassengerType.CarrierEmployee)
                {
                    totalProfit += Route.BasePrice;
                }
            }

            return totalProfit;
        }
        
        private string GenerateProfitSummary(double profitFromJourney, double costOfJourney)
        {
            double profitSurplus = profitFromJourney - costOfJourney;
            return (profitSurplus > 0 ? "Route generating profit of: " : "Route losing money of: ") + profitSurplus;
        }
    }
}
