///*
// * Copyright (c) 2012 Lambert van Lieshout, YUMMO Software Development
// * 
// * Permission is hereby granted, free of charge, to any person obtaining a copy
// * of this software and associated documentation files (the "Software"), to
// * deal in the Software without restriction, including without limitation the
// * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// * sell copies of the Software, and to permit persons to whom the Software is
// * furnished to do so, subject to the following conditions:
// * 
// * The above copyright notice and this permission notice shall be included in
// * all copies or substantial portions of the Software.
// * 
// * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// * IN THE SOFTWARE.
//*/

//namespace BlackBox
//{
//    using System;
//    using System.Data;
//    using System.Data.SqlClient;
//    using System.Collections.Generic;
//    using System.Text;
//    using System.Configuration;

//    /// <summary>
//    /// Event writer which outputs to Microsoft SQL Server.
//    /// </summary>
//    public class EventTSqlWriter : IDisposable
//    {
//        private readonly SqlConnection  _connection;
//        private readonly bool           _keepConnectionOpen;
//        private readonly string         _application;
//        private readonly string         _tableName;

//        /// <summary>
//        /// Constructor of EventFileWriter. Set-up database connection and creates data table is not exist.
//        /// </summary>
//        /// <param name="application">Application name to filter out when multiple application write to the database.</param>
//        /// <param name="connectionString">Connection string to the database</param>
//        /// <param name="keepConnectionOpen">Keep connection open after writing to the database.</param>
//        public EventTSqlWriter(string application, string connectionString, bool keepConnectionOpen = false, string tableName = "BlackBox")
//        {
//            if (String.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString", "Connection string cannot be NULL or empty.");
//            if (String.IsNullOrEmpty(tableName)) throw new ArgumentNullException("tableName", "Table name cannot be NULL or empty.");
//            _tableName          = tableName;
//            _application        = application;
//            _keepConnectionOpen = keepConnectionOpen;
//            _connection         = new SqlConnection(connectionString);
//            if (_keepConnectionOpen) _connection.Open();
//            CreateTable();
//        }

//        /// <summary>
//        /// Write an event message to Microsoft SQL Server.
//        /// </summary>
//        /// <param name="message">EventMessage</param>
//        public void Write(EventMessage message)
//        {
//            if (_connection.State == ConnectionState.Closed) _connection.Open();
//            using (SqlCommand command = new SqlCommand())
//            {
//                command.Connection = _connection;
//                command.CommandText = "INSERT INTO [BlackBox]([EventType],[Host],[TimeStamp],[Service],[Parameters],[Data],[Thread],[Application]) VALUES (@EventType,@Host,@TimeStamp,@Service,@Parameters,@Data,@Thread,@Application)";
//                command.Parameters.Add("EventType", SqlDbType.SmallInt).Value = (short)message.EventType;
//                command.Parameters.Add("Host", SqlDbType.VarChar, 255).Value = message.Host;
//                command.Parameters.Add("TimeStamp", SqlDbType.DateTime).Value = message.TimeStamp;
//                command.Parameters.Add("Service", SqlDbType.VarChar, 255).Value = message.Service;
//                if (!String.IsNullOrEmpty(message.Parameters)) command.Parameters.Add("Parameters", SqlDbType.VarChar).Value = message.Parameters;
//                else command.Parameters.AddWithValue("Parameters", DBNull.Value);
//                command.Parameters.Add("Data", SqlDbType.VarChar).Value = message.Data;
//                command.Parameters.Add("Thread", SqlDbType.VarChar, 255).Value = message.Thread;
//                command.Parameters.Add("Application", SqlDbType.VarChar, 255).Value = _application;
//                if (command.ExecuteNonQuery() != 1) throw new Exception("BlackBox.Write : Error writing to black box database table");
//            }
//            if (!_keepConnectionOpen) _connection.Close();
//        }

//        /// <summary>
//        /// Checks if table 'BlackBox' exist in the database. If not then it will be created.
//        /// </summary>
//        private void CreateTable()
//        {
//            if (_connection.State == ConnectionState.Closed) _connection.Open();
//            int tableCount = 0;
//            using (SqlCommand command = new SqlCommand())
//            {
//                command.Connection = _connection;
//                command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BlackBox'";
//                object o = command.ExecuteScalar();
//                if (o is int) tableCount = (int)o;
//                if (tableCount == 0)
//                {
//                    command.CommandText = "CREATE TABLE [dbo].[BlackBox] ([EventId] bigint IDENTITY(1, 1) NOT NULL,[EventType] smallint NOT NULL,[Host] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT (host_name()),[TimeStamp] datetime NOT NULL DEFAULT (getdate()),[Service] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,[Parameters] varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,[Data] varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,[Thread] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS,[Application] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS,CONSTRAINT [PK__BlackBox__0CBAE877] PRIMARY KEY NONCLUSTERED ([EventId] ASC) WITH ( PAD_INDEX = OFF,FILLFACTOR = 100,IGNORE_DUP_KEY = OFF,STATISTICS_NORECOMPUTE = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];";
//                    command.ExecuteNonQuery();
//                }
//            }
//            if (!_keepConnectionOpen) _connection.Close();
//        }

//        /// <summary>
//        /// Create event writer from config settings using the following app settings entities: TSqlLogApplication, TSqlLogLevel and BlackBoxConnection as connectionstring.
//        /// </summary>
//        /// <param name="manager">BlackBox manager to register the writer.</param>
//        /// <returns>Created and registered EventTSqlWriter.</returns>
//        public static EventTSqlWriter RegisterFromConfig(BlackBoxManager manager)
//        {
//            if (ConfigurationManager.AppSettings["TSqlLogApplication"] == null)         throw new ArgumentNullException("Application not set in configuration");
//            if (ConfigurationManager.ConnectionStrings["BlackBoxConnection"] == null)   throw new ArgumentNullException("Database connection string not set in configuration");
//            if (ConfigurationManager.AppSettings["TSqlLogLevel"] == null)               throw new ArgumentNullException("EventLog level not set in configuration");
//            EventTypes eventLevel = BlackBoxUtilities.GetWriteLevelFromString(ConfigurationManager.AppSettings["TSqlLogLevel"]);
//            EventTSqlWriter writer = new EventTSqlWriter(ConfigurationManager.AppSettings["TSqlLogApplication"], ConfigurationManager.ConnectionStrings["BlackBoxConnection"].ConnectionString);
//            manager.RegisterWriter(eventLevel, writer.Write);
//            return writer;
//        }

//        /// <summary>
//        /// Dispose EventTSqlWriter. Closes the database connection if open.
//        /// </summary>
//        public void Dispose()
//        {
//            if (_connection != null)
//            {
//                if (_connection.State != ConnectionState.Closed) _connection.Close();
//                _connection.Dispose();
//            }
//        }
//    }
//}
