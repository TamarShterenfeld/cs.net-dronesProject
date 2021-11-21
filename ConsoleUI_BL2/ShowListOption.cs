using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace ConsoleUI_BL
{/// <summary>
 /// Navigates the showList choice appropriate to the customer's choice.
 /// </summary>
    public class ShowListOption : ISubNavigate
    {
        int innerChoice;
        public void options(ref IBL.BL bl)
        {
            Console.WriteLine("Please enter: \n1- For base stations list\n2- For drones list\n3- For customers list\n4- For parcels list\n5- For not associated parcels list\n6 - For base stations with available charge slots");
            if (int.TryParse(Console.ReadLine(), out innerChoice))
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
            else Console.WriteLine("The show list option must hold a numeric value!");
        }

        public void ShowList(IEnumerable<IBL.BO.BaseStation> baseStations)
        {
            foreach (var item in baseStations)
            {
                Console.WriteLine(item);
            }
        }

        public void ShowList(IEnumerable<IBL.BO.Drone> drones)
        {
            foreach (var item in drones)
            {
                Console.WriteLine(item);
            }
        }

        public void ShowList(IEnumerable<IBL.BO.Parcel> parcels)
        {
            foreach (var item in parcels)
            {
                Console.WriteLine(item);
            }
        }

        public void ShowList(IEnumerable<IBL.BO.Customer> customers)
        {
            foreach (var item in customers)
            {
                Console.WriteLine(item);
            }
        }
    }
}


