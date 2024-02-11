using AccesoDatos;
using ClasesNegocio;

namespace ValidadorNegocio
{
    public class vrProductos
    {
        public List<classProductos> obtenerProductos()
        {
            List<classProductos> id_producto = new List<classProductos>();

            daProductos odaProductos = new daProductos();

            try
            {
                id_producto = odaProductos.obtenerProductos();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return id_producto;
        }
    }
}
