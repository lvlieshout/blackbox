namespace BlackBox.Writers
{
    using Xunit;

    public class EventConsoleWriterTest
    {
        [Fact]
        public void WirteTest()
        {
            var logger = new BlackBoxManager();
            //var writer = new EventTestWriter();
            //logger.RegisterWriter(EventLevel.Critical, writer.Write);

            logger.Write(new EventMessage(EventLevel.Debug, "Hello Debug World!"));
            logger.Write(new EventMessage(EventLevel.Trace, "Hello Trace World!"));
            logger.Write(new EventMessage(EventLevel.Info, "Hello Info World!"));
            logger.Write(new EventMessage(EventLevel.Warning, "Hello Warning World!"));
            logger.Write(new EventMessage(EventLevel.Error, "Hello Error World!"));
            logger.Write(new EventMessage(EventLevel.Critical, "Hello Critical World!"));

        }
    }
}
