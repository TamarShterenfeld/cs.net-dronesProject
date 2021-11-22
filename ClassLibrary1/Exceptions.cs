using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class OverloadException : Exception
        {
            public OverloadException() : base() { }
            public OverloadException(string message) : base(message) { }
            public OverloadException(string message, Exception inner) : base(message, inner) { }
            protected OverloadException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            
            override public string ToString()
            {
                return "OverloadCapacityException: DAL capacity of " + " overloaded\n" + Message;
            }


        }
    }
}
