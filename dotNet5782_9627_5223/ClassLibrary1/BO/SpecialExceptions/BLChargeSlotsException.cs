using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// a class that treats in exceptions of ChargeSlots type.
    /// </summary>
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
            return "ChargeSlots Exception is thrown from BL logic level because: " + ChargeSlots + " isn't a valid value";
        }

    }
}