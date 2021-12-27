using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// a class that treats in exceptions of IntId type.
    /// </summary>
    [Serializable]
    public class IntIdException : Exception
    {
        public int Id { get; set; }
        public IntIdException() : base() { }
        public IntIdException(string message) : base(message) { }
        public IntIdException(string message, Exception inner) : base(message, inner) { }
        protected IntIdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public IntIdException(int id) { Id = id; }
        override public string ToString()
        {
            return "IntIdException Exception is thrown from DAL logic level because: " + Id + " isn't a valid value";
        }

    }
}
