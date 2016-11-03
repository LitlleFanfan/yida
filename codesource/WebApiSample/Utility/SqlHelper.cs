using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Xml;

namespace WebApiSample.Utility
{
    /// <summary>
    ///     The SqlHelper class is intended to encapsulate high performance, scalable best practices for
    ///     common uses of SqlClient.
    /// </summary>
    public sealed class SqlHelper
    {
        static Dictionary<string, string> connectionDictionary = CallContext.LogicalGetData("dataConnectionStr") as Dictionary<string, string>;

        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private static readonly int timeout = int.Parse(ConfigurationManager.AppSettings["dbTimeOut"],CultureInfo.InvariantCulture);

        #region private utility methods & constructors

        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new SqlHelper()".
        private SqlHelper()
        {
        }


        /// <summary>
        ///     This method is used to attach array of SqlParameters to a SqlCommand.
        ///     This method will assign a value of DbNull to any parameter with a direction of
        ///     InputOutput and a value of null.
        ///     This behavior will prevent default values from being used, but
        ///     this will be the less common case than an intended pure output parameter (derived as InputOutput)
        ///     where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">an array of SqlParameters tho be added to command</param>
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (var p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                command.Parameters.Add(p);
            }
        }

        /// <summary>
        ///     This method assigns an array of values to an array of SqlParameters.
        /// </summary>
        /// <param name="commandParameters">array of SqlParameters to be assigned values</param>
        /// <param name="parameterValues">array of objects holding the values to be assigned</param>
        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                //do nothing if we get no data
                return;
            }

            // we must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            //iterate through the SqlParameters, assigning the values from the corresponding position in the 
            //value array
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                commandParameters[i].Value = parameterValues[i];
            }
        }

        /// <summary>
        ///     This method opens (if necessary) and assigns a connection, transaction, command type and parameters
        ///     to the provided command.
        /// </summary>
        /// <param name="command">the SqlCommand to be prepared</param>
        /// <param name="connection">a valid SqlConnection, on which to execute this command</param>
        /// <param name="transaction">a valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">
        ///     an array of SqlParameters to be associated with the command or 'null' if no parameters
        ///     are required
        /// </param>
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction,
            CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //associate the connection with the command
            command.Connection = connection;

            //set the command timeout value
            command.CommandTimeout = timeout;

            //set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        #endregion private utility methods & constructors

        #region ExecuteNonQuery

        /// <summary>
        ///     Execute a SqlCommand (that returns no resultset and takes no parameters) against the database specified in
        ///     the connection string.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(connectionString, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns no resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid",
        ///     24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(cn, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in
        ///     the connection string using the provided parameter values.  This method will query the database to discover the
        ///     parameters for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="spName">the name of the stored prcedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlHelperParameterCache.GetStoreProcedureParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(connection, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns no resultset) against the specified SqlConnection
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            using (var cmd = new SqlCommand())
            {
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                if (commandType == CommandType.StoredProcedure) //added by Zhan XuDong 18/07/2005
                    cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                        false, 0, 0, String.Empty, DataRowVersion.Default, null));

                //finally, execute the command.
                var retval = cmd.ExecuteNonQuery();

                if (commandType == CommandType.StoredProcedure) //added by Zhan XuDong 18/07/2005
                    retval = (int)cmd.Parameters["ReturnValue"].Value;

                // detach the SqlParameters from the command object, so they can be used again.
                cmd.Parameters.Clear();
                return retval;
            }
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection
        ///     using the provided parameter values.  This method will query the database to discover the parameters for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlTransaction.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns no resultset) against the specified SqlTransaction
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //create a command and prepare it for execution
            using (var cmd = new SqlCommand())
            {
                PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                //finally, execute the command.
                var retval = cmd.ExecuteNonQuery();

                // detach the SqlParameters from the command object, so they can be used again.
                cmd.Parameters.Clear();
                return retval;
            }
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified
        ///     SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters
        ///     for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(transaction.Connection.ConnectionString,
                        spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns no resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid",
        ///     24));
        /// </remarks>
        /// <param name="spName">the stored procedure name or T-SQL command </param>
        /// <param name="parameterValues">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string spName, SqlParameter[] parameterValues)
        {
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, parameterValues);
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataSet

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
        ///     the connection string.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     DataSet ds = ExecuteDataSet(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteDataSet(connectionString, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     DataSet ds = ExecuteDataSet(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteDataSet(cn, commandType, commandText, commandParameters);
            }
        }


        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     DataSet ds = ExecuteDataSet(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteDataSet(connection, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     DataSet ds = ExecuteDataSet(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            using (var cmd = new SqlCommand())
            {
                cmd.CommandTimeout = 120;
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                //create the DataAdapter & DataSet
                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    ds.Locale = CultureInfo.InvariantCulture;

                    //fill the DataSet using default values for DataTable names, etc.
                    da.Fill(ds);

                    // detach the SqlParameters from the command object, so they can be used again.			
                    cmd.Parameters.Clear();

                    //return the dataset
                    return ds;
                }
            }
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        ///     using the provided parameter values.  This method will query the database to discover the parameters for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     DataSet ds = ExecuteDataSet(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteDataSet(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteDataSet(connection, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     DataSet ds = ExecuteDataSet(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteDataSet(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     DataSet ds = ExecuteDataSet(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //create a command and prepare it for execution
            using (var cmd = new SqlCommand())
            {
                PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                //create the DataAdapter & DataSet
                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    ds.Locale = CultureInfo.InvariantCulture;

                    //fill the DataSet using default values for DataTable names, etc.
                    da.Fill(ds);

                    // detach the SqlParameters from the command object, so they can be used again.
                    cmd.Parameters.Clear();

                    //return the dataset
                    return ds;
                }
            }
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
        ///     SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters
        ///     for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     DataSet ds = ExecuteDataSet(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(transaction.Connection.ConnectionString,
                        spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteDataSet(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteDataSet(transaction, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     DataSet ds = ExecuteDataSet(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="spName">the stored procedure name or T-SQL command</param>
        /// <param name="parameterValues">an array of SqlParamters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(string spName, SqlParameter[] parameterValues)
        {
            return ExecuteDataSet(connectionString, CommandType.StoredProcedure, spName, parameterValues);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <param name="returnValue">return Value</param>
        /// <param name="spName">the stored procedure name or T-SQL command</param>
        /// <param name="parameterValues">an array of SqlParamters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static DataSet ExecuteDataSetWithRet(ref int returnValue, string spName, SqlParameter[] parameterValues)
        {
            return ExecuteDataSetWithRet(ref returnValue, connectionString, CommandType.StoredProcedure, spName,
                parameterValues);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <param name="returnValue">return Value</param>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static DataSet ExecuteDataSetWithRet(ref int returnValue, string connectionString,
            CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                //create a command and prepare it for execution
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandTimeout = 120;
                    PrepareCommand(cmd, cn, null, commandType, commandText, commandParameters);
                    cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                        false, 0, 0, String.Empty, DataRowVersion.Default, null));

                    //create the DataAdapter & DataSet
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        //fill the DataSet using default values for DataTable names, etc.
                        var ds = new DataSet();
                        ds.Locale = CultureInfo.InvariantCulture;
                        da.Fill(ds);

                        returnValue = (int)cmd.Parameters["ReturnValue"].Value;

                        // detach the SqlParameters from the command object, so they can be used again.			
                        cmd.Parameters.Clear();

                        //return the dataset
                        return ds;
                    }
                }
            }
        }

        #endregion ExecuteDataSet

        #region ExecuteReader

        /// <summary>
        ///     this enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
        ///     we can set the appropriate CommandBehavior when calling ExecuteReader()
        /// </summary>
        private enum SqlConnectionOwnership
        {
            /// <summary>Connection is owned and managed by SqlHelper</summary>
            Internal,

            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        /// <summary>
        ///     Create and prepare a SqlCommand, and call ExecuteReader with the appropriate CommandBehavior.
        /// </summary>
        /// <remarks>
        ///     If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
        ///     If the caller provided the connection, we want to leave it to them to manage.
        /// </remarks>
        /// <param name="connection">a valid SqlConnection, on which to execute this command</param>
        /// <param name="transaction">a valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">
        ///     an array of SqlParameters to be associated with the command or 'null' if no parameters
        ///     are required
        /// </param>
        /// <param name="connectionOwnership">
        ///     indicates whether the connection parameter was provided by the caller, or created by
        ///     SqlHelper
        /// </param>
        /// <returns>SqlDataReader containing the results of the command</returns>
        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction,
            CommandType commandType, string commandText, SqlParameter[] commandParameters,
            SqlConnectionOwnership connectionOwnership)
        {
            //create a command and prepare it for execution
            using (var cmd = new SqlCommand())
            {
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);

                //create a reader
                SqlDataReader dr;

                // call ExecuteReader with the appropriate CommandBehavior
                if (connectionOwnership == SqlConnectionOwnership.External)
                {
                    dr = cmd.ExecuteReader();
                }
                else
                {
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }

                // detach the SqlParameters from the command object, so they can be used again.
                cmd.Parameters.Clear();

                return dr;
            }
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
        ///     the connection string.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteReader(connectionString, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid",
        ///     24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            //create & open a SqlConnection
            var cn = new SqlConnection(connectionString);
            cn.Open();

            try
            {
                //call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(cn, null, commandType, commandText, commandParameters,
                    SqlConnectionOwnership.Internal);
            }
            catch
            {
                //if we fail to return the SqlDatReader, we need to close the connection ourselves
                cn.Close();
                throw;
            }
        }

        ///// <summary>
        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        ///// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        ///// </summary>
        ///// <remarks>
        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
        ///// 
        ///// e.g.:  
        /////  SqlDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
        ///// </remarks>
        ///// <param name="connectionString">a valid connection string for a SqlConnection</param>
        ///// <param name="spName">the name of the stored procedure</param>
        ///// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        ///// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        //public static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues) {
        //    //if we receive parameter values, we need to figure out where they go
        //    if ((parameterValues != null) && (parameterValues.Length > 0)) {
        //        //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        //        SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

        //        //assign the provided values to these parameters based on parameter order
        //        AssignParameterValues(commandParameters, parameterValues);

        //        //call the overload that takes an array of SqlParameters
        //        return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        //    }
        //        //otherwise we can just call the SP without params
        //    else {
        //        return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        //    }
        //}

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteReader(connection, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            //pass through the call to the private overload using a null transaction value and an externally owned connection
            return ExecuteReader(connection, null, commandType, commandText, commandParameters,
                SqlConnectionOwnership.External);
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        ///     using the provided parameter values.  This method will query the database to discover the parameters for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, string spName,
            params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(connection.ConnectionString, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType,
            string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteReader(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType,
            string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //pass through to private overload, indicating that the connection is owned by the caller
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters,
                SqlConnectionOwnership.External);
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
        ///     SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters
        ///     for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName,
            params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(transaction.Connection.ConnectionString,
                        spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid",
        ///     24));
        /// </remarks>
        /// <param name="spName">the stored procedure name or T-SQL command</param>
        /// <param name="parameterValues">an array of SqlParamters used to execute the command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(string spName, SqlParameter[] parameterValues)
        {
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, parameterValues);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid",
        ///     24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="parameterValues">an array of SqlParamters used to execute the command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText,
            SqlParameter[] parameterValues)
        {
            return ExecuteReader(connectionString, commandType, commandText, parameterValues);
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        ///     Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in
        ///     the connection string.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteScalar(connectionString, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new
        ///     SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteScalar(cn, commandType, commandText, commandParameters);
            }
        }


        /// <summary>
        ///     Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteScalar(connection, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid",
        ///     24));
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            using (var cmd = new SqlCommand())
            {
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                //execute the command & return the results
                var retval = cmd.ExecuteScalar();

                // detach the SqlParameters from the command object, so they can be used again.
                cmd.Parameters.Clear();
                return retval;
            }
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
        ///     using the provided parameter values.  This method will query the database to discover the parameters for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlTransaction.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteScalar(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new
        ///     SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //create a command and prepare it for execution
            using (var cmd = new SqlCommand())
            {
                PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                //execute the command & return the results
                var retval = cmd.ExecuteScalar();

                // detach the SqlParameters from the command object, so they can be used again.
                cmd.Parameters.Clear();
                return retval;
            }
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified
        ///     SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters
        ///     for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     int orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(transaction.Connection.ConnectionString,
                        spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     int orderCount = (int)ExecuteScalar("GetOrderCount", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="spName">the stored procedure name or T-SQL command</param>
        /// <param name="parameterValues">an array of SqlParamters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string spName, SqlParameter[] parameterValues)
        {
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, parameterValues);
        }

        #endregion ExecuteScalar

        #region ExecuteXmlReader

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteXmlReader(connection, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            using (var cmd = new SqlCommand())
            {
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                //create the DataAdapter & DataSet
                var retval = cmd.ExecuteXmlReader();

                // detach the SqlParameters from the command object, so they can be used again.
                cmd.Parameters.Clear();
                return retval;
            }
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        ///     using the provided parameter values.  This method will query the database to discover the parameters for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     XmlReader r = ExecuteXmlReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">a valid SqlConnection</param>
        /// <param name="spName">the name of the stored procedure using "FOR XML AUTO"</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, string spName,
            params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteXmlReader(transaction, commandType, commandText, null);
        }

        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //create a command and prepare it for execution
            using (var cmd = new SqlCommand())
            {
                PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                //create the DataAdapter & DataSet
                var retval = cmd.ExecuteXmlReader();

                // detach the SqlParameters from the command object, so they can be used again.
                cmd.Parameters.Clear();
                return retval;
            }
        }

        /// <summary>
        ///     Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
        ///     SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters
        ///     for the
        ///     stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        ///     This method provides no access to output parameters or the stored procedure's return value parameter.
        ///     e.g.:
        ///     XmlReader r = ExecuteXmlReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName,
            params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters =
                    SqlHelperParameterCache.GetStoreProcedureParameterSet(transaction.Connection.ConnectionString,
                        spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
        }


        /// <summary>
        ///     Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
        ///     using the provided parameters.
        /// </summary>
        /// <remarks>
        ///     e.g.:
        ///     XmlReader r = ExecuteXmlReader("GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="spName">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="parameterValues">an array of SqlParamters used to execute the command</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(string spName, params object[] parameterValues)
        {
            return ExecuteXmlReader(connectionString, CommandType.StoredProcedure, spName, parameterValues);
        }

        #endregion ExecuteXmlReader

    }


    /// <summary>
    ///     SqlHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
    ///     ability to discover parameters for stored procedures at run-time.
    /// </summary>
    public sealed class SqlHelperParameterCache
    {
        #region private methods, variables, and constructors

        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new SqlHelperParameterCache()".
        private SqlHelperParameterCache()
        {
        }

        private static readonly Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        ///     resolve at run time the appropriate set of SqlParameters for a stored procedure
        /// </summary>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">whether or not to include their return value parameter</param>
        /// <returns></returns>
        private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName,
            bool includeReturnValueParameter)
        {
            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(spName, cn))
            {
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlCommandBuilder.DeriveParameters(cmd);

                if (!includeReturnValueParameter)
                {
                    cmd.Parameters.RemoveAt(0);
                }

                var discoveredParameters = new SqlParameter[cmd.Parameters.Count];
                ;

                cmd.Parameters.CopyTo(discoveredParameters, 0);

                return discoveredParameters;
            }
        }

        //deep copy of cached SqlParameter array
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            var clonedParameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion private methods, variables, and constructors

        #region caching functions

        /// <summary>
        ///     add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters to be cached</param>
        public static void CacheParameterSet(string connectionString, string commandText,
            params SqlParameter[] commandParameters)
        {
            var hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        ///     retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an array of SqlParamters</returns>
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            var hashKey = connectionString + ":" + commandText;

            var cachedParameters = (SqlParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                return null;
            }
            return CloneParameters(cachedParameters);
        }

        #endregion caching functions

        #region Parameter Discovery Functions

        /// <summary>
        ///     Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        ///     This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <returns>an array of SqlParameters</returns>
        public static SqlParameter[] GetStoreProcedureParameterSet(string connectionString, string spName)
        {
            return GetStoreProcedureParameterSet(connectionString, spName, false);
        }

        /// <summary>
        ///     Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        ///     This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">
        ///     a bool value indicating whether the return value parameter should be included
        ///     in the results
        /// </param>
        /// <returns>an array of SqlParameters</returns>
        public static SqlParameter[] GetStoreProcedureParameterSet(string connectionString, string spName,
            bool includeReturnValueParameter)
        {
            var hashKey = connectionString + ":" + spName +
                          (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            SqlParameter[] cachedParameters;

            cachedParameters = (SqlParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                cachedParameters =
                    (SqlParameter[])
                        (paramCache[hashKey] =
                            DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter));
            }

            return CloneParameters(cachedParameters);
        }

        #endregion Parameter Discovery Functions
    }
}