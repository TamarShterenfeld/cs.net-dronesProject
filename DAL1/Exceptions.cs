using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IDal
{
    namespace DO
    {
        [Serializable]
        public class OverloadException : Exception
        {
            public OverloadException() : base() { }
            public OverloadException(string message) : base(message) { }
            public OverloadException(string message, Exception inner) : base(message, inner) { }
            protected OverloadException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            // special constructor for our custom exception
            //will be develop in the next stages of the project.
            public OverloadException(int capacity, string message) : base(message)
            {
               
            }
            override public string ToString()
            {
                return "OverloadCapacityException: DAL capacity of " + " overloaded\n" + Message;
            }


        }
    }
}
