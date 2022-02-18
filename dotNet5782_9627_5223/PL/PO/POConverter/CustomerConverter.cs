using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        //------------------------Customer Converting---------------------
        /// <summary>
        /// convert Customer object from PO to BO
        /// </summary>
        /// <param name="customer">BO.Customer</param>
        /// <returns>PO.Customer</returns>
        public static BO.Customer CustomerPoToBo(PO.Customer customer)
        {
            return new BO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Location = LocationPOTOBO(customer.Location),
                Phone = customer.Phone,
                FromCustomer = ParcelInCustomerListPOToBO(customer.FromCustomer).ToList(),
                ToCustomer = ParcelInCustomerListPOToBO(customer.ToCustomer).ToList(),
            };
        }

        public static PO.CustomerInParcel CustomerInParcelBOTOPO(BO.CustomerInParcel customer)
        {
            if (customer == null) return new PO.CustomerInParcel();
            return new PO.CustomerInParcel(customer.Id, customer.Name);
        }

        public static BO.CustomerInParcel CustomerInParcelPOTOBO(PO.CustomerInParcel customer)
        {
            return new BO.CustomerInParcel(customer.Id, customer.Name);
        }
    }
}
