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
                                ShowList(bl.GetBaseStationList());
                                break;
                            }
                        case (int)ShowingListsOptions.Drones:
                            {
                                ShowList(bl.GetDronesForLists());
                                break;
                            }
                        case (int)ShowingListsOptions.Customers:
                            {
                                ShowList(bl.GetCustomersList());
                                break;
                            }
                        case (int)ShowingListsOptions.Parcels:
                            {
                                ShowList(bl.GetParcelsList());
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
                                Console.WriteLine("ERROR! \nan unknown option." + "\n");
                                break;
                            }
                    }
                }
                //while getting data only IdExceptions may be thrown.
                catch (BLIntIdException exe)
                {
                    Console.WriteLine("the Id: " + exe.Id + " isn't valid in the BL logic level");

                }
                catch (BLStringIdException exe)
                {
                    Console.WriteLine("the Customer Id: " + exe.Id + " isn't valid in the BL logic level");
                }
                catch (IntIdException exe)
                {
                    Console.WriteLine("the Id: " + exe.Id + " isn't valid in the DAL logic level");
                }
                catch (StringIdException exe)
                {
                    Console.WriteLine("the Customer Id: " + exe.Id + " isn't valid in the DAL logic level");
                }
            }
            else Console.WriteLine("The show list option must hold a numeric value!");
        }


        public void ShowList(IEnumerable<BaseStationForList> baseStations)
        {
            foreach (var item in baseStations)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n");
        }

        public void ShowList(IEnumerable<DroneForList> drones)
        {
            foreach (var item in drones)
            {
                Console.WriteLine(item);
            }
        }

        public void ShowList(IEnumerable<ParcelForList> parcels)
        {
            foreach (var item in parcels)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n");
        }

        public void ShowList(IEnumerable<CustomerForList> customers)
        {
            foreach (var item in customers)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n");
        }
    }
}


