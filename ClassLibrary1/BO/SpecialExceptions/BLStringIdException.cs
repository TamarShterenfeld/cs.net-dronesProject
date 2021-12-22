using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// a class that treats in exceptions of stringId type.
    /// </summary>
    [Serializable]
    public class BLStringIdException : Exception
    {
        public string Id { get; set; }
        public BLStringIdException() : base() { }

        public BLStringIdException(string message, Exception inner) : base(message, inner) { }
        protected BLStringIdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public BLStringIdException(string str) { Id = str; }
        override public string ToString()
        {
            return "StringId Exception is thrown from BL logic level because: " + Id + " isn't a valid value";
        }

    }
}