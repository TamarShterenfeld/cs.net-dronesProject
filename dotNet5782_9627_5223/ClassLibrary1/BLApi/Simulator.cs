using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Math;

namespace IBL
{
    internal class Simulator
    {
        enum Maintenance { Starting, Going, Charging }

        private const double VELOCITY = 1.0;
        private const int DELAY = 500;
        private const double TIME_STEP = DELAY / 1000.0;
        private const double STEP = VELOCITY / TIME_STEP;

        public Simulator(BLApi.IBL Bl, int droneId, Action<object> updateDrone, Func<bool> checkStop)
        {
            BLApi.IBL bl = Bl;
            DalApi.IDal dal = DalApi.DalFactory.GetDal();
            DroneForList drone = bl.GetDroneForList(droneId);
            int parcelId = 0;
            BO.Parcel parcel = drone.ParcelId == 0? null: bl.GetBLParcel(drone.ParcelId);
            BO.BaseStation station = null;
            double distance = 0.0;
            int batteryUsage = 0;
            bool pickedUp = parcel != null && parcel.Id != 0 && parcel.PickUpDate != null ? true:false;
            BO.Customer customer = parcel != null && parcel.Id != 0 ? bl.GetBLCustomer(pickedUp ? parcel.Target.Id:parcel.Sender.Id) : null;
            Maintenance maintenance = drone.Status == DroneStatuses.Maintenance ? Maintenance.Charging : Maintenance.Starting;

            void startMaintenance()
            {
                drone.Status = DroneStatuses.Maintenance;
                maintenance = Maintenance.Starting;
            }
          

            do
            {
                switch (drone)
                {
                    case DroneForList { Status: DroneStatuses.Available }:
                        if (!sleepDelayTime()) 
                            break;
                        lock (bl)
                        {
                            try
                            {
                                lock (bl)
                                {
                                    parcel = bl.Associateparcel(drone);
                                    parcelId = parcel != default(BO.Parcel) ? parcel.Id : 0;
                                    switch (parcelId, drone.Battery)
                                    {
                                        case (default(int), 100):
                                            break;

                                        case (default(int), _):
                                            startMaintenance();
                                            break;
                                        case (_, _):
                                            try
                                            {
                                                customer = bl.GetBLCustomer(parcel.Sender.Id);
                                                bl.UpdateParcel(parcel);
                                            }
                                            catch (Exception ex) { throw new Exception("Internal error getting parcel", ex); }
                                            break;
                                    }
                                }
                            }
                            catch (ParcelActionsException ex)
                            {
                                startMaintenance();
                            }
                            catch (DroneStatusException ex) { startMaintenance(); }
                            catch (Exception ex){ startMaintenance(); }
                            }
                        break;

                    case DroneForList { Status: DroneStatuses.Maintenance } :
                        switch (maintenance)
                        {
                            case Maintenance.Starting:
                                lock (bl)
                                {
                                    try
                                    {
                                        station = bl.NearestBaseStation(drone, bl.GetBOBaseStationsList());
                                        //if all chargeSlots are cought.the drone with the highest battery leave the charge slot.
                                        if(station.DroneCharging.Count == station.ChargeSlots)
                                        {
                                            double maxBattery = station.DroneCharging.Max(item => item.Battery);
                                            DroneForList droneWithMaxBattery =bl.GetDroneForList(station.DroneCharging.Where(item => item.Battery == maxBattery).FirstOrDefault().Id);
                                            bl.ReleaseDroneFromRecharge(droneWithMaxBattery.Id);
                                            bl.UpdateDrone(droneWithMaxBattery);
                                        }
                                    }
                                    catch (IntIdException ex) { throw new IntIdException("Internal error base station", ex); }
                                    distance = drone.Distance(station);
                                    maintenance = Maintenance.Going;
                                }
                                break;

                            case Maintenance.Going:
                                if (distance < 1)
                                    lock (bl)
                                    {
                                        drone.Location = station.Location;
                                        maintenance = Maintenance.Charging;
                                        drone.Status = DroneStatuses.Available;
                                        bl.SendDroneForCharge(droneId);
                                    }
                                else
                                {
                                    if (!sleepDelayTime()) break;
                                    lock (bl)
                                    {
                                        double delta = distance < STEP ? distance : STEP;
                                        distance -= delta;
                                        drone.Battery = Max(0.0, drone.Battery - delta * bl.BatteryUsages[BL.DRONE_FREE]);
                                    }
                                }
                                break;

                            case Maintenance.Charging:
                                if (drone.Battery == 100)
                                {
                                    lock (dal)
                                    {
                                        dal.ReleaseDroneFromRecharge(drone.Id);
                                        drone.Status = DroneStatuses.Available;
                                        bl.UpdateDrone(drone);
                                        maintenance = Maintenance.Starting;
                                    }
                                } 
                                else
                                {
                                    if (!sleepDelayTime()) break;
                                    lock (bl) drone.Battery = Min(100, drone.Battery + bl.BatteryUsages[BL.DRONE_CHARGE] * TIME_STEP);
                                }
                                break;

                            default:
                                throw new Exception("Internal error: wrong maintenance substate");
                        }
                        break;

                    case DroneForList { Status: DroneStatuses.Shipment }:
                        
                        distance = drone.Distance(customer);
                        if (distance < 1 || drone.Battery == 0.0)
                            lock (bl)
                            {
                                drone.Location = customer.Location;
                                if (pickedUp)
                                {
                                    //parcel.SupplyDate = DateTime.Now;
                                    bl.SupplyParcel(drone.Id);
                                    //bl.UpdateParcel(parcel);
                                    drone.ParcelId = parcelId = 0;
                                    pickedUp = false;
                                }
                                else
                                {
                                    //parcel.PickUpDate = DateTime.Now;
                                    //bl.UpdateParcel(parcel);
                                    bl.PickUpParcel(drone.Id);
                                    customer = bl.GetBLCustomer(parcel.Target.Id);
                                    pickedUp = true;
                                }
                            }
                        else
                        {
                            if (!sleepDelayTime()) break;
                            lock (bl)
                            {
                                batteryUsage = (int)Enum.Parse(typeof(BatteryUsage), parcel?.Weight.ToString());
                                double delta = distance < STEP ? distance : STEP;
                                double proportion = delta / distance;
                                drone.Battery = Max(0.0, drone.Battery - delta * bl.BatteryUsages[pickedUp ? batteryUsage : BL.DRONE_FREE]);
                                double lat = drone.Location.CoorLatitude.InputCoorValue + (customer.Location.CoorLatitude.InputCoorValue - drone.Location.CoorLatitude.InputCoorValue) * proportion;
                                double lon = drone.Location.CoorLongitude.InputCoorValue + (customer.Location.CoorLongitude.InputCoorValue - drone.Location.CoorLongitude.InputCoorValue) * proportion;
                                drone.Location = new(new BO.Coordinate(lon, BO.Locations.Longitude), new BO.Coordinate(lat, BO.Locations.Latitude));
                            }
                        }
                        
                        break;

                    default:
                        throw new Exception("Internal error: not available after Delivery...");

                }
                lock (bl) { bl.UpdateDrone(drone); }
                updateDrone(new UserStage(distance,pickedUp,drone.Status,maintenance != Maintenance.Charging));
            } while (!checkStop());
        }

        private static bool sleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }
    }
}

