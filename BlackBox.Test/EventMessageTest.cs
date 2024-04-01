using Xunit;

namespace BlackBox.Test
{
    public class EventMessageTest
    {
        [Fact]
        public void ConstructorWithTwoParameters()
        {
            string content = "content";
            string source = nameof(ConstructorWithTwoParameters);

            var message = new EventMessage(EventLevel.Info, content);

            Assert.Equal(EventLevel.Info, message.Level);
            Assert.Equal(content, message.Content);
            Assert.Contains(source, message.Source);
        }

        [Fact]
        public void ConstructorWithThreeParameters()
        {
            string content = "content";
            string source = "source";
            
            var message = new EventMessage(EventLevel.Info, content, source);

            Assert.Equal(EventLevel.Info, message.Level);
            Assert.Equal(content, message.Content);
            Assert.Equal(source, message.Source);
        }
    }
}
