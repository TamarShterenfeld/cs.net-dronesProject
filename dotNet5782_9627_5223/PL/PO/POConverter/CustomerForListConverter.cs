using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        //-----------------------CustomerForList Converting----------------------
        public static PO.CustomerForList CustomerForListBOToPO(BO.CustomerForList customer)
        {
            return new(customer);
        }

        public static IEnumerable<PO.CustomerForList> ListOFCustomerForListBOToPO(IEnumerable<BO.CustomerForList> customers)
        {
            return customers.Select(item=> CustomerForListBOToPO(item));
        }

    }
}
