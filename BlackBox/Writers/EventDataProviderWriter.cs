/*
 * Copyright (c) 2021 Lambert van Lieshout, YUMMO Software Development
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

namespace BlackBox.Writers
{
    using System;
    using System.Data;

    /// <summary>
    /// Event writer which outputs to .NET data provider.
    /// </summary>
    public class EventDataProviderWriter : IEventWriter
    {
        /// <summary>
        /// Data provider connection
        /// </summary>
        protected readonly IDbConnection _connection;

        /// <summary>
        /// Keep connection open. If false then Open eand Close are called at start en and of write.
        /// </summary>
        protected readonly bool _keepConnectionOpen;

        /// <summary>
        /// Value for application column.
        /// </summary>
        protected readonly string _application;

        /// <summary>
        /// Data table to write into, default is BlackBox.
        /// </summary>
        protected readonly string _tableName;

        /// <summary>
        /// Constructor of EventFileWriter. Set-up database connection and creates data table is not exist.
        /// </summary>
        /// <param name="connection">Connection to the data store.</param>
        /// <param name="application">Application name to filter out when multiple application write to the database.</param>
        /// <param name="keepConnectionOpen">Keep connection open after writing to the database.</param>
        /// <param name="tableName">Table to log into, will be created if not exists.</param>
        public EventDataProviderWriter(IDbConnection connection, string application = "", bool keepConnectionOpen = false, string tableName = "BlackBox")
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be NULL.");
            if (String.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName), "Table name cannot be NULL or empty.");
            _tableName = tableName;
            _application = application;
            _keepConnectionOpen = keepConnectionOpen;
            if (_keepConnectionOpen) _connection.Open();
            CreateTable();
        }

        /// <summary>
        /// Write an event message to Microsoft SQL Server.
        /// </summary>
        /// <param name="message">EventMessage</param>
        public virtual void Write(IEventMessage message)
        {
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            using (IDbCommand command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                command.CommandText = "INSERT INTO [" + _tableName + "]([EventType],[TimeStamp],[Source],[Content],[Application]) VALUES (@EventType,@TimeStamp,@Source,@Content,@Application)";
                command.AddParameter("EventType", (short)message.Level);
                command.AddParameter("TimeStamp", message.TimeStamp);
                command.AddParameter("Source", message.Source, 255);
                command.AddParameter("Content", message.Content);
                command.AddParameter("Application", _application, 255);
                if (command.ExecuteNonQuery() != 1) throw new Exception("BlackBox.Write : Error writing to black box data store table");
            }
            if (!_keepConnectionOpen) _connection.Close();
        }

        /// <summary>
        /// Checks if log table exist in the database. If not then it will be created.
        /// </summary>
        protected virtual void CreateTable()
        {
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            int tableCount = 0;
            using (IDbCommand command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName";
                command.AddParameter("TableName", _tableName);
                object result = command.ExecuteScalar();
                if (result is int resultInt)
                {
                    tableCount = resultInt;
                }
                if (tableCount == 0)
                {
                    command.CommandText = "CREATE TABLE [dbo].[" + _tableName + "] ([EventId] bigint IDENTITY(1, 1) NOT NULL,[EventType] smallint NOT NULL,[Host] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT (host_name()),[TimeStamp] datetime NOT NULL DEFAULT (getdate()),[Source] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,[Content] varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,[Application] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS,CONSTRAINT [PK__BlackBox__0CBAE877] PRIMARY KEY NONCLUSTERED ([EventId] ASC) WITH ( PAD_INDEX = OFF,FILLFACTOR = 100,IGNORE_DUP_KEY = OFF,STATISTICS_NORECOMPUTE = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];";
                    command.ExecuteNonQuery();
                }
            }
            if (!_keepConnectionOpen) _connection.Close();
        }
    }
}
