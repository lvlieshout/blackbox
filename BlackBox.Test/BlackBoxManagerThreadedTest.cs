namespace BlackBox.Test
{
    using System.Threading.Tasks;
    using System.Threading;
    using Xunit;
    using BlackBox.Writers;

    public class BlackBoxManagerThreadedTest
    {
        [Fact]
        public void WriteFromMultipleThreads()
        {
            const int threadCount = 10000;

            var logger = new BlackBoxManagerThreaded();
            var queueWriter = new EventQueueWriter();
            logger.RegisterWriter(EventLevel.Critical, queueWriter.Write);

            Task[] tasks = new Task[threadCount];
            for (int i = 0; i < tasks.Length; i++)
            {
                
                tasks[i] = Task.Factory.StartNew((n) =>
                {
                    logger.Write(new EventMessage(EventLevel.Critical, "Hello Critical World #" + n + " from thread id " + Thread.CurrentThread.ManagedThreadId + "!"));
                }, i);
            }

            Task.WaitAll(tasks);
            Assert.True(queueWriter.Messages.Count == threadCount);
        }
    }
}
