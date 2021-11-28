using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace DO
    {
        [Serializable]
        public class ChargeSlotsException : Exception
        {
            public int ChargeSlots { get; set; }
            public ChargeSlotsException() : base() { }
            public ChargeSlotsException(string message) : base(message) { }
            public ChargeSlotsException(string message, Exception inner) : base(message, inner) { }
            protected ChargeSlotsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public ChargeSlotsException(int chargeSlots) { ChargeSlots = chargeSlots; }
            override public string ToString()
            {
                return "Int Id Exceptioin : DAL logic level throws an int id exception " + Message;
            }

        }
    }
}