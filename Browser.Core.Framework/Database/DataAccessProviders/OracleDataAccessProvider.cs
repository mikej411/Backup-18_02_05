﻿// I had to comment this class out because it uses C#6.0 syntax (nameof). We are using TFS 2013 for build. The "nameof" is C#6.0 syntax, and TFS 2013 does not build with C#6.0 syntax

//using Oracle.ManagedDataAccess.Client;
//using System;
//using System.Data.Common;

//namespace Browser.Core.Framework
//{
//    /// <summary>
//    /// An Oracle implementation of the DataAccessProvider.
//    /// </summary>
//    public class OracleDataAccessProvider : DataAccessProvider
//    {
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="connectionString">The string used to connect to the data source.</param>
//        public OracleDataAccessProvider(string connectionString) : base(connectionString) { }

//        /// <summary>
//        /// Using the connection string, connect to a data source.
//        /// </summary>
//        /// <returns>A connection to the data source.</returns>
//        public override DbConnection CreateConnection()
//        {
//            return new OracleConnection(ConnectionString);
//        }

//        /// <summary>
//        /// Create a DataAdapter for the data source.
//        /// </summary>
//        /// <returns>A DataAdapter for the data source.</returns>
//        public override DbDataAdapter CreateAdapter()
//        {
//            return new OracleDataAdapter();
//        }

//        /// <summary>
//        /// Given a data adapter, create a single table command builder.
//        /// </summary>
//        /// <param name="adapter"></param>
//        /// <returns>A commmand builder.</returns>
//        public override DbCommandBuilder CreateCommandBuilder(DbDataAdapter adapter)
//        {
//            if (adapter == null) throw new ArgumentNullException(nameof(adapter));

//            return new OracleCommandBuilder((OracleDataAdapter)adapter);
//        }
//    }
//}
