using ClasesNegocio;
using Npgsql;

namespace AccesoDatos
{
    public class daProductos
    {
        //obtenerProductos

        public List<classProductos> obtenerProductos()
        {

            string SELECT_PRODUCTOS = "SELECT * FROM productos order by id_producto desc";

            List<classProductos> lProductos = new List<classProductos>();

            classProductos productos;

            conexionDB con = new conexionDB();
            NpgsqlConnection v_con = con.conexion();
            v_con.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SELECT_PRODUCTOS, v_con);

                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) 
                {
                    productos = new classProductos();
                    productos.id_producto = reader.GetInt32(0);
                    productos.nombre_producto = reader.GetString(1);
                    productos.aplicacion_productos = reader.GetString(2);
                    productos.costo_producto = reader.GetInt32(3);
                    productos.precio_producto = reader.GetInt32(4);
                    productos.cant_producto = reader.GetInt32(5);
                    productos.cod_producto = reader.GetInt32(6);
                    productos.id_subcategoria = reader.GetInt32(7);

                    lProductos.Add(productos);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            v_con.Close();

            return lProductos;
        }
    }
}
