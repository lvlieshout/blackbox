/*
 * Copyright (c) 2020 Lambert van Lieshout, YUMMO Software Development
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
*/

namespace BlackBox
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Event writer which outputs to Microsoft SQL Server.
    /// </summary>
    public class EventTSqlWriter : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly bool _keepConnectionOpen;
        private readonly string _application;
        private readonly string _tableName;

        /// <summary>
        /// Constructor of EventFileWriter. Set-up database connection and creates data table is not exist.
        /// </summary>
        /// <param name="connectionString">Connection string to the database</param>
        /// <param name="application">Application name to filter out when multiple application write to the database.</param>
        /// <param name="keepConnectionOpen">Keep connection open after writing to the database.</param>
        /// <param name="tableName">Table to log into, will be created if not exists.</param>
        public EventTSqlWriter(string connectionString, string application = "", bool keepConnectionOpen = false, string tableName = "BlackBox")
        {
            if (String.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be NULL or empty.");
            if (String.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName), "Table name cannot be NULL or empty.");
            _tableName = tableName;
            _application = application;
            _keepConnectionOpen = keepConnectionOpen;
            _connection = new SqlConnection(connectionString);
            if (_keepConnectionOpen) _connection.Open();
            CreateTable();
        }

        /// <summary>
        /// Write an event message to Microsoft SQL Server.
        /// </summary>
        /// <param name="message">EventMessage</param>
        public void Write(EventMessage message)
        {
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
#pragma warning disable CA2100 // Cannot use parameter for table name. Tablename is already checked in the CreateTable method.
                command.CommandText = "INSERT INTO [" + _tableName + "]([EventType],[TimeStamp],[Source],[Content],[Application]) VALUES (@EventType,@TimeStamp,@Source,@Content,@Application)";
#pragma warning disable CA2100 // Cannot use parameter for table name. Tablename is already checked in the CreateTable method.
                command.Parameters.Add("EventType", SqlDbType.SmallInt).Value = (short)message.Level;
                //command.Parameters.Add("Host", SqlDbType.VarChar, 255).Value = message.Host;
                command.Parameters.Add("TimeStamp", SqlDbType.DateTime).Value = message.TimeStamp;
                command.Parameters.Add("Source", SqlDbType.VarChar, 255).Value = message.Source;
                command.Parameters.Add("Content", SqlDbType.VarChar).Value = message.Content;
                command.Parameters.Add("Application", SqlDbType.VarChar, 255).Value = _application;
                if (command.ExecuteNonQuery() != 1) throw new Exception("BlackBox.Write : Error writing to black box database table");
            }
            if (!_keepConnectionOpen) _connection.Close();
        }

        /// <summary>
        /// Checks if log table exist in the database. If not then it will be created.
        /// </summary>
        private void CreateTable()
        {
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            int tableCount = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = _connection;
                command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName";
                command.Parameters.Add("TableName", SqlDbType.NVarChar).Value = _tableName;
                object o = command.ExecuteScalar();
                if (o is int) tableCount = (int)o;
                if (tableCount == 0)
                {
#pragma warning disable CA2100 // Since the previous query is succesfull executed the name must be valid. Also, tablename should be decided in code, not user input. 
                    command.CommandText = "CREATE TABLE [dbo].[" + _tableName + "] ([EventId] bigint IDENTITY(1, 1) NOT NULL,[EventType] smallint NOT NULL,[Host] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT (host_name()),[TimeStamp] datetime NOT NULL DEFAULT (getdate()),[Source] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,[Content] varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,[Application] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS,CONSTRAINT [PK__BlackBox__0CBAE877] PRIMARY KEY NONCLUSTERED ([EventId] ASC) WITH ( PAD_INDEX = OFF,FILLFACTOR = 100,IGNORE_DUP_KEY = OFF,STATISTICS_NORECOMPUTE = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];";
#pragma warning restore CA2100 // Since the previous query is succesfull executed the name must be valid. Also, tablename should be decided in code, not user input.
                    command.ExecuteNonQuery();
                }
            }
            if (!_keepConnectionOpen) _connection.Close();
        }

        /// <summary>
        /// Dispose EventTSqlWriter. Closes the database connection if open.
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed) _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
