namespace ClasesNegocio
{
    public class classProductos
    {
        public int id_producto {  get; set; }
        public string nombre_producto { get; set; }
        public string aplicacion_productos { get; set; }
        public int cant_producto { get; set; }
        public int cod_producto { get; set; }
        public List<classPrecio> bePrecio { get; set; }
        public List<classCosto> beCosto { get; set; }
        public classSubcategoria beSubcategoria { get; set; }
    }
}
