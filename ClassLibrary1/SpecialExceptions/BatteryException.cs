using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// a class that treats in exceptions of Battery type.
        /// </summary>
        [Serializable]
        public class BatteryException : Exception
        {
            public double Battery { get; set; }
            public BatteryException() : base() { }
            public BatteryException(string message) : base(message) { }
            public BatteryException(string message, Exception inner) : base(message, inner) { }
            protected BatteryException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public BatteryException(double battery) { Battery = battery; }
            override public string ToString()
            {
                return "Battery Exception is thrown from BL logic level because: " + Battery + " isn't a valid value";
            }
        }


        }
    }
}
