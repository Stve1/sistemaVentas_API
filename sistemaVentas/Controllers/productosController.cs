using ClasesNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidadorNegocio;

namespace sistemaVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class productosController : Controller
    {
        // POST: HomeController1/Create
        [HttpGet, Route("obtenerProductos")]
        public List<classProductos> obtenerProductos()
        {
            List<classProductos> respuesta = new List<classProductos>();

            vrProductos ovrProductos = new vrProductos();
                
            try
            {
                respuesta = ovrProductos.obtenerProductos();
            }
            catch(Exception ex)
            {
                string mensaje = ex.Message;
            }

            return respuesta;
        }

        // POST: HomeController1/Edit/5
        /*
        [HttpPost, Route("guardarProductos")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
