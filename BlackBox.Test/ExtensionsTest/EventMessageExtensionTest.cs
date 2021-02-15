using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using BlackBox;

namespace BlackBox.Test.ExtensionsTest
{
    public class EventMessageExtensionTest
    {
        [Fact]
        public void ExceptionToMessage()
        {
            string content = "Hello world";
            
            var ex = new Exception(content);
            var message = ex.ToMessage();

            Assert.Equal(EventLevel.Error, message.Level);
            Assert.Equal("System.RuntimeMethodHandle.InvokeMethod", message.Source);
            Assert.Equal("Exception: " + content, message.Content.Split(Environment.NewLine).First());
        }

        [Fact]
        public void ThrowToMessage()
        {
            string content = "Hello world";
            Exception exception;

            try
            {
                throw new Exception(content);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            var message = exception.ToMessage();
            Assert.Equal(EventLevel.Error, message.Level);
            Assert.Equal(this.GetType().FullName +  ".ThrowToMessage", message.Source);
            Assert.Equal("Exception: " + content, message.Content.Split(Environment.NewLine).First());
        }
    }
}
