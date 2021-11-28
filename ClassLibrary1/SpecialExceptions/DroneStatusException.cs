using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class DroneStatusException : Exception
        {
            public DroneStatusException() : base() { }
            public DroneStatusException(string message) : base(message) { }
            public DroneStatusException(string message, Exception inner) : base(message, inner) { }
            protected DroneStatusException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public DroneStatusException(DroneStatuses status) { }
            override public string ToString()
            {
                return "OverloadCapacityException: DAL capacity of " + " overloaded\n" + Message;
            }


        }
    }
}
