using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDal.DO;


namespace IBL
{

    public partial class BL : IBL
    {
        private IDal.IDal dal;
        private List<DroneForList> dronesForList;

        public List<DroneForList> DronesForList 
        { get
            {
                return dronesForList;
            }
            set
            {
                dronesForList = value;
            }

        }

        public BL()
        {
            dal = new DalObject.DalObject();
            double[] droneElectricityInfo = dal.ElectricityConsuming();
            double electricityConsumingOfAvailable = droneElectricityInfo[0];
            double electricityConsumingOfLightWeight = droneElectricityInfo[0];
            double electricityConsumingOfHeavyWeight = droneElectricityInfo[0];
            double electricityConsumingOfAverageWeight = droneElectricityInfo[0];
            double chargeRate = droneElectricityInfo[0];
            List <BO.Drone> drones =( List<BO.Drone>)GetDronesList();
            for (int i = 0; i < drones.Count; i++)
            {
                if(drones[i].Parcel.)
            }
            
        }


    }
}
