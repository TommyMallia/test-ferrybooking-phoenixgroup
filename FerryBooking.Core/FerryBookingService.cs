using System;
using FerryBooking.Core;

namespace FerryBooking.Core
{
    public class FerryBookingService
    {
        public void SetupVesselData(ScheduledRoutes scheduledRoutes)
        {
            Route doverToDunkirk = new Route("Dover", "Dunkirk")
            {
                BaseCost = 50, 
                BasePrice = 100, 
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };

            scheduledRoutes.SetVesselForRoute(
                new Vessel { Id = 123, Name = "MV Ulysses", NumberOfSeats = 2000 });
        }

        public void ShowInstructions()
        {
            Console.WriteLine("Commands to use the app:");
            Console.WriteLine("'print summary' : Displays the journey summary.");
            Console.WriteLine("'add general (insert name) (insert age)' : Add a general passenger.");
            Console.WriteLine("'add loyalty (insert name) (insert age) (insert loyaltyPoints) (true/false)' : Add a loyalty passenger and decide if they're using their points.");
            Console.WriteLine("'add employee (insert name) (insert age)' : Add a carrier employee passenger.");
            Console.WriteLine("'exit' : Exit the application.");
            Console.WriteLine();
        }
    }
}