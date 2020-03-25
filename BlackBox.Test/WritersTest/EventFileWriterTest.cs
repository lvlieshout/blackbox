namespace BlackBox.Test.WritersTest
{
    using System;
    using System.IO;
    using Xunit;
    using BlackBox.Writers;

    public class EventFileWriterTest
    {
        [Fact]
        public void WriteMessage()
        {
            var message = new EventMessage(EventLevel.Info, "Wello world info");
            string path = AppDomain.CurrentDomain.BaseDirectory;
            var writer = new EventFileWriter(path, "WriteMessage-", 1);
            writer.Write(message);

            string filename = Path.Combine(path, "WriteMessage-" + message.TimeStamp.ToString("yyyy-MM-dd")) + " (1).txt";
            Assert.True(File.Exists(filename));
            File.Delete(filename);
        }

        [Fact]
        public void CreateMultipleFiles()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            var message = new EventMessage(EventLevel.Info, "Wello world info".PadRight(1024, '0'));

            string filename1 = Path.Combine(path, "CreateMultipleFiles-" + message.TimeStamp.ToString("yyyy-MM-dd")) + " (1).txt";
            string filename2 = Path.Combine(path, "CreateMultipleFiles-" + message.TimeStamp.ToString("yyyy-MM-dd")) + " (2).txt";
            string filename3 = Path.Combine(path, "CreateMultipleFiles-" + message.TimeStamp.ToString("yyyy-MM-dd")) + " (3).txt";

            bool exist1 = File.Exists(filename1);
            bool exist2 = File.Exists(filename2);
            bool exist3 = File.Exists(filename3);

            if (exist1) File.Delete(filename1);
            if (exist2) File.Delete(filename2);
            if (exist3) File.Delete(filename3);

            var writer = new EventFileWriter(path, "CreateMultipleFiles-", 1);

            for(int i = 0; i < 2; i++)
            {
                writer.Write(message);
            }

            exist1 = File.Exists(filename1);
            exist2 = File.Exists(filename2);
            exist3 = File.Exists(filename3);

            if (exist1) File.Delete(filename1);
            if (exist2) File.Delete(filename2);
            if (exist3) File.Delete(filename3);

            Assert.True(exist1);
            Assert.True(exist2);
            Assert.False(exist3);
        }
    }
}
