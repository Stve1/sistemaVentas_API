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

        public int registrarVentas(classTotProductos totalProductos)
        {
            int idProducto = -1;

            daProductos odaProductos = new daProductos();

            try
            {
                idProducto = odaProductos.registrarVentas(totalProductos);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return idProducto;
        }


        public int registrarProductos(classVentaProd productos)
        {
            int idProducto = -1;

            daProductos odaProductos = new daProductos();

            try
            {
                idProducto = odaProductos.registrarProductos(productos);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return idProducto;
        }

        public List<classMetodosPagos> obtenerMetodosPagos(int id_unidad)
        {
            List<classMetodosPagos> id_producto = new List<classMetodosPagos>();

            daProductos odaProductos = new daProductos();

            try
            {
                id_producto = odaProductos.obtenerMetodosPagos(id_unidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id_producto;
        }
    }
}
