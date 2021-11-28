using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class DateTimeException : Exception
        {
            public DateTimeException() : base() { }
            public DateTimeException(string message) : base(message) { }
            public DateTimeException(string message, Exception inner) : base(message, inner) { }
            protected DateTimeException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public DateTimeException(DateTime dateTime) { }
            override public string ToString()
            {
                return "OverloadCapacityException: DAL capacity of " + " overloaded\n" + Message;
            }


        }
    }
}
