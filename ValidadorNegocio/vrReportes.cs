using AccesoDatos;
using ClasesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidadorNegocio
{
    public class vrReportes
    {
        public List<classReporteVentas> reportesDiarios()
        {
            List<classReporteVentas> id_venta = new List<classReporteVentas>();

            daReportes odaReportes = new daReportes();

            try
            {
                id_venta = odaReportes.reportesDiarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id_venta;
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
    }
}
