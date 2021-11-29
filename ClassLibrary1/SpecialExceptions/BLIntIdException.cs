using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// a class that treats in exceptions of int id type.
        /// </summary>
        [Serializable]
        public class BLIntIdException : Exception
        {
            public int Id { get; set; }
            public BLIntIdException() : base() { }
            public BLIntIdException(string message) : base(message) { }
            public BLIntIdException(string message, Exception inner) : base(message, inner) { }
            protected BLIntIdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public BLIntIdException(int id) { Id = id; }
            override public string ToString()
            {
                return "IntId Exception is thrown from BL logic level because: " + Id + " isn't a valid value";
            }

        }
    }
}