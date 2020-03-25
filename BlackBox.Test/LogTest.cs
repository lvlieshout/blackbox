namespace BlackBox.Test
{
    using System;
    using Xunit;

    public class LogTest
    {
        [Fact]
        public void LogNullWriter()
        {
            Log.Trace("Hello World!");
            Assert.True(true);
        }
    }
}
