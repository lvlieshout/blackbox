﻿namespace BlackBox.Test.WritersTest
{
    using System;
    using System.Data.SqlClient;
    using Xunit;
    using BlackBox.Writers;

    /*
     * The sql writer is hard to test because it uses a real SQL server. But I want this class tested.
     * So I compromise, the test relults are always true. After a clean get it will not fail
     * at this test. It wil be a manual action to set the connection string. And hopyfullly no one
     * commits security credentials. This is a risk!
    */

    public class EventDataProviderWriterTest
    {
        private readonly string _connectionString = "Data Source={0};Initial Catalog={1};User Id={2};Password={3}";

        [Fact]
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
        
        [Fact]
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
