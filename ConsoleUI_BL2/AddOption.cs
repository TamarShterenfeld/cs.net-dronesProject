using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static ConsoleUI_BL.Program;
using static IBL.BO.WeightCategories;
using IBL;
using DAL.DO;
using IDal.DO;

namespace ConsoleUI_BL
{
    /// <summary>
    /// Navigates the add choice appropriate to the user's choice.
    /// </summary>
    public class AddOption : ISubNavigate
    {
        /// <inheritdoc />
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
                                IBL.BO.Drone drone = new();
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
                                parcel.Target = bl.GetBLCustomrInParcel(sender);
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
                catch (ChargeSlotsException exe)
                {
                    Console.WriteLine("The ChargeSlots: " + exe.ChargeSlots + " isn't available!");
                }
                catch (IntIdException exe)
                {
                    Console.WriteLine("The id: " + exe.Id + " isn't valid!");
                }
                catch (LocationException exe)
                {
                    Console.WriteLine("The Location: " + exe.Location + "isn't valid"+"\nCoordinante value must be a positive number and in range of - 180º to 180º");
                }
                catch (StringException exe)
                {
                    Console.WriteLine("The string : " + exe.Str + " isn't valid!");
                }
            }
            else Console.WriteLine("The add option must hold a numeric value!");
        }
    }
}
