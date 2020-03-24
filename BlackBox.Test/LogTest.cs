using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BlackBox.Test
{
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
