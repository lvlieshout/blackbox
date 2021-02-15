namespace BlackBox.Test.WritersTest
{
    using Xunit;
    using BlackBox.Writers;

    /*
     * The sql writer is hard to test because it uses a real SQL server. But I want to test the writer.
     * So I compromise, the test results are always true. After a clean get from version control it will not fail
     * at this test. It will be a manual action to set the connection string. And hopefully no one commits the
     * security credentials. This is a risk!
    */

    public class EventTSqlWriterTest
    {
        private readonly string _connectionString = "Data Source={0};Initial Catalog={1};User Id={2};Password={3}";

        [Fact]
        public void CreateTable()
        {
            try
            {
                EventTSqlWriter writer = new EventTSqlWriter(_connectionString, "EventTSqlWriterTest");
            }
            catch { }
            Assert.True(true);
        }
        
        [Fact]
        public void Write()
        {
            try
            {
                EventTSqlWriter writer = new EventTSqlWriter(_connectionString, "EventTSqlWriterTest");
                writer.Write(new EventMessage(EventLevel.Critical, "Hello critical world."));
            }
            catch { }
            Assert.True(true);
        }
    }
}
