using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BlackBox.Test
{
    public class BlackBoxManagerTest
    {
        [Fact]
        public void CreateWriterTroughDelegate()
        {
            var logger = new BlackBoxManager();
            var writer = new EventMockWriter();
            logger.RegisterWriter(EventLevel.All, writer.Write);

            logger.Write(new EventMessage(EventLevel.Info, "Hello world!"));

            Assert.Equal(1, writer.Messages.Count);
        }
    }
}
