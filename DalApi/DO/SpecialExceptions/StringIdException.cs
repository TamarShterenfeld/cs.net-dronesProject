using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// a class that treats in exceptions of StringId type.
    /// </summary>
    [Serializable]
    public class StringIdException : Exception
    {
        public string Id { get; set; }
        public StringIdException() : base() { }

        public StringIdException(string message, Exception inner) : base(message, inner) { }
        protected StringIdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public StringIdException(string str) { Id = str; }
        override public string ToString()
        {
            return "StringID Exception is thrown from DAL logic level because: " + Id + " isn't a valid value";
        }

    }
}
