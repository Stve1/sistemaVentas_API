using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesNegocio
{
    public class classReporteVentas
    {
        public int id_venta {  get; set; }
        public string nombre_cliente { get; set; }
        public DateTime fecha_venta { get; set; }
        public int total_descuento { get; set; }
        public int total_incremento { get; set; }
        public int total_venta { get; set; }
        public int id_cliente { get; set; }
        public int cantidad_productos { get; set; }
    }
}
