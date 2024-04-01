namespace BlackBox.Test.WritersTest
{
    using Xunit;
    using BlackBox.Writers;

    public class EventTSqlWriterTest
    {
        private readonly string _connectionString = "Data Source={0};Initial Catalog={1};User Id={2};Password={3}";

        [Fact(Skip = "Dependent on SQL server")]
        public void CreateTable()
        {
            EventTSqlWriter writer = new EventTSqlWriter(_connectionString, "EventTSqlWriterTest");

            Assert.True(true);
        }

        [Fact(Skip = "Dependent on SQL server")]
        public void Write()
        {
            EventTSqlWriter writer = new EventTSqlWriter(_connectionString, "EventTSqlWriterTest");
            writer.Write(new EventMessage(EventLevel.Critical, "Hello critical world."));

            Assert.True(true);
        }
    }
}
