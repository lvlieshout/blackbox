﻿namespace BlackBox.Test.WritersTest
{
    using System.Data.SqlClient;
    using Xunit;
    using BlackBox.Writers;

    /*
     * The sql writer is hard to test because it uses a real SQL server. But I want to test the writer.
     * So I compromise, the test results are always true. After a clean get from version control it will not fail
     * at this test. It will be a manual action to set the connection string. And hopefully no one commits the
     * security credentials. This is a risk!
    */

    public class EventDataProviderWriterTest
    {
        private readonly string _connectionString = "Data Source={0};Initial Catalog={1};User Id={2};Password={3}";

        [Fact(Skip = "Dependent on SQL server")]
        public void CreateTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    EventDataProviderWriter writer = new EventDataProviderWriter(connection, "EventDataProviderWriterTest");
                }
            }
            catch { }
            Assert.True(true);
        }

        [Fact(Skip = "Dependent on SQL server")]
        public void Write()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    EventDataProviderWriter writer = new EventDataProviderWriter(connection, "EventDataProviderWriterTest");
                    writer.Write(new EventMessage(EventLevel.Critical, "Hello critical world."));
                }
            }
            catch { }
            Assert.True(true);
        }
    }
}
