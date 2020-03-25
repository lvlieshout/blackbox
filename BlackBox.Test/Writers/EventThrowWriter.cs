namespace BlackBox.Writers
{
    public class EventThrowWriter : EventQueueWriter
    {
        public override void Write(EventMessage message)
        {
            base.Write(message);
            throw new EventException(message);
        }
    }
}
