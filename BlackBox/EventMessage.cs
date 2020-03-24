namespace BlackBox
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Data transfer object holding an event message
    /// </summary>
    public class EventMessage
    {
        /// <summary>
        /// Event severity.
        /// </summary>
        public EventLevel Severity { get; private set; }

        /// <summary>
        /// Time stamp when the event happend.
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Service (Namespace and method name).
        /// </summary>
        public string Service { get; private set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Constructs event messages. Sets time stamp.
        /// </summary>
        public EventMessage(EventLevel severity, string message, [CallerMemberName]string service = "")
        {
            TimeStamp = DateTime.Now;
            Severity = severity;
            Service = service;
            Message = message;
        }

        /// <summary>
        /// Formats EventMessage to a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Concat(TimeStamp.ToString("yyyy-mm-dd hh:MM:ss"), " ", Severity.ToString(), ": ", Message, " @ ", Service);
        }
    }
}
