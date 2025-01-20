using System;
using FerryBooking.Core;

namespace FerryBooking.Core
{
    public class PassengerCreation
    {
        private readonly ScheduledRoutes _scheduledRoutes;

        public PassengerCreation(ScheduledRoutes scheduledRoutes)
        {
            _scheduledRoutes = scheduledRoutes;
        }

        public void AddEmployee(string enteredText)
        {
            string[] passengerSegments = enteredText.Split(' ');
            _scheduledRoutes.AddPassenger(new Passenger
            {
                Type = PassengerType.CarrierEmployee, 
                Name = passengerSegments[2], 
                Age = Convert.ToInt32(passengerSegments[3]),
            });
        }

        public void AddLoyalty(string enteredText)
        {
            string[] passengerSegments = enteredText.Split(' ');
            _scheduledRoutes.AddPassenger(new Passenger
            {
                Type = PassengerType.LoyaltyMember, 
                Name = passengerSegments[2], 
                Age = Convert.ToInt32(passengerSegments[3]),
                LoyaltyPoints = Convert.ToInt32(passengerSegments[4]),
                IsUsingLoyaltyPoints = Convert.ToBoolean(passengerSegments[5]),
            });
        }

        public void AddGeneral(string enteredText)
        {
            string[] passengerSegments = enteredText.Split(' ');
            _scheduledRoutes.AddPassenger(new Passenger
            {
                Type = PassengerType.General, 
                Name = passengerSegments[2], 
                Age = Convert.ToInt32(passengerSegments[3])
            });
        }
    }
}