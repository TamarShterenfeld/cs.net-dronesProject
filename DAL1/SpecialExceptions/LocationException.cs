using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace DO
    {
        [Serializable]
        public class LocationException : Exception
        {
            public LocationException() : base() { }
            public LocationException(string message) : base(message) { }
            public LocationException(string message, Exception inner) : base(message, inner) { }
            protected LocationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public LocationException(double location) { }
            override public string ToString()
            {
                return "Int Id Exceptioin : DAL logic level throws an int id exception " + Message;
            }

        }
    }
}