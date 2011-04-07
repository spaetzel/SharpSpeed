using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSpeed
{
    /// <summary>
    /// Base Exception class for errors thrown by SharpSpeed
    /// </summary>
    public class SharpSpeedException : Exception
    {
        /// <summary>
        /// Creates an instance of SharpSpeedException
        /// </summary>
        public SharpSpeedException(){}
        /// <summary>
        /// Creates an instance of SharpSpeedException
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="ex">A previous exception</param>
        public SharpSpeedException(string message , Exception ex) : base(message, ex) { }
    }

    /// <summary>
    /// Thrown in the event of failing to authorise a user
    /// </summary>
    public class SharpSpeedAuthorisationException : SharpSpeedException
    {
        public override string Message
        {
            get
            {
                return "dailymile authorization key has expired, is invalid or has not been set.  Re-authenticate the user";
            }
        }

        public SharpSpeedAuthorisationException(Exception ex = null):base(null, ex)
        {

        }
    }

    /// <summary>
    /// Thrown in the event a non existent key is referenced
    /// </summary>
    public class SharpSpeedNonExistentPersonException : SharpSpeedException
    {
        private string _key;
        public override string Message
        {
            get
            {
                return string.Format("No person found for username: {0}", _key);
            }
        }

        public SharpSpeedNonExistentPersonException (string username, Exception ex = null):base(null, ex)
	    {

            _key = username;
	    }

    }
}
