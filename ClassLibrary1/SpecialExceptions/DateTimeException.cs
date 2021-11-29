using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// a class that treats in exceptions of DateTime type.
        /// </summary>
        [Serializable]
        public class DateTimeException : Exception
        {
            public DateTime MyDateTime { get; set; }
            public DateTimeException() : base() { }
            public DateTimeException(string message) : base(message) { }
            public DateTimeException(string message, Exception inner) : base(message, inner) { }
            protected DateTimeException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public DateTimeException(DateTime dateTime) { MyDateTime = dateTime; }
            override public string ToString()
            {
                return "DateTime Exception is thrown from BL logic level because: " + MyDateTime + " isn't a valid value";
            }


        }
    }
}
