using System;
using System.Collections.Generic;
using System.Data;
using MyGo.Core.Logs;
using MySql.Data.MySqlClient;

namespace MyGo.Core
{
    public class MySqlDBHelper
    {
        private MySqlConnection _ConnectionToDB;
        private string _cnnString = "";

       

        public MySqlDBHelper(string connStr)
        {
            _cnnString = connStr;
        }

        public string cnnString
        {
            get { return _cnnString; }
            set { _cnnString = value; }
        }

        public MySqlConnection ConnectionToDB
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
        /// return an Open MySqlConnection
        /// </summary>
        public MySqlConnection OpenConnection(string connectionString)
        {
            try
            {
               // RijndaelEnhanced
                _cnnString = connectionString;
                return OpenConnection();
            }
            //MySqlConnection
            catch (MySqlException myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        /// <summary>
        /// return an Open MySqlConnection
        /// </summary>
        public MySqlConnection OpenConnection()
        {
            if (_cnnString == "")
            {
                throw new Exception("Connection String can not null");
            }
            MySqlConnection myMySqlConnection;

            try
            {
                myMySqlConnection = new MySqlConnection(FixCNN(_cnnString, true));
                myMySqlConnection.Open();
                return myMySqlConnection;
            }
            catch (Exception)
            {
                // De phong truong hop bi max pool thi se fix lai connection string pooling=false
                myMySqlConnection = new MySqlConnection(FixCNN(_cnnString, false));
                myMySqlConnection.Open();
                return myMySqlConnection;
                // throw (new Exception(myException.Message));
            }
        }

        /// <summary>
        /// close an MySqlConnection
        /// </summary>
        public void CloseConnection(MySqlConnection myMySqlConnection)
        {
            try
            {
                if (myMySqlConnection != null)
                {
                    if (myMySqlConnection.State == ConnectionState.Open)
                    {
                        myMySqlConnection.Close();
                    }
                }
            }
            catch (MySqlException myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        // GetDataReader
        public MySqlDataReader GetDataReader(MySqlCommand MySqlCommand)
        {
            MySqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    MySqlCommand.Connection = conn;
                }
                else
                {
                    MySqlCommand.Connection = _ConnectionToDB;
                }
                MySqlDataReader dr = MySqlCommand.ExecuteReader();
                return dr;
            }
            catch (MySqlException myException)
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

        public MySqlDataReader GetDataReader(MySqlCommand MySqlCommand, params MySqlParameter[] Parameters)
        {
            try
            {
                foreach (MySqlParameter par in Parameters)
                {
                    MySqlCommand.Parameters.Add(par);
                }
                return GetDataReader(MySqlCommand);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        public MySqlDataReader GetDataReader(string strSQL)
        {
            var MySqlCommand = new MySqlCommand(strSQL);
            return GetDataReader(MySqlCommand);
        }

        public MySqlDataReader GetDataReader(string strSQL, params MySqlParameter[] Parameters)
        {
            var MySqlCommand = new MySqlCommand(strSQL);
            return GetDataReader(MySqlCommand, Parameters);
        }

        public List<T> GetList<T>(MySqlCommand MySqlCommand)
        {
            MySqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    MySqlCommand.Connection = conn;
                }
                else
                {
                    MySqlCommand.Connection = _ConnectionToDB;
                }
                MySqlDataReader dr = MySqlCommand.ExecuteReader();
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
            catch (MySqlException myException)
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

        public List<T> GetList<T>(MySqlCommand MySqlCommand, out int totalRows)
        {
            MySqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    MySqlCommand.Connection = conn;
                }
                else
                {
                    MySqlCommand.Connection = _ConnectionToDB;
                }
                totalRows = 0;
                MySqlDataReader dr = MySqlCommand.ExecuteReader();
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
            catch (MySqlException myException)
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

        public List<T> GetList<T>(MySqlCommand MySqlCommand, params MySqlParameter[] Parameters)
        {
            try
            {
                foreach (MySqlParameter par in Parameters)
                {
                    MySqlCommand.Parameters.Add(par);
                }
                return GetList<T>(MySqlCommand);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        public List<T> GetList<T>(string strSQL)
        {
            var MySqlCommand = new MySqlCommand(strSQL);
            return GetList<T>(MySqlCommand);
        }

        # region Get DataTable

        public DataTable getDataTable(MySqlCommand MySqlCommand)
        {
            MySqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    MySqlCommand.Connection = conn;
                }
                else
                {
                    MySqlCommand.Connection = _ConnectionToDB;
                }
                var myDataAdapter = new MySqlDataAdapter(MySqlCommand);
                var myDataSet = new DataSet();
                myDataAdapter.Fill(myDataSet);
                return myDataSet.Tables[0];
            }
            catch (MySqlException myException)
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

        public DataTable getDataTable(MySqlCommand MySqlCommand, params MySqlParameter[] Parameters)
        {
            try
            {
                foreach (MySqlParameter par in Parameters)
                {
                    MySqlCommand.Parameters.Add(par);
                }
                return getDataTable(MySqlCommand);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        public DataTable getDataTable(string strSQL)
        {
            var MySqlCommand = new MySqlCommand(strSQL);
            return getDataTable(MySqlCommand);
        }

        public DataTable getDataTable(string strSQL, params MySqlParameter[] Parameters)
        {
            var MySqlCommand = new MySqlCommand(strSQL);
            return getDataTable(MySqlCommand, Parameters);
        }

        #endregion

        # region ExecuteScalar

        public object ExecuteScalar(MySqlCommand MySqlCommand)
        {
            MySqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    MySqlCommand.Connection = conn;
                }
                else
                {
                    MySqlCommand.Connection = _ConnectionToDB;
                }
                return MySqlCommand.ExecuteScalar();
            }
            catch (MySqlException myException)
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

        public object ExecuteScalar(MySqlCommand MySqlCommand, params MySqlParameter[] Parameters)
        {
            try
            {
                foreach (MySqlParameter par in Parameters)
                {
                    MySqlCommand.Parameters.Add(par);
                }
                return ExecuteScalar(MySqlCommand);
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
            var MySqlCommand = new MySqlCommand(strSQL);
            return ExecuteScalar(MySqlCommand);
        }

        public object ExecuteScalar(string strSQL, params MySqlParameter[] Parameters)
        {
            var MySqlCommand = new MySqlCommand(strSQL);
            return ExecuteScalar(MySqlCommand, Parameters);
        }

        #endregion

        # region ExecuteNonQuery

        public int ExecuteNonQuery(MySqlCommand MySqlCommand)
        {
            MySqlConnection conn = null;
            try
            {
                if (_ConnectionToDB == null)
                {
                    conn = OpenConnection();
                    MySqlCommand.Connection = conn;
                }
                else
                {
                    MySqlCommand.Connection = _ConnectionToDB;
                }
                return MySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException myException)
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

        public int ExecuteNonQuery(MySqlCommand MySqlCommand, params MySqlParameter[] Parameters)
        {
            try
            {
                foreach (MySqlParameter par in Parameters)
                {
                    MySqlCommand.Parameters.Add(par);
                }
                return ExecuteNonQuery(MySqlCommand);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        public int ExecuteNonQuery(string strSQL)
        {
            var MySqlCommand = new MySqlCommand(strSQL);
            return ExecuteNonQuery(MySqlCommand);
        }

        public int ExecuteNonQuery(string strSQL, params MySqlParameter[] Parameters)
        {
            var MySqlCommand = new MySqlCommand(strSQL);
            return ExecuteNonQuery(MySqlCommand, Parameters);
        }

        public int ExecuteNonQuerySP(string SPName, params MySqlParameter[] Parameters)
        {
            var MySqlCommand = new MySqlCommand(SPName);
            MySqlCommand.CommandType = CommandType.StoredProcedure;
            return ExecuteNonQuery(MySqlCommand, Parameters);
        }

        #endregion

        #region ExecuteScalarSP

        public object ExecuteScalarSP(string SPName)
        {
            var MySqlCommand = new MySqlCommand(SPName);
            MySqlCommand.CommandType = CommandType.StoredProcedure;
            return ExecuteScalar(MySqlCommand);
        }

        public object ExecuteScalarSP(string SPName, params MySqlParameter[] Parameters)
        {
            var MySqlCommand = new MySqlCommand(SPName);
            MySqlCommand.CommandType = CommandType.StoredProcedure;
            return ExecuteScalar(MySqlCommand, Parameters);
        }

        #endregion

        #region ExecuteSP

        public int ExecuteSP(string SPName)
        {
            var MySqlCommand = new MySqlCommand(SPName);
            MySqlCommand.CommandType = CommandType.StoredProcedure;
            var ReturnValue = new MySqlParameter("@ReturnValue", SqlDbType.Int);
            ReturnValue.Direction = ParameterDirection.ReturnValue;
            MySqlCommand.Parameters.Add(ReturnValue);
            ExecuteNonQuery(MySqlCommand);
            return (int) MySqlCommand.Parameters["@ReturnValue"].Value;
        }

        public int ExecuteSP(string SPName, params MySqlParameter[] Parameters)
        {
            var MySqlCommand = new MySqlCommand(SPName);
            MySqlCommand.CommandType = CommandType.StoredProcedure;
            var ReturnValue = new MySqlParameter("@ReturnValue", SqlDbType.Int);
            ReturnValue.Direction = ParameterDirection.ReturnValue;
            MySqlCommand.Parameters.Add(ReturnValue);
            ExecuteNonQuery(MySqlCommand, Parameters);
            return (int) MySqlCommand.Parameters["@ReturnValue"].Value;
        }

        #endregion

        #region getDataTableSP

        public DataTable getDataTableSP(string SPName)
        {
            var MySqlCommand = new MySqlCommand(SPName);
            MySqlCommand.CommandType = CommandType.StoredProcedure;
            return getDataTable(MySqlCommand);
        }

        public DataTable getDataTableSP(string SPName, params MySqlParameter[] Parameters)
        {
            var MySqlCommand = new MySqlCommand(SPName);
            MySqlCommand.CommandType = CommandType.StoredProcedure;
            return getDataTable(MySqlCommand, Parameters);
        }

        #endregion
    }
}