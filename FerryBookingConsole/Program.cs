using System;
using FerryBooking.Core;

namespace FerryBookingProblem
{
    class Program
    {
        private static ScheduledRoutes _scheduledRoutes;
        private static FerryBookingService _ferryBookingService;
        private static PassengerCreation _passengerCreation;

        static void Main(string[] args)
        {
            _ferryBookingService = new FerryBookingService();
            _scheduledRoutes = new ScheduledRoutes(new Route("Dover", "Dunkirk"));
            _passengerCreation = new PassengerCreation(_scheduledRoutes);
            
            _ferryBookingService.SetupVesselData(_scheduledRoutes);
            _ferryBookingService.ShowInstructions();
            
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
                    _passengerCreation.AddGeneral(enteredText);
                }
                else if (enteredText.Contains("add loyalty"))
                {
                    _passengerCreation.AddLoyalty(enteredText);
                }
                else if (enteredText.Contains("add employee"))
                {
                    _passengerCreation.AddEmployee(enteredText);
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
    }
}
