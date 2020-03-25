namespace BlackBox.Test
{
    using System;
    using Xunit;
    using BlackBox.Writers;

    public class LogTest
    {
        [Fact]
        public void LogWriter()
        {
            var writer = new EventQueueWriter();
            Log.GetManager()
               .RegisterWriter(EventLevel.Debug, writer.Write);

            Log.Critical("Hello Critical World!");
            
            Assert.Single(writer.Messages);
        }
    }
}
