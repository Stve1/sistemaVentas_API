using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesNegocio
{
    public class classSalidas
    {
        public int id_salida {  get; set; }
        public string descripcion_salida { get; set; }
        public int valor_salida { get; set; }
        public int metodo_pago_salida { get; set; }
        public string descripcion_metodo_pago { get; set; }
        public DateTime fecha_registro_salida { get; set; }
    }
}
