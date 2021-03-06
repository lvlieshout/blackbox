﻿namespace BlackBox
{
    using BlackBox.Writers;

    /// <summary>
    /// Extension methods to quickly set up logging.
    /// </summary>
    public static class BlackBoxManagerExtensions
    {
        /// <summary>
        /// Add Microsot SQL Server writer to the manager.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="level"></param>
        /// <param name="connectionString"></param>
        /// <param name="application"></param>
        /// <param name="keepConnectionOpen"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static IBlackBoxManager AddTSqlWriter(this IBlackBoxManager manager, string connectionString, EventLevel level = EventLevel.Info, string application = "", bool keepConnectionOpen = false, string tableName = "BlackBox")
        {
            manager.RegisterWriter(level, new EventTSqlWriter(connectionString, application, keepConnectionOpen, tableName).Write);
            return manager;
        }

        /// <summary>
        /// Add event file writer to the manager.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="level"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="maxSizeInKB"></param>
        /// <returns></returns>
        public static IBlackBoxManager AddFileWriter(this IBlackBoxManager manager, EventLevel level = EventLevel.Info, string path = "", string name = "log-", int maxSizeInKB = 1024)
        {
            manager.RegisterWriter(level, new EventFileWriter(path, name, maxSizeInKB).Write);
            return manager;
        }

        /// <summary>
        /// Add event file fallback writer to the manager.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="maxSizeInKB"></param>
        /// <returns></returns>
        public static IBlackBoxManager AddFileFallbackWriter(this IBlackBoxManager manager, string path = "", string name = "log-", int maxSizeInKB = 1024)
        {
            manager.RegisterFallbackWriter(new EventFileWriter(path, name, maxSizeInKB).Write);
            return manager;
        }

        /// <summary>
        /// Add event console writer to the manager.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static IBlackBoxManager AddConsoleWriter(this IBlackBoxManager manager, EventLevel level = EventLevel.Info)
        {
            manager.RegisterWriter(level, new EventConsoleWriter().Write);
            return manager;
        }

        /// <summary>
        /// Add event console fallback writer to the manager.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static IBlackBoxManager AddConsoleFallbackWriter(this IBlackBoxManager manager)
        {
            manager.RegisterFallbackWriter(new EventConsoleWriter().Write);
            return manager;
        }
    }
}
