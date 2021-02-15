namespace BlackBox.Writers
{
    using System;
    using Xunit.Abstractions;

    public class EventTestOutputWriter : EventConsoleWriter
    {
        private readonly ITestOutputHelper _output;

        public EventTestOutputWriter(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public override void Write(IEventMessage message)
        {
            _output.WriteLine(base.FormatMessage(message));
        }
    }
}
