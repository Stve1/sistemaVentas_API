using ClasesNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidadorNegocio;

namespace sistemaVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class reportesController : Controller
    {
        //obtenerProductos
        [HttpGet, Route("reportesDiarios")]
        public List<classReporteVentas> reportesDiarios(string fechaIni, string fechaFin, int id_unidad)
        {
            List<classReporteVentas> respuesta = new List<classReporteVentas>();
            //reporteVentas

            vrReportes ovrReportes = new vrReportes();

            try
            {
                respuesta = ovrReportes.reportesDiarios(fechaIni, fechaFin, id_unidad);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return respuesta;
        }

        //registrarVentas
        [HttpPost, Route("registrarVentas")]
        public int registrarVentas(classTotProductos totalProductos)
        {
            int idProducto = -1;

            vrProductos ovrProductos = new vrProductos();

            try
            {
                idProducto = ovrProductos.registrarVentas(totalProductos);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return idProducto;
        }

        //registrarSalidas - gastos
        [HttpPost, Route("registrarSalida")]
        public int registrarSalida(classSalidas salidas)
        {
            int idSalida = -1;

            vrReportes ovrReportes = new vrReportes();

            try
            {
                idSalida = ovrReportes.registrarSalida(salidas);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return idSalida;
        }

        //obtener Reporte Salidas - gastos
        [HttpGet, Route("obtenerSalidas")]
        public List<classSalidas> obtenerSalidas(string fechaIni, string fechaFin, int id_unidad)
        {
            List<classSalidas> salidas = new List<classSalidas>();

            vrReportes ovrReportes = new vrReportes();

            try
            {
                salidas = ovrReportes.obtenerSalidas(fechaIni, fechaFin, id_unidad);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return salidas;
        }
    }
}
