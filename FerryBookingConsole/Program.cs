using System;
using FerryBooking.Core;

namespace FerryBookingProblem
{
    class Program
    {
        private static ScheduledRoutes _scheduledRoutes;

        static void Main(string[] args)
        {
            SetupVesselData();

            ShowInstructions();
            
            string command = "";
            do
            {
                command = Console.ReadLine() ?? "";
                var enteredText = command.ToLower();
                if (enteredText.Contains("print summary"))
                {
                    Console.WriteLine();
                    Console.WriteLine(_scheduledRoutes.GetSummary());
                }
                else if (enteredText.Contains("add general"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledRoutes.AddPassenger(new Passenger
                    {
                        Type = PassengerType.General, 
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3])
                    });
                }
                else if (enteredText.Contains("add loyalty"))
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
                else if (enteredText.Contains("add employee"))
                {
                    string[] passengerSegments = enteredText.Split(' ');
                    _scheduledRoutes.AddPassenger(new Passenger
                    {
                        Type = PassengerType.CarrierEmployee, 
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3]),
                    });
                }
                else if (enteredText.Contains("exit"))
                {
                    Environment.Exit(1);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("UNKNOWN INPUT");
                    Console.ResetColor();
                }
            } while (command != "exit");
        }
        
        private static void ShowInstructions()
        {
            Console.WriteLine("Commands to use the app:");
            Console.WriteLine("'print summary' : Displays the journey summary.");
            Console.WriteLine("'add general (insert name) (insert age)' : Add a general passenger.");
            Console.WriteLine("'add loyalty (insert name) (insert age) (insert loyaltyPoints) (true/false)' : Add a loyalty passenger and decide if they're using their points.");
            Console.WriteLine("'add employee (insert name) (insert age)' : Add a carrier employee passenger.");
            Console.WriteLine("'exit' : Exit the application.");
            Console.WriteLine();
        }

        private static void SetupVesselData()
        {
            Route doverToDunkirk = new Route("Dover", "Dunkirk")
            {
                BaseCost = 50, 
                BasePrice = 100, 
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };

            _scheduledRoutes = new ScheduledRoutes(doverToDunkirk);

            _scheduledRoutes.SetVesselForRoute(
                new Vessel { Id = 123, Name = "MV Ulysses", NumberOfSeats = 2000 });
        }
    }
}
