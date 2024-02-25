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
        public List<classReporteVentas> reportesDiarios()
        {
            List<classReporteVentas> respuesta = new List<classReporteVentas>();
            //reporteVentas

            vrReportes ovrReportes = new vrReportes();

            try
            {
                respuesta = ovrReportes.reportesDiarios();
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
    }
}
