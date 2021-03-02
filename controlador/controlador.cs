using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelo;

namespace controlador
{
    public class controlador
    {
        private ModeloDatos Modelo = new ModeloDatos(); 

        public void CerrarConexion()
        {
            Modelo.ModeloDatosDesconectar(); 
        }

        public DataTable ContieneIVA()
        {
            DataTable Temp = new DataTable();
            Temp = Modelo.CosultarIVA(); 
            return Temp; 
        }
        public int EditarIVA(double IVA)
        {
            return Modelo.ModificarIVA(IVA); 
        }

        public DataTable ContieneProducto()
        {
            DataTable Temp = new DataTable();
            Temp = Modelo.CosultarProducto(); 
            return Temp;
        }

        public int AltaProducto(string Producto, double Precio)
        {
            int Resultado = 0;
            Resultado = Modelo.AgregarProducto(Producto, Precio); 
            return Resultado; 
        }

        public int ModificarProducto(int Id, string Producto, double Precio)
        {
            int Resultado = 0;
            Resultado = Modelo.ModificarProducto(Id, Producto, Precio); 
            return 0; 
        }

        public void EliminarProducto(int Id)
        {
            Modelo.EliminarProducto(Id); 
        }

    }
    public class Producto
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
    }

   
}
