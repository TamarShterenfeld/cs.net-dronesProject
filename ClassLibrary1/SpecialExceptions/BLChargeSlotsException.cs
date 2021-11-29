using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class BLChargeSlotsException : Exception
        {
            public int ChargeSlots { get; set; }
            public BLChargeSlotsException() : base() { }
            public BLChargeSlotsException(string message) : base(message) { }
            public BLChargeSlotsException(string message, Exception inner) : base(message, inner) { }
            protected BLChargeSlotsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public BLChargeSlotsException(int chargeSlots) { ChargeSlots = chargeSlots; }
            override public string ToString()
            {
                return "Int Id Exceptioin : DAL logic level throws an int id exception " + Message;
            }

        }
    }
}