using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {

        /// <summary>
        /// convert ParcelInCustomer object from PO to BO.
        /// </summary>
        /// <param name="parcel">BO.ParcelInCustomer</param>
        /// <returns>PO.ParcelInCustomer</returns>
        public static PO.ParcelInCustomer ParcelInCustomerBOToPO(BO.ParcelInCustomer parcel)
        {
            return new PO.ParcelInCustomer(parcel.Id, (WeightCategories)Enum.Parse(typeof(WeightCategories), parcel.Weight.ToString()), (Priorities)Enum.Parse(typeof(Priorities), parcel.Priority.ToString()), (ParcelStatuses)Enum.Parse(typeof(ParcelStatuses), parcel.ParcelStatus.ToString()), CustomerInParcelBOTOPO(parcel.SourceOrDest));
        }

        /// <summary>
        /// convert ParcelInCustomer object from BO to PO.
        /// </summary>
        /// <param name="parcel">PO.ParcelInCustomer</param>
        /// <returns>BO.ParcelInCustomer</returns>
        public static BO.ParcelInCustomer ParcelInCustomerPOToBO(PO.ParcelInCustomer parcel)
        {
            return new BO.ParcelInCustomer(parcel.Id, (BO.WeightCategories)Enum.Parse(typeof(BO.WeightCategories), parcel.Weight.ToString()), (BO.Priorities)Enum.Parse(typeof(Priorities), parcel.Priority.ToString()), (BO.ParcelStatuses)Enum.Parse(typeof(ParcelStatuses), parcel.ParcelStatus.ToString()), CustomerInParcelPOTOBO(parcel.SourceOrDest));
        }

        /// <summary>
        /// convert ParcelInCustomer IEnumerable from PO to BO.
        /// </summary>
        /// <param name="parcels">BO.ParcelInCustomer IEnumerable</param>
        /// <returns>PO.ParcelInCustomer IEnumerable</returns>
        public static IEnumerable<PO.ParcelInCustomer> ParcelInCustomerListBOToPO(IEnumerable<BO.ParcelInCustomer> parcels)
        {
            if (parcels == null)
                return Enumerable.Empty<PO.ParcelInCustomer>();
            else
                return parcels.Select(parcel => ParcelInCustomerBOToPO(parcel));
        }

        public static IEnumerable<BO.ParcelInCustomer> ParcelInCustomerListPOToBO(IEnumerable<PO.ParcelInCustomer> parcels)
        {
            if (parcels == null)
                return Enumerable.Empty<BO.ParcelInCustomer>();
            else
                return parcels.Select(parcel => ParcelInCustomerPOToBO(parcel));
        }

    }
}

