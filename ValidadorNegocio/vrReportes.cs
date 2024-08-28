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
        public List<classReporteVentas> reportesDiarios(string fechaIni, string fechaFin, int id_unidad)
        {
            List<classReporteVentas> id_venta = new List<classReporteVentas>();

            daReportes odaReportes = new daReportes();

            try
            {
                id_venta = odaReportes.reportesDiarios(fechaIni, fechaFin, id_unidad);
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

        public int registrarSalida(classEnvioSalida salidas)
        {
            int idSalida = -1;

            daReportes odaReportes = new daReportes();

            try
            {
                idSalida = odaReportes.registrarSalida(salidas);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return idSalida;
        }

        public List<classSalidas> obtenerSalidas(string fechaIni, string fechaFin, int id_unidad)
        {
            List<classSalidas> salidas = new List<classSalidas>();

            daReportes odaReportes = new daReportes();

            try
            {
                salidas = odaReportes.obtenerSalidas(fechaIni, fechaFin, id_unidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return salidas;
        }

    }
}
