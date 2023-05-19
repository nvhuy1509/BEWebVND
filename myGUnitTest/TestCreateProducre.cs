using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Xunit;

namespace myGUnitTest
{
    public class TestCreateProducre
    {
      private  string ConnectionString = "server=123.30.245.58,61433;database=myG.BeAStar;uid=myg.id;pwd=myG.id_c0re123*654;";
        [Fact]
        public void CreateProducre()
        {
            StringBuilder sbSP = new StringBuilder();

            sbSP.AppendLine("CREATE PROCEDURE sp_test AS BEGIN    print 'Enter Stored Procedure here'  END");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        [Fact]
        public void GetNameTable()
        {
            List<string> tables = new List<string>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                DataTable dt = connection.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    string tablename = (string)row[2];
                    tables.Add(tablename);
                }
                connection.Close();
            }

            //return tables;
        }
        [Fact]
        public void GetNameColumsTable()
        {
            List<string> colList = new List<string>();
            DataTable dataTable = new DataTable();


            string cmdString = String.Format("SELECT TOP 0 * FROM {0}", "");

            //if (ConnectionManager != null)
            //{
            //    try
            //    {
            //        using (SqlDataAdapter dataContent = new SqlDataAdapter(cmdString, ConnectionManager.ConnectionToSQL))
            //        {
            //            dataContent.Fill(dataTable);

            //            foreach (DataColumn col in dataTable.Columns)
            //            {
            //                colList.Add(col.ColumnName);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        InternalError = ex.Message;
            //    }
            //}

            //return tables;
        }
    }
}
