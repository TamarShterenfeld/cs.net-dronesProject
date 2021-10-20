using System;
using static DalObject.DalObject;



namespace ConsoleUI
{
    public partial class Program
    {
        static void Main()
        { 
            int option, innerChoice;
            int id = 0, droneId = 0, parcelId = 0, baseStationId = 0;
            string name = "", customerId = "", phone = "", model = "", senderId = "", targetId = "";
            double longitude = 0, latitude = 0, battery = 0;
            int chrgeSlots = 0;
            //all the enum type litteral are entered as string type and then checked.
            string status = "", maxWeight = "", weight = "", priority = "";
            DateTime Production = new DateTime(), Association = new DateTime(), PickingUp = new DateTime(), Arrival = new DateTime();

            while (true)
            {
                Console.WriteLine("Please enter : \n1- For add\n2- For update\n3- For display\n4- For showing the lists\n5- For exit");
                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Please enter only a digit! Try again!");
                }
                try
                {
                    switch (option)
                    {
                        case (int)Options.Add:
                            {
                                Console.WriteLine("Please enter : \n1- For Base Station \n2- For Drone\n3- For Customer\n4- For Parcel ");
                                if (int.TryParse(Console.ReadLine(), out innerChoice))
                                {
                                    switch (innerChoice)
                                    {
                                        case (int)AddOptions.BaseStation:
                                            {
                                                CheckBaseStationDetails(ref id, ref name, ref longitude, ref latitude, ref chrgeSlots);
                                                AddingBaseStation(id, name, longitude, latitude, chrgeSlots);
                                                break;
                                            }

                                        case (int)AddOptions.Drone:
                                            {

                                                CheckDroneDetails(ref id, ref battery, ref model, ref maxWeight, ref status);
                                                AddingDrone(id, model, status, maxWeight, battery);
                                                break;
                                            }
                                        case (int)AddOptions.Customer:
                                            {
                                                CheckCustomerDetails(ref customerId, ref name, ref phone, ref longitude, ref latitude);
                                                AddingCustomer(ref customerId, ref name, ref phone, ref longitude, ref latitude);
                                                break;
                                            }
                                        case (int)AddOptions.Parcel:
                                            {
                                                CheckParcelDetails(ref customerId, ref senderId, ref targetId, ref weight, ref priority);
                                                AddingParcel(id, senderId, targetId, droneId, weight, priority, Production, Association, PickingUp, Arrival);
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("ERROR! \nan unknown option");
                                                break;
                                            }
                                    }

                                }
                                else Console.WriteLine("the add option must hold a numeric value!");
                                break;
                            }

                        case (int)Options.UpDate:
                            {
                                Console.WriteLine("Please enter : \n1- For associating package\n2- For picking up package\n3- For supply package\n4- For charging drone\n5- For stop drone charging ");
                                if (int.TryParse(Console.ReadLine(), out innerChoice))
                                {
                                    switch (innerChoice)
                                    {
                                        case (int)UpDateOptions.AssociatingParcel:
                                            {
                                                Console.WriteLine("Please enter the parcel's id and the drone's id");
                                                AssociatingParcel(parcelId, droneId);
                                                break;
                                            }
                                        case (int)UpDateOptions.PickingUpParcel:
                                            {
                                                Console.WriteLine("Please enter the parcel's id and the sender's id");
                                                PickingUpParcel(parcelId, senderId);
                                                break;
                                            }
                                        case (int)UpDateOptions.SupplyingParcel:
                                            {
                                                Console.WriteLine("Please enter the parcel's id and the target's id");
                                                SupplyingParcel(parcelId, targetId);
                                                break;
                                            }
                                        case (int)UpDateOptions.ChargingDrone:
                                            {
                                                Console.WriteLine("Please enter droneId and baseStationId");
                                                ChargingDrone(droneId, baseStationId);
                                                break;
                                            }
                                        case (int)UpDateOptions.StopDroneCharging:
                                            {
                                                Console.WriteLine("Please enter droneId and baseStationId");
                                                StopDroneCharging(droneId, baseStationId);
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("ERROR! \nan unknown option");
                                                break;
                                            }
                                    }

                                }
                                else Console.WriteLine("the add option must hold a numeric value!");
                                break;
                            }

                        case (int)Options.Display:
                            {
                                if (int.TryParse(Console.ReadLine(), out innerChoice))
                                {
                                    switch (innerChoice)
                                    {

                                        case (int)DisplayOptions.BaseStation:
                                            {
                                                Console.WriteLine("Please enter baseStationId");
                                                while (!int.TryParse(Console.ReadLine(), out baseStationId))
                                                {
                                                    Console.WriteLine("Id can contain only digits, Please try again!");
                                                }
                                                DisplayBaseStation(id);
                                                break;
                                            }

                                        case (int)DisplayOptions.Drone:
                                            {
                                                Console.WriteLine("Please enter droneId");
                                                while (!int.TryParse(Console.ReadLine(), out droneId))
                                                {
                                                    Console.WriteLine("Id can contain only digits, Please try again!");
                                                }
                                                DisplayDrone(id);
                                                break;
                                            }
                                        case (int)DisplayOptions.Customer:
                                            {
                                                Console.WriteLine("Please enter customerId");
                                                customerId = Console.ReadLine();
                                                DisplayCustomer(customerId);
                                                break;
                                            }
                                        case (int)DisplayOptions.Parcel:
                                            {
                                                Console.WriteLine("Please enter parcelId");
                                                while (!int.TryParse(Console.ReadLine(), out parcelId))
                                                {
                                                    Console.WriteLine("Id can contain only digits, Please try again!");
                                                }
                                                DisplayParcel(parcelId);
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("ERROR! \nan unknown option.");
                                                break;
                                            }
                                    }
                                }
                                else Console.WriteLine("The add option must hold a numeric value!");
                                break;
                            }


                        case (int)Options.ShowingLists:
                            {
                                if (int.TryParse(Console.ReadLine(), out innerChoice))
                                {

                                    switch (innerChoice)
                                    {
                                        case (int)ShowingListsOptions.BaseStations:
                                            {
                                                ShowingBaseStationsList();
                                                break;
                                            }
                                        case (int)ShowingListsOptions.Drones:
                                            {
                                                ShowingBDronesList();
                                                break;
                                            }
                                        case (int)ShowingListsOptions.Customers:
                                            {
                                                ShowingCustomersList();
                                                break;
                                            }
                                        case (int)ShowingListsOptions.Parcels:
                                            {
                                                ShowingParcelsList();
                                                break;
                                            }
                                        case (int)ShowingListsOptions.NotAssociatedParcels:
                                            {
                                                ShowingNotAssociatedParcelsList();
                                                break;
                                            }
                                        case (int)ShowingListsOptions.AvailableChargeSlots:
                                            {
                                                AvailableChargeSlots();
                                                break;
                                            }
                                    }
                                }
                                else Console.WriteLine("The add option must hold a numeric value!");

                                break;
                            }

                        case (int)Options.Exit:
                            {
                                System.Environment.Exit(0);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("ERROR! \nan unknown option, Please try again.");
                                while (!int.TryParse(Console.ReadLine(), out option))
                                {
                                    Console.WriteLine("Please enter a digit! Try again!");
                                }
                                break;
                            }
                    }
                }
                catch (FormatException exe)
                {
                    Console.WriteLine(exe.Message);
                }
            }



        }

    }


}

