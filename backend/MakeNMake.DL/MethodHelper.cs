using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
namespace MakeNMake.DL
{
    public class MethodHelper
    {
        // for stored procedures
        public int ExcuteNonQuery(string connectionString,string procedureName, params SqlParameter[] commandParameters)
        {
            SqlConnection con = new SqlConnection();
            try
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.ConnectionString = connectionString;
                    cmd.Connection = con;
                    foreach (SqlParameter paramter in commandParameters)
                    {
                        cmd.Parameters.AddWithValue(paramter.ParameterName, paramter.Value);
                    }
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    SqlParameter outPutParameter = new SqlParameter();
                    outPutParameter.ParameterName = "@StatOutPut";
                    outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                    outPutParameter.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(outPutParameter);
                    cmd.ExecuteNonQuery();
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                        con.Dispose();
                        SqlConnection.ClearPool(con);
                    }
                    int result = Convert.ToInt32(outPutParameter.Value);
                    commandParameters = null;
                    return result;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                    SqlConnection.ClearPool(con);
                }
            }
        }

        public int ExcuteNonQueryMultipleOutput(string connectionString, string procedureName,string parameter,out int ID, params SqlParameter[] commandParameters)
        {
            SqlConnection con = new SqlConnection();
            try
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.ConnectionString = connectionString;
                    cmd.Connection = con;
                    foreach (SqlParameter paramter in commandParameters)
                    {
                        cmd.Parameters.AddWithValue(paramter.ParameterName, paramter.Value);
                    }
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    SqlParameter RoleIDParameter = new SqlParameter();
                    RoleIDParameter.ParameterName = parameter;
                    RoleIDParameter.SqlDbType = System.Data.SqlDbType.Int;
                    RoleIDParameter.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(RoleIDParameter);

                    SqlParameter outPutParameter = new SqlParameter();
                    outPutParameter.ParameterName = "@StatOutPut";
                    outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                    outPutParameter.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(outPutParameter);
                    cmd.ExecuteNonQuery();
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                        con.Dispose();
                        SqlConnection.ClearPool(con);
                    }
                    int result = Convert.ToInt32(outPutParameter.Value);
                    commandParameters = null;
                    ID = Convert.ToInt32(RoleIDParameter.Value == DBNull.Value ? 0 : RoleIDParameter.Value);
                    return result;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                    SqlConnection.ClearPool(con);
                }
            }
        }
    }
}

