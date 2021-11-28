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
        public class StringIdException : Exception
        {
            public string Id { get; set; }
            public StringIdException() : base() { }

          //  public StringIdExceptioin(string message) : base(message) { }
            public StringIdException(string message, Exception inner) : base(message, inner) { }
            protected StringIdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public StringIdException(string str) { Id = str; }
            override public string ToString()
            {
                return "String id Exceptioin : DAL logic level throws a string id exception " + Message;
            }

        }
    }
}