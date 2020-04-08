namespace BlackBox.Test
{
    using System;
    using Xunit;
    using Xunit.Abstractions;
    using BlackBox.Writers;

    public class BlackBoxManagerTest
    {
        private readonly ITestOutputHelper _output;

        public BlackBoxManagerTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        [Fact]
        public void WriteOneMessage()
        {
            var logger = new BlackBoxManager();
            var queueWriter = new EventQueueWriter();
            logger.RegisterWriter(EventLevel.Critical, queueWriter.Write);

            logger.Write(new EventMessage(EventLevel.Critical, "Hello Critical World!"));

            Assert.Single(queueWriter.Messages);
        }

        [Fact]
        public void WriteOnlyCriticalMessage()
        {
            var logger = new BlackBoxManager();
            var queueWriter = new EventQueueWriter();
            logger.RegisterWriter(EventLevel.Critical, queueWriter.Write);

            logger.RegisterWriter(EventLevel.Error, t => { Console.WriteLine(t.Content); })

            logger.Write(new EventMessage(EventLevel.Debug, "Hello Debug World!"));
            logger.Write(new EventMessage(EventLevel.Trace, "Hello Trace World!"));
            logger.Write(new EventMessage(EventLevel.Info, "Hello Info World!"));
            logger.Write(new EventMessage(EventLevel.Warning, "Hello Warning World!"));
            logger.Write(new EventMessage(EventLevel.Error, "Hello Error World!"));
            logger.Write(new EventMessage(EventLevel.Critical, "Hello Critical World!"));

            Assert.Single(queueWriter.Messages);
        }

        [Fact]
        public void WriteAllLevelMessage()
        {
            var logger = new BlackBoxManager();
            var queueWriter = new EventQueueWriter();
            logger.RegisterWriter(EventLevel.Debug, queueWriter.Write);

            //logger.Write(new EventMessage(EventLevel.Debug, "Hello Debug World!")); // Only when debugger is attached!
            logger.Write(new EventMessage(EventLevel.Trace, "Hello Trace World!"));
            logger.Write(new EventMessage(EventLevel.Info, "Hello Info World!"));
            logger.Write(new EventMessage(EventLevel.Warning, "Hello Warning World!"));
            logger.Write(new EventMessage(EventLevel.Error, "Hello Error World!"));
            logger.Write(new EventMessage(EventLevel.Critical, "Hello Critical World!"));

            Assert.True(queueWriter.Messages.Count == 5);
        }

        [Fact]
        public void SkipDifferentLevelMessage()
        {
            var logger = new BlackBoxManager();
            var queueWriter = new EventQueueWriter();
            logger.RegisterWriter(EventLevel.Critical, queueWriter.Write);

            logger.Write(new EventMessage(EventLevel.Debug, "Hello Debug World!"));
            logger.Write(new EventMessage(EventLevel.Trace, "Hello Trace World!"));
            logger.Write(new EventMessage(EventLevel.Info, "Hello Info World!"));
            logger.Write(new EventMessage(EventLevel.Warning, "Hello Warning World!"));
            logger.Write(new EventMessage(EventLevel.Error, "Hello Error World!"));

            Assert.Empty(queueWriter.Messages);
        }

        [Fact]
        public void ErrornousWriter()
        {
            var logger = new BlackBoxManager();
            var queueWriter = new EventQueueWriter();
            var throwWriter = new EventThrowWriter();
            logger.RegisterFallbackWriter(queueWriter.Write);
            logger.RegisterWriter(EventLevel.Critical, throwWriter.Write);

            logger.Write(new EventMessage(EventLevel.Critical, "Hello Critical World!"));

            Assert.Single(throwWriter.Messages);
            Assert.True(queueWriter.Messages.Count == 2);
        }

        [Fact]
        public void ErrornousFallbackWriter()
        {
            var logger = new BlackBoxManager();
            var throwWriter = new EventThrowWriter();
            var throwFallbackWriter = new EventThrowWriter();
            logger.RegisterWriter(EventLevel.Critical, throwWriter.Write);
            logger.RegisterFallbackWriter(throwFallbackWriter.Write);

            logger.Write(new EventMessage(EventLevel.Critical, "Hello Critical World!"));

            Assert.Single(throwWriter.Messages);
            Assert.Single(throwFallbackWriter.Messages);
        }

        [Fact]
        public void MultipleWriters()
        {
            var logger = new BlackBoxManager();
            var queueWriter1 = new EventQueueWriter();
            var queueWriter2 = new EventQueueWriter();
            logger.RegisterWriter(EventLevel.Critical, queueWriter1.Write);
            logger.RegisterWriter(EventLevel.Critical, queueWriter2.Write);

            logger.Write(new EventMessage(EventLevel.Critical, "Hello Critical World!"));

            Assert.Single(queueWriter1.Messages);
            Assert.Single(queueWriter2.Messages);
        }
    }
}
