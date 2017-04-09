using AutomatedValetParking.Entities;
using AutomatedValetParking.Entities.Exception;
using AutomatedValetParking.Services.IManager;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AutomatedValetParking.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Loading the unity configuration and registering all the dependencies
            var container = LoadUnityConfiguration();
            var rows = 0;
            var columns = 0;
            var reservedRows = new List<int>();

            // Load the system configuration from the configuration file
            if (!LoadConfiguration(ref rows, ref columns, ref reservedRows))
            {
                return;
            }

            var manager = container.Resolve<IParkingLotManager>();
            // Setting up Parking lot system to start parking
            try
            {
                manager.SetupSystem(rows, columns, reservedRows);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Welcome to Automated Valet Parking");
            Console.WriteLine("------------------------------------------------------------");
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Please enter S for Standard parking or R for reserved parking and press Q to exit");
                var key = Console.ReadLine();

                switch (key?.ToUpper())
                {
                    case "S":
                        ParkVehicle(manager, ParkingType.Standard);
                        break;
                    case "R":
                        ParkVehicle(manager, ParkingType.Reserved);
                        break;
                    case "Q":
                        exit = true;
                        break;
                    default:
                        break;
                }
                Console.WriteLine();
            }


            Console.ReadKey();
        }

        /// <summary>
        /// Loads the configuration for the Parking system from configuration file
        /// </summary>
        /// <param name="rows">Rows</param>
        /// <param name="columns">Columns</param>
        /// <param name="reservedRows">Reserved rows</param>
        /// <returns>Whether configuration is loaded successfully or not</returns>
        private static bool LoadConfiguration(ref int rows, ref int columns, ref List<int> reservedRows)
        {
            try
            {
                rows = Convert.ToInt32(ConfigurationManager.AppSettings["ParkingRows"]);
                columns = Convert.ToInt32(ConfigurationManager.AppSettings["ParkingColumns"]);
                var reserved = ConfigurationManager.AppSettings["ReservedParkingRows"]?.ToString();
                if (!string.IsNullOrEmpty(reserved))
                {
                    reservedRows = (from r in reserved.Split(',').ToList()
                                    select Convert.ToInt32(r)).ToList();
                }
                return true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please provide appropriate rows and columns in the configuration!!");
                Console.ReadKey();
                return false;
            }
        }

        /// <summary>
        /// Helper method to park a vehicle to its desired type
        /// </summary>
        /// <param name="manager">Parking lot manager</param>
        /// <param name="type">Parking type</param>
        private static void ParkVehicle(IParkingLotManager manager, ParkingType type)
        {
            try
            {
                // Print the booking number if vehicle is parked successfully
                Console.WriteLine(string.Format("Ticket number: {0}", manager.ParkVehicle(type)));
            }
            catch (ParkingSpaceNotAvailableException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Loads the unity configuration and resolves all the dependencies
        /// </summary>
        /// <returns>Unity container</returns>
        private static UnityContainer LoadUnityConfiguration()
        {
            var container = new UnityContainer();
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Containers.Default.Configure(container);

            return container;
        }
    }
}
