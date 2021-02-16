namespace BlackBox.Writers
{
    using System;
    using System.Data;

    /// <summary>
    /// .NET Data Provider parameter extensions.
    /// </summary>
    public static class IDbDataParameterExtensions
    {
        /// <summary>
        /// Create String type parameter for data provider.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IDbDataParameter AddParameter(this IDbCommand command, string name, string value, int size = 0)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = DbType.String;
            parameter.Direction = ParameterDirection.Input;
            if (size > 0) parameter.Size = size;
            command.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Create Short parameter for data provider,
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDbDataParameter AddParameter(this IDbCommand command, string name, short value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = DbType.Int16;
            parameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Create integer parameter for data provider,
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDbDataParameter AddParameter(this IDbCommand command, string name, int value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = DbType.Int32;
            parameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Create DateTime type parameter for data provider.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDbDataParameter AddParameter(this IDbCommand command, string name, DateTime value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = DbType.DateTime;
            parameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Create Boolean type parameter for data provider.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDbDataParameter AddParameter(this IDbCommand command, string name, bool value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = DbType.Boolean;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter);
            return parameter;
        }
    }
}
