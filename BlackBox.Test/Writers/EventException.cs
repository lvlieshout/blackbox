namespace BlackBox.Writers
{
    using System;

    public class EventException : Exception
    {
        public EventMessage EventMessage { get; private set; }

        public EventException()
        {
            EventMessage = new EventMessage(EventLevel.Error, "No message set.");
        }

        public EventException(EventMessage eventMessage)
        {
            EventMessage = eventMessage;
        }

        public EventException(string message) : base(message)
        {
            EventMessage = new EventMessage(EventLevel.Error, message);
        }

        public EventException(string message, Exception innerException) : base(message, innerException)
        {
            EventMessage = new EventMessage(EventLevel.Error, message);
        }
    }
}
