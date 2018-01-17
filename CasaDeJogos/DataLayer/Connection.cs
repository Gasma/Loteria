using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CasaDeJogos.DataLayer
{
    public sealed class Connection : IDisposable
    {
        private readonly SqlConnection minhaconexao;

        public Connection()
        {
            minhaconexao = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            minhaconexao.Open();
        }
        public int getCod(string strQuery)
        {
            SqlCommand cmdCod = new SqlCommand(strQuery, minhaconexao);
            int cod = 0;
            try
            {
                cod = (int)cmdCod.ExecuteScalar();
            }
            catch (Exception)
            {
                cod = 1;

            }
            return cod;
        }
        public int ExecutaComando(string strQuery)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(strQuery, minhaconexao);
                return (int)cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public SqlDataReader ConRetornaReader(string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery, minhaconexao);
            SqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }
        public DataTable ConRetornaTable(string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery, minhaconexao);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void Dispose()
        {
            if (minhaconexao.State == ConnectionState.Open)
                minhaconexao.Close();
        }
        public int InsertData(string strQuery)
        {
            return ExecutaComando(strQuery);
        }
    }
}