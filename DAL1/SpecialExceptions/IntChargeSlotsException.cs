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
        public class IntChargeSlotsException : Exception
        {
            public IntChargeSlotsException() : base() { }
            public IntChargeSlotsException(string message) : base(message) { }
            public IntChargeSlotsException(string message, Exception inner) : base(message, inner) { }
            protected IntChargeSlotsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public IntChargeSlotsException(int chargeSlots) { }
            override public string ToString()
            {
                return "Int Id Exceptioin : DAL logic level throws an int id exception " + Message;
            }

        }
    }
}