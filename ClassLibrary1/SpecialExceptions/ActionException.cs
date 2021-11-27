using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class ActionException : Exception
        {
            public ActionException() : base() { }
            public ActionException(string message) : base(message) { }
            public ActionException(string message, Exception inner) : base(message, inner) { }
            protected ActionException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public ActionException(DroneStatuses droneStatuses) { }
            override public string ToString()
            {
                return "OverloadCapacityException: DAL capacity of " + " overloaded\n" + Message;
            }


        }
    }
}
