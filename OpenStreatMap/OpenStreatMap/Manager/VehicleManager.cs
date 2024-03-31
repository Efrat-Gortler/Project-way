using OpenStreatMap.DTO;

using System;

namespace OpenStreatMap.Manager
{
    public class VehicleManager
    {
       
        public static VehicleParking CreateVehicleFromUserInput()
        {
            Console.WriteLine("Enter IdVehicle: ");
            string idVehicle = Console.ReadLine();
            Console.WriteLine("Enter GateNumber: ");
            string GateNumber = Console.ReadLine();

            // You may add more validation or error handling based on your requirements

            return new VehicleParking
            {
                IdVehicle = idVehicle,
                AtTime = DateTime.UtcNow, // Use DateTime.UtcNow for consistent time across the application
                TransitionType = false,
                GateNumber= GateNumber// Default value; you may modify this based on your requirements
            };
        }
    }
}

