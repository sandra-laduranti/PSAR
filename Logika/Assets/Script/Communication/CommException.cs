using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Runtime;
using System.Runtime.Serialization;

    [Serializable()]
public class CommException : System.Exception
{
        public CommException() : base() { }
        public CommException(string message) : base(message) { }
        public CommException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected CommException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
}
