using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ParcelInPassing
{
    class ParcelInPassingViewModel
    {
        public ParcelInPassingViewModel()
        {
            PrioritiesArr = typeof(BO.Priorities).GetEnumValues();
            WeightArr = typeof(BO.WeightCategories).GetEnumValues();
        }
        public Array PrioritiesArr { get; set; }
        public Array WeightArr { get; set; }
    }
}
