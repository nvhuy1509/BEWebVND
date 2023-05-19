using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MyGo.Core.Logs;

namespace MyGo.Core
{
    public class DBHelper
    {
        private SqlConnection _ConnectionToDB;
        private string _cnnString = "";

        public DBHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DBHelper(string connStr)
        {
            _cnnString = connStr;
        }

        public string cnnString
        {
            get { return _cnnString; }
            set { _cnnString = value; }
        }

        public SqlConnection ConnectionToDB
        {
            get { return _ConnectionToDB; }
            set { _ConnectionToDB = value; }
        }

        public string FixCNN(string connStr, bool Pooling)
        {
            string[] aconnStr = connStr.Split(';');
            string sTemp = "";

            for (int i = 0; i < aconnStr.Length; i++)
            {
                if (
                    !aconnStr[i].ToLower().StartsWith("pooling=")
                    && !aconnStr[i].ToLower().StartsWith("min pool size=")
                    && !aconnStr[i].ToLower().StartsWith("max pool size=")
                    && !aconnStr[i].ToLower().StartsWith("connect timeout=")
                    && !aconnStr[i].Equals("")
                    )
                {
                    sTemp += aconnStr[i] + ";";
                }
            }

            if (Pooling)
            {
                sTemp +=
                    "Pooling=true;Min Pool Size=5;Max Pool Size=15;Connect Timeout=2;Connection Reset = True;Connection Lifetime = 600;";
            }
            else
            {
                sTemp += "Pooling=false;Connect Timeout=45;";
            }
            return sTemp;
        }

        public void Open()
        {
            if (_cnnString == "")
            {
                throw new Exception("Connection String can not null");
            }
            //_ConnectionToDB = OpenConnection();
        }

        public void Close()
        {
            CloseConnection(_ConnectionToDB);
        }

        /// <summary>
        /// return an Open SqlConnection
        /// </summary>
        public SqlConnection OpenConnection(string connectionString)
        {
            try
            {
               // RijndaelEnhanced
                _cnnString = connectionString;
                return OpenConnection();
            }
            catch (SqlException myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        /// <summary>
        /// return an Open SqlConnection
        /// </summary>
        public SqlConnection OpenConnection()
        {
            if (_cnnString == "")
            {
                throw new Exception("Connection String can not null");
            }
            SqlConnection mySqlConnection;

            try
            {
                mySqlConnection = new SqlConnection(FixCNN(_cnnString, true));
                mySqlConnection.Open();
                return mySqlConnection;
            }
            catch (Exception)
            {
                // De phong truong hop bi max pool thi se fix lai connection string pooling=false
                mySqlConnection = new SqlConnection(FixCNN(_cnnString, false));
                mySqlConnection.Open();
                return mySqlConnection;
                // throw (new Exception(myException.Message));
            }
        }

        /// <summary>
        /// close an SqlConnection
        /// </summary>
        public void CloseConnection(SqlConnection mySqlConnection)
        {
            try
            {
                if (mySqlConnection != null)
                {
                    if (mySqlConnection.State == ConnectionState.Open)
                    {
                        mySqlConnection.Close();
                    }
                }
            }
            catch (SqlException myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        // GetDataReader
        public SqlDataReader GetDataReader(SqlCommand sqlCommand)
        {
            SqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    sqlCommand.Connection = conn;
                }
                else
                {
                    sqlCommand.Connection = _ConnectionToDB;
                }
                SqlDataReader dr = sqlCommand.ExecuteReader();
                return dr;
            }
            catch (SqlException myException)
            {
                throw (new Exception(myException.Message));
            }
            finally
            {
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public SqlDataReader GetDataReader(SqlCommand sqlCommand, params SqlParameter[] Parameters)
        {
            try
            {
                foreach (SqlParameter par in Parameters)
                {
                    sqlCommand.Parameters.Add(par);
                }
                return GetDataReader(sqlCommand);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        public SqlDataReader GetDataReader(string strSQL)
        {
            var sqlCommand = new SqlCommand(strSQL);
            return GetDataReader(sqlCommand);
        }

        public SqlDataReader GetDataReader(string strSQL, params SqlParameter[] Parameters)
        {
            var sqlCommand = new SqlCommand(strSQL);
            return GetDataReader(sqlCommand, Parameters);
        }

        public List<T> GetList<T>(SqlCommand sqlCommand)
        {
            SqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    sqlCommand.Connection = conn;
                }
                else
                {
                    sqlCommand.Connection = _ConnectionToDB;
                }
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr == null || dr.FieldCount == 0)
                    return null;

                //int fCount = dr.FieldCount;
                //Type m_Type = typeof(T);
                //object obj;

                var m_List = new List<T>();

                DynamicBuilder<T> builder = DynamicBuilder<T>.CreateBuilder(dr);

                while (dr.Read())
                {
                    //obj = Activator.CreateInstance(m_Type);
                    //for (int i = 0; i < fCount; i++)
                    //{
                    //    if (dr[i] != System.DBNull.Value)
                    //    {
                    //        m_Type.GetProperty(dr.GetName(i)).SetValue(obj, dr[i], null);
                    //    }
                    //}
                    T r = builder.Build(dr);
                    m_List.Add(r);
                }
                dr.Close();
                return m_List;
            }
            catch (SqlException myException)
            {
                throw (new Exception(myException.Message));
            }
            finally
            {
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public List<T> GetList<T>(SqlCommand sqlCommand, out int totalRows)
        {
            SqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    sqlCommand.Connection = conn;
                }
                else
                {
                    sqlCommand.Connection = _ConnectionToDB;
                }
                totalRows = 0;
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr == null || dr.FieldCount == 0)
                {
                    return null;
                }

                //int fCount = dr.FieldCount;
                //Type m_Type = typeof(T);
                //object obj;

                var m_List = new List<T>();

                DynamicBuilder<T> builder = DynamicBuilder<T>.CreateBuilder(dr);

                while (dr.Read())
                {
                    //obj = Activator.CreateInstance(m_Type);
                    //for (int i = 0; i < fCount; i++)
                    //{
                    //    if (dr[i] != System.DBNull.Value)
                    //    {
                    //        m_Type.GetProperty(dr.GetName(i)).SetValue(obj, dr[i], null);
                    //    }
                    //}
                    T r = builder.Build(dr);
                    m_List.Add(r);
                }
                if (dr.NextResult())
                {
                    if (dr.Read())
                    {
                        totalRows = (int) dr["TotalRowCount"];
                    }
                }
                dr.Close();
                return m_List;
            }
            catch (SqlException myException)
            {
              //  Logger.Error(myException);
                throw (new Exception(myException.Message));
            }
            finally
            {
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public List<T> GetList<T>(SqlCommand sqlCommand, params SqlParameter[] Parameters)
        {
            try
            {
                foreach (SqlParameter par in Parameters)
                {
                    sqlCommand.Parameters.Add(par);
                }
                return GetList<T>(sqlCommand);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        public List<T> GetList<T>(string strSQL)
        {
            var sqlCommand = new SqlCommand(strSQL);
            return GetList<T>(sqlCommand);
        }

        # region Get DataTable

        public DataTable getDataTable(SqlCommand sqlCommand)
        {
            SqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    sqlCommand.Connection = conn;
                }
                else
                {
                    sqlCommand.Connection = _ConnectionToDB;
                }
                var myDataAdapter = new SqlDataAdapter(sqlCommand);
                var myDataSet = new DataSet();
                myDataAdapter.Fill(myDataSet);
                return myDataSet.Tables[0];
            }
            catch (SqlException myException)
            {
                throw (new Exception(myException.Message));
            }
            finally
            {
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public DataTable getDataTable(SqlCommand sqlCommand, params SqlParameter[] Parameters)
        {
            try
            {
                foreach (SqlParameter par in Parameters)
                {
                    sqlCommand.Parameters.Add(par);
                }
                return getDataTable(sqlCommand);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        public DataTable getDataTable(string strSQL)
        {
            var sqlCommand = new SqlCommand(strSQL);
            return getDataTable(sqlCommand);
        }

        public DataTable getDataTable(string strSQL, params SqlParameter[] Parameters)
        {
            var sqlCommand = new SqlCommand(strSQL);
            return getDataTable(sqlCommand, Parameters);
        }

        #endregion

        # region ExecuteScalar

        public object ExecuteScalar(SqlCommand sqlCommand)
        {
            SqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    sqlCommand.Connection = conn;
                }
                else
                {
                    sqlCommand.Connection = _ConnectionToDB;
                }
                return sqlCommand.ExecuteScalar();
            }
            catch (SqlException myException)
            {
                Logger.Error(myException);
                return -1;
                //throw (new Exception(myException.Message));
            }
            finally
            {
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public object ExecuteScalar(SqlCommand sqlCommand, params SqlParameter[] Parameters)
        {
            try
            {
                foreach (SqlParameter par in Parameters)
                {
                    sqlCommand.Parameters.Add(par);
                }
                return ExecuteScalar(sqlCommand);
            }
            catch (Exception myException)
            {
                Logger.Error(myException);
                return -1;
                //throw (new Exception(myException.Message));
            }
        }

        public object ExecuteScalar(string strSQL)
        {
            var sqlCommand = new SqlCommand(strSQL);
            return ExecuteScalar(sqlCommand);
        }

        public object ExecuteScalar(string strSQL, params SqlParameter[] Parameters)
        {
            var sqlCommand = new SqlCommand(strSQL);
            return ExecuteScalar(sqlCommand, Parameters);
        }

        #endregion

        # region ExecuteNonQuery

        public int ExecuteNonQuery(SqlCommand sqlCommand)
        {
            SqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    sqlCommand.Connection = conn;
                }
                else
                {
                    sqlCommand.Connection = _ConnectionToDB;
                }
                return sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException myException)
            {
                throw (new Exception(myException.Message));
            }
            finally
            {
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public int ExecuteNonQuery(SqlCommand sqlCommand, params SqlParameter[] Parameters)
        {
            try
            {
                foreach (SqlParameter par in Parameters)
                {
                    sqlCommand.Parameters.Add(par);
                }
                return ExecuteNonQuery(sqlCommand);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        public int ExecuteNonQuery(string strSQL)
        {
            var sqlCommand = new SqlCommand(strSQL);
            return ExecuteNonQuery(sqlCommand);
        }

        public int ExecuteNonQuery(string strSQL, params SqlParameter[] Parameters)
        {
            var sqlCommand = new SqlCommand(strSQL);
            return ExecuteNonQuery(sqlCommand, Parameters);
        }

        public int ExecuteNonQuerySP(string SPName, params SqlParameter[] Parameters)
        {
            var sqlCommand = new SqlCommand(SPName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            return ExecuteNonQuery(sqlCommand, Parameters);
        }

        #endregion

        #region ExecuteScalarSP

        public object ExecuteScalarSP(string SPName)
        {
            var sqlCommand = new SqlCommand(SPName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            return ExecuteScalar(sqlCommand);
        }

        public object ExecuteScalarSP(string SPName, params SqlParameter[] Parameters)
        {
            var sqlCommand = new SqlCommand(SPName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            return ExecuteScalar(sqlCommand, Parameters);
        }

        #endregion

        #region ExecuteSP

        public int ExecuteSP(string SPName)
        {
            var sqlCommand = new SqlCommand(SPName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            var ReturnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
            ReturnValue.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(ReturnValue);
            ExecuteNonQuery(sqlCommand);
            return (int) sqlCommand.Parameters["@ReturnValue"].Value;
        }

        public int ExecuteSP(string SPName, params SqlParameter[] Parameters)
        {
            var sqlCommand = new SqlCommand(SPName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            var ReturnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
            ReturnValue.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(ReturnValue);
            ExecuteNonQuery(sqlCommand, Parameters);
            return (int) sqlCommand.Parameters["@ReturnValue"].Value;
        }

        #endregion

        #region getDataTableSP

        public DataTable getDataTableSP(string SPName)
        {
            var sqlCommand = new SqlCommand(SPName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            return getDataTable(sqlCommand);
        }

        public DataTable getDataTableSP(string SPName, params SqlParameter[] Parameters)
        {
            var sqlCommand = new SqlCommand(SPName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            return getDataTable(sqlCommand, Parameters);
        }

        #endregion
    }
}