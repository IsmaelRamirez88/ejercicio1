using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace modelo
{
    public class ConexionBD
    {
        public SqlConnection ConnSql;
        public void ConectarBD()
        {
            ConnSql = new SqlConnection();
            ConnSql.ConnectionString = global::modelo.Properties.Settings.Default.ConexionBD;
            ConnSql.Open();
        }

        public void Desconectar()
        {
            if(ConnSql.State == ConnectionState.Open)
            {
                ConnSql.Close(); 
            }
        }

        public int ConsultaSqlExecuteNonQuery(string Query)
        {
            int RowsAffected = 0; 
            try
            {
                using (SqlCommand cmd = new SqlCommand(Query, ConnSql))
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.Text;
                    RowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
            }
            return RowsAffected; 
        }


        public DataTable ConsultaSqlSelectDataTable(string Query)
        {
            DataTable TempResul = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand(Query, ConnSql))
                {
                    SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter(cmd);
                    cmd.CommandTimeout = 0;
                    MySqlDataAdapter.Fill(TempResul);
                }
            }
            catch (Exception ex)
            {

            }
            return TempResul; 
        }

    }
}
