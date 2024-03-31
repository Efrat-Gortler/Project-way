//using OpenStreatMap.DTO;

//namespace OpenStreatMap.Manager
//{
//    public class UserManager
//    {
//        public void enterCar(Passenger passenger)
//        {
//            //קליטת id ממצלמת לוחית רישוי-

//            Console.Write("Enter Type Passenger: ");
//            passenger.TypePassenger = Console.ReadLine();

//            // ל
//            Console.Write("Enter Type Passenger: ");


//            passenger.TypePassenger = Console.ReadLine();


//        }
//    }
//}
using System;
using OpenStreatMap.DTO;

public class UserManager
{
    public static User GetUserInput(List<string> targets)
    {
        Console.WriteLine("Destination List:");

        for (int i = 0; i < targets.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {targets[i]}");
        }

        Console.WriteLine("Enter the destination number you want to reach:");
        int selectedDestinationIndex;

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out selectedDestinationIndex) &&
                selectedDestinationIndex >= 1 && selectedDestinationIndex <= targets.Count)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid destination number.");
            }
        }

        string selectedDestination = targets[selectedDestinationIndex - 1];

        Console.WriteLine("Enter TypePassenger:");
        string typePassenger = Console.ReadLine();

        Console.WriteLine("Enter LicensePlateCar:");
        string licensePlateCar = Console.ReadLine();

        return new User
        {
            // Map values from the console input to the User object
            Destination = selectedDestination,
            TypePassenger = typePassenger,
            LicensePlateCar = licensePlateCar
        };
    }
}


