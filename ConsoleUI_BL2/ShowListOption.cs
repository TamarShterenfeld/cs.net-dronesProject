using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            ShowBaseStationsList();
                            break;
                        }
                    case (int)ShowingListsOptions.Drones:
                        {
                            ShowDronesList();
                            break;
                        }
                    case (int)ShowingListsOptions.Customers:
                        {
                            ShowCustomersList();
                            break;
                        }
                    case (int)ShowingListsOptions.Parcels:
                        {
                            ShowParcelsList();
                            break;
                        }
                    case (int)ShowingListsOptions.NotAssociatedParcels:
                        {
                            ShowNotAssociatedParcelsList();
                            break;
                        }
                    case (int)ShowingListsOptions.AvailableChargeSlots:
                        {
                            AvailableChargeSlots();
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
    }
}


