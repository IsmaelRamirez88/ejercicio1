using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class ModeloDatos
    {
        ConexionBD ConexionDatos = new ConexionBD();
        string query = string.Empty;
        public ModeloDatos()
        {
            ConexionDatos.ConectarBD();
        }
        public void ModeloDatosDesconectar()
        {
            ConexionDatos.Desconectar();
        }

        #region TODO IVA
        public DataTable CosultarIVA()
        {
            DataTable Temp = new DataTable();
            try
            {
                query = "SELECT id, iva FROM configuraiva";
                Temp = ConexionDatos.ConsultaSqlSelectDataTable(query); 
            }
            catch (Exception)
            {
            }
            return Temp;
        }

        public int ModificarIVA(double ValorIVA)
        {
            int Resultado = 0; 
            try
            {
                query = $"update configuraiva set iva = {ValorIVA}";
                Resultado = ConexionDatos.ConsultaSqlExecuteNonQuery(query); 
            }
            catch (Exception ex)
            {
            }
            return Resultado; 
        }
        #endregion

        #region TODO PRODUCTO
        public DataTable CosultarProducto()
        {
            DataTable Temp = new DataTable();
            try
            {
                query = "SELECT id,descripcion, precio FROM producto";
                Temp = ConexionDatos.ConsultaSqlSelectDataTable(query);
            }
            catch (Exception ex)
            {
            }
            return Temp;
        }

        public int AgregarProducto(string Descripcion, double Precio)
        {
            int Resultado = 0;
            try
            {
                query = $"insert into producto(descripcion, precio) values('{Descripcion}',{Precio})";
                Resultado = ConexionDatos.ConsultaSqlExecuteNonQuery(query);
            }
            catch (Exception)
            {

            }
            return Resultado; 
        }

        public int ModificarProducto(int Id, string Descripcion, double Precio)
        {
            int Resultado = 0;
            try
            {
                query = $"update producto set descripcion = '{Descripcion}', precio = {Precio} where id = {Id}  ";
                Resultado = ConexionDatos.ConsultaSqlExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
            }
            return Resultado;
        }

        public int EliminarProducto(int Id)
        {
            int Resultado = 0;
            try
            {
                query = $"delete from producto where id = {Id}";
                Resultado = ConexionDatos.ConsultaSqlExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
            }
            return Resultado;
        }

        #endregion
    }
}
