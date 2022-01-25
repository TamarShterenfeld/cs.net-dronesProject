using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL
    {
        public void Delete(BO.BaseStation station)
        {
            station.IsDeleted = true;
            dal.UpDate(ConvertBaseStationBOtODO(station), station.Id);
        }
        public void Delete(BO.Customer customer)
        {
            DO.Customer curCustomer = dal.GetCustomer(customer.Id);
            curCustomer.IsDeleted = true;
            dal.UpDate(curCustomer, customer.Id);
        }

    }
}
