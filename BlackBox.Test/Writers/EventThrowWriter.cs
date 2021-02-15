namespace BlackBox.Writers
{
    public class EventThrowWriter : EventQueueWriter
    {
        public override void Write(IEventMessage message)
        {
            base.Write(message);
            throw new EventException(message);
        }
    }
}
