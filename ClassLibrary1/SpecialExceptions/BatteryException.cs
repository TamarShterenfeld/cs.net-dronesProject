using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class BatteryException : Exception
        {
            public BatteryException() : base() { }
            public BatteryException(string message) : base(message) { }
            public BatteryException(string message, Exception inner) : base(message, inner) { }
            protected BatteryException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public BatteryException(double battery) { }
            override public string ToString()
            {
                return "OverloadCapacityException: DAL capacity of " + " overloaded\n" + Message;
            }


        }
    }
}
