using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IBL;
using DAL.DO;

namespace ConsoleUI_BL
{/// <summary>
 /// Navigates the showList choice appropriate to the customer's choice.
 /// </summary>
    public class ShowListOption : ISubNavigate, IShowList
    {

        /// <inheritdoc />
        public void Options(ref BL bl)
        {
            Console.WriteLine("Please enter: \n1- For base stations list\n2- For drones list\n3- For customers list\n4- For parcels list\n5- For not associated parcels list\n6- For base stations with available charge slots");
            if (int.TryParse(Console.ReadLine(), out int innerChoice))
            {
                try
                {
                    switch (innerChoice)
                    {
                        case (int)ShowingListsOptions.BaseStations:
                            {
                                ShowList(bl.GetBOBaseStationsList());
                                break;
                            }
                        case (int)ShowingListsOptions.Drones:
                            {
                                ShowList(bl.GetBODronesList());
                                break;
                            }
                        case (int)ShowingListsOptions.Customers:
                            {
                                ShowList(bl.GetBOCustomersList());
                                break;
                            }
                        case (int)ShowingListsOptions.Parcels:
                            {
                                ShowList(bl.GetBOParcelsList());
                                break;
                            }
                        case (int)ShowingListsOptions.NotAssociatedParcels:
                            {
                                ShowList(bl.GetNotAssociatedParcelsList());
                                break;
                            }
                        case (int)ShowingListsOptions.AvailableChargeSlots:
                            {
                                ShowList(bl.GetAvailableChargeSlots());
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("ERROR! \nan unknown option.");
                                break;
                            }
                    }
                }
                catch (IntIdException exe)
                {
                    Console.WriteLine("The id: " + exe.Id + " isn't valid!");
                }
                catch (LocationException exe)
                {
                    Console.WriteLine("The Location: " + exe.Location + " isn't valid!");
                }
                catch (StringException exe)
                {
                    Console.WriteLine("The string : " + exe.Str + " isn't valid!");
                }
            }
            else Console.WriteLine("The show list option must hold a numeric value!");
        }


        /// <inheritdoc />
        public static void ShowList(IEnumerable<BaseStation> baseStations)
        {
            foreach (var item in baseStations)
            {
                Console.WriteLine(item);
            }
        }

        /// <inheritdoc />
        public static void ShowList(IEnumerable<Drone> drones)
        {
            foreach (var item in drones)
            {
                Console.WriteLine(item);
            }
        }

        /// <inheritdoc />
        public static void ShowList(IEnumerable<Parcel> parcels)
        {
            foreach (var item in parcels)
            {
                Console.WriteLine(item);
            }
        }

        /// <inheritdoc />
        public static void ShowList(IEnumerable<Customer> customers)
        {
            foreach (var item in customers)
            {
                Console.WriteLine(item);
            }
        }
    }
}


