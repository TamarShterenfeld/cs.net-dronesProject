using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// a class that treats in exceptions of Location type.
    /// </summary>
    [Serializable]
    public class LocationException : Exception
    {
        public double Location { get; set; }
        public LocationException() : base() { }
        public LocationException(string message) : base(message) { }
        public LocationException(string message, Exception inner) : base(message, inner) { }
        protected LocationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public LocationException(double location) { Location = location; }
        override public string ToString()
        {
            return "Location Exception is thrown from DAL logic level because: " + Location + " isn't a valid value";
        }

    }
}
