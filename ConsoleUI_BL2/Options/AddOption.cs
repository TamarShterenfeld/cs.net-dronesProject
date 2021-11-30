using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static ConsoleUI_BL.Program;
using IBL;


namespace ConsoleUI_BL
{
    /// <summary>
    /// Navigates the add choice appropriate to the user's choice.
    /// </summary>
    public class AddOption : ISubNavigate
    {
        public void Options(ref BL bl)
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
                                IBL.BO.BaseStation baseStation = new();
                                (baseStation.Id, baseStation.Name, baseStation.Location, baseStation.ChargeSlots) = InputBaseStationDetails();
                                bl.Add(baseStation);
                                break;
                            }

                        case (int)AddOptions.Drone:
                            {
                                int baseStationId;
                                Drone drone = new();
                                (drone.Id, drone.Model, drone.MaxWeight) = InputDroneDetails();
                                baseStationId = InputIntValue();
                                drone.Location = bl.GetBLBaseStation(baseStationId).Location;
                                bl.Add(drone, baseStationId);
                                break;
                            }
                        case (int)AddOptions.Customer:
                            {
                                IBL.BO.Customer customer = new();
                                (customer.Id, customer.Name, customer.Phone, customer.Location) = InputCustomerDetails();
                                bl.Add(customer);
                                break;
                            }
                        case (int)AddOptions.Parcel:
                            {
                                IBL.BO.Parcel parcel = new();
                                string sender , target ;
                                (sender, target, parcel.Weight, parcel.Priority) = InputParcelDetails();
                                parcel.Sender = bl.GetBLCustomrInParcel(sender);
                                parcel.Target = bl.GetBLCustomrInParcel(target);
                                parcel.MyDrone = null;
                                bl.Add(parcel);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("ERROR! \nan unknown option");
                                break;
                            }
                    }
                }
                catch (BLChargeSlotsException exe)
                {
                    Console.WriteLine("The ChargeSlots: " + exe.ChargeSlots + " isn't available!");
                }
                catch (BLIntIdException exe)
                {
                    Console.WriteLine("The id: " + exe.Id + " isn't valid!");
                }
                catch (BLLocationException exe)
                {
                    Console.WriteLine("The Location: " + exe.Location + "isn't valid"+"\nCoordinante value must be a positive number and in range of - 180º to 180º");
                }
                catch (BLStringIdException exe)
                {
                    Console.WriteLine("The Id : " + exe.Id + " isn't valid!");
                }
            }
            else Console.WriteLine("The add option must hold a numeric value!");
        }
    }
}
