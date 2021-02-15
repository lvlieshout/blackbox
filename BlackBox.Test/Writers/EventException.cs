namespace BlackBox.Writers
{
    using System;

    public class EventException : Exception
    {
        public IEventMessage EventMessage { get; private set; }

        public EventException()
        {
            EventMessage = new EventMessage(EventLevel.Error, "No message set.", StackTrace);
        }

        public EventException(IEventMessage eventMessage)
        {
            EventMessage = eventMessage;
        }

        public EventException(string message) : base(message)
        {
            EventMessage = new EventMessage(EventLevel.Error, message, StackTrace);
        }

        public EventException(string message, Exception innerException) : base(message, innerException)
        {
            EventMessage = new EventMessage(EventLevel.Error, message, StackTrace);
        }
    }
}
