using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static IDAL.DO.OverloadException;
using static IBL.BO.DalObject;


namespace IBL
{
    namespace BO
    {
        class DeliveryInCustomer
        {
            int id;
            public int Id
            {
                get { return id; }
                set
                {
                    if (value < 0)
                    {
                        throw new OverloadException("Id must contain a positive number");
                    }
                    id = value;
                }
            }
            public WeightCategories Weight { get; set; }
            public Priorities Priority;
            public ParcelStatuses ParcelStatus;
            public CustomerInShipment SourceOrDest { get; set; }
        }
    }
}
