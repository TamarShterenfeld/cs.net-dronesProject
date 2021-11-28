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
                return "Int Id Exceptioin : DAL logic level throws an int id exception " + Message;
            }

        }
    }
}