using AccesoDatos;
using ClasesNegocio;

namespace ValidadorNegocio
{
    public class vrProductos
    {
        public BeRes obtenerProductos()
        {
            BeRes resProd= new();
            daProductos odaProductos = new daProductos();

            try
            {
                resProd = odaProductos.obtenerProductos();
            }
            catch(Exception ex)
            {
                resProd = new()
                {
                    code = 401,
                    status = "F",
                    message = ex.Message
                };
            }

            return resProd;
        }

        public BeRes registrarVentas(classTotProductos totalProductos)
        {
            BeRes res = new();
            int idProducto = -1;

            daProductos odaProductos = new daProductos();

            try
            {
                res = odaProductos.registrarVentas(totalProductos);
            }
            catch (Exception ex)
            {
                res = new()
                {
                    code = 400,
                    status= "F",
                    message = ex.Message
                };
            }

            return res;
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
