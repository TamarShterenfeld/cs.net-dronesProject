using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// a class that treats in exceptions of DroneStatuses type.
        /// </summary>
        [Serializable]
        public class DroneStatusException : Exception
        {
            public DroneStatuses Status { get; set; }
            public DroneStatusException() : base() { }
            public DroneStatusException(string message) : base(message) { }
            public DroneStatusException(string message, Exception inner) : base(message, inner) { }
            protected DroneStatusException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public DroneStatusException(DroneStatuses status) { Status = status; }
            override public string ToString()
            {
                return "DroneStatus Exception is thrown from BL logic level because: " + Status + " isn't a valid value";
            }


        }
    }
}
