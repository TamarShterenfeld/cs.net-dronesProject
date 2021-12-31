using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using static ConsoleUI_BL.Program;
using IBL;
using DO;


namespace ConsoleUI_BL
{
    /// <summary>
    /// Navigates the add choice appropriate to the user's choice.
    /// </summary>
    public class AddOption : ISubNavigate
    {
        public void Options(ref BLApi.IBL bl)
        {
            Console.WriteLine("Please enter : \n1- For Base Station \n2- For Drone\n3- For Customer\n4- For Parcel ");
            if (int.TryParse(Console.ReadLine(), out int innerChoice))
            {
                try
                {
                    switch (innerChoice)
                    {
                        case (int)AddOptions.BaseStation:
                            {
                                try
                                {
                                    BO.BaseStation baseStation = new();
                                    (baseStation.Id, baseStation.Name, baseStation.Location, baseStation.ChargeSlots) = InputBaseStationDetails();
                                    bl.Add(baseStation);
                                    break;
                                }
                                catch (StringException exe)
                                {
                                    Console.WriteLine("the String: " + exe.Str + " isn't valid in the DAL logic level");
                                }
                                catch (BLStringException exe)
                                {
                                    Console.WriteLine("The String: " + exe.Str + "isn't valid in the BL logic level ");
                                }
                                catch (LocationException exe)
                                {
                                    Console.WriteLine("the Location: " + exe.Location + " isn't valid in the DAL logic level" + "\nCoordinante value must be in range of - 180º to 180º");
                                }

                                catch (BLLocationException exe)
                                {
                                    Console.WriteLine("The Location: " + exe.Location + " isn't valid in the BL logic level" + "\nCoordinante value must be in range of - 180º to 180º");
                                }
                                catch (BLChargeSlotsException exe)
                                {
                                    Console.WriteLine("The ChargeSlots: " + exe.ChargeSlots + " isn't available in the BL logic level ");
                                }
                                catch (ChargeSlotsException exe)
                                {
                                    Console.WriteLine("the chargeSlots: " + exe.ChargeSlots + " isn't valid in the DAL logic level");
                                }
                                break;


                            }

                        case (int)AddOptions.Drone:
                            {
                                try
                                {
                                    int baseStationId;
                                    BO.Drone drone = new();
                                    (drone.Id, drone.Model, drone.MaxWeight) = InputDroneDetails();
                                    baseStationId = InputIntValue();
                                    drone.Location = bl.GetBLBaseStation(baseStationId).Location;
                                    bl.Add(drone, baseStationId);
                                }
                                catch (StringException exe)
                                {
                                    Console.WriteLine("the String: " + exe.Str + " isn't valid in the DAL logic level");
                                }
                                catch (BLStringException exe)
                                {
                                    Console.WriteLine("The String: " + exe.Str + "isn't valid in the BL logic level ");
                                }

                                catch (LocationException exe)
                                {
                                    Console.WriteLine("the Location: " + exe.Location + " isn't valid in the DAL logic level");
                                }

                                catch (BLLocationException exe)
                                {
                                    Console.WriteLine("The Location: " + exe.Location + " isn't valid in the BL logic level" + "\nCoordinante value must be a positive number and in range of - 180º to 180º");
                                }
                                break;
                            }
                        case (int)AddOptions.Customer:
                            {
                                try
                                {
                                    BO.Customer customer = new();
                                    (customer.Id, customer.Name, customer.Phone, customer.Location) = InputCustomerDetails();
                                    bl.Add(customer);
                                }
                                catch (StringException exe)
                                {
                                    Console.WriteLine("the String: " + exe.Str + " isn't valid in the DAL logic level");
                                }
                                catch (BLStringException exe)
                                {
                                    Console.WriteLine("The String: " + exe.Str + "isn't valid in the BL logic level ");
                                }

                                catch (LocationException exe)
                                {
                                    Console.WriteLine("the Location: " + exe.Location + " isn't valid in the DAL logic level");
                                }

                                catch (BLLocationException exe)
                                {
                                    Console.WriteLine("The Location: " + exe.Location + " isn't valid in the BL logic level" + "\nCoordinante value must be a positive number and in range of - 180º to 180º");
                                }
                                catch (BLPhoneException exe)
                                {
                                    Console.WriteLine("The Phone: " + exe.Phone + "isn't valid in the BL logic level ");
                                }
                                catch (PhoneException exe)
                                {
                                    Console.WriteLine("the Phone: " + exe.Phone + " isn't valid in the DAL logic level");
                                }
                                catch (StringIdException exe)
                                {
                                    Console.WriteLine("the Customer Id: " + exe.Id + " isn't valid in the DAL logic level");
                                }
                                break;
                            }
                        case (int)AddOptions.Parcel:
                            {
                                try
                                {
                                    BO.Parcel parcel = new();
                                    string sender, target;
                                    (sender, target, parcel.Weight, parcel.Priority) = InputParcelDetails();
                                    parcel.Sender = bl.GetCustomrInParcel(sender);
                                    parcel.Target = bl.GetCustomrInParcel(target);
                                    parcel.MyDrone = null;
                                    bl.Add(parcel);
                                }
                                catch (ParcelStatusException exe)
                                {
                                    Console.WriteLine("the Status: " + exe.ParcelStatus + " isn't valid  in the BL logic level");
                                }
                                catch (BLStringIdException exe)
                                {
                                    Console.WriteLine("The Customer Id: " + exe.Id + "isn't valid in the BL logic level ");
                                }
                                break;
                            }


                        default:
                            {
                                Console.WriteLine("ERROR! \nan unknown option");
                                break;
                            }
                    }
                }

                //all the cases may throw these exceptions:
                //these are common throws.
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
            else Console.WriteLine("The add option must hold a numeric value!");
        }
    }
}
