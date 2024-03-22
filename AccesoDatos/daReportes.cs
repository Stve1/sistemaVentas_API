using ClasesNegocio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class daReportes
    {
        public List<classReporteVentas> reportesDiarios(string fechaIni, string fechaFin, int id_unidad)
        {
            string fecha1 = fechaIni;
            string fecha2 = fechaFin;


            string SELECT_REPORTES = @"SELECT id_venta, nombre_cliente, fecha_venta, total_descuento, total_incremento, total_venta, id_cliente, cantidad_productos, descripcion_metodo
                FROM v_reportes_diarios WHERE to_char(fecha_venta, 'yyyy-MM-dd') between '" + fecha1 + "' AND '"+ fecha2 + "' AND id_unidad = "+ id_unidad + ";";

            List<classReporteVentas> lReportes = new List<classReporteVentas>();

            classReporteVentas reportes;

            conexionDB con = new conexionDB();
            NpgsqlConnection v_con = con.conexion();
            v_con.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SELECT_REPORTES, v_con);

                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    reportes = new classReporteVentas();
                    reportes.id_venta = reader.GetInt32(0);
                    reportes.nombre_cliente = reader.GetString(1);
                    reportes.fecha_venta = reader.GetDateTime(2);
                    reportes.total_descuento = reader.GetInt32(3);
                    reportes.total_incremento = reader.GetInt32(4);
                    reportes.total_venta = reader.GetInt32(5);
                    reportes.id_cliente = reader.GetInt32(6);
                    reportes.cantidad_productos = reader.GetInt32(7);
                    reportes.descripcion_metodo_pago = reader.GetString(8);

                    lReportes.Add(reportes);
                }

                v_con.Close();

            }
            catch (Exception ex)
            {
                v_con.Close();
                throw ex;
            }

            return lReportes;
        }

        //Método para RegistrarVentas
        public int registrarVentas(classTotProductos totalProductos)
        {

            string INSERT_PRODUCTOS = @$"INSERT INTO VENTAS (fecha_venta, total_descuento, total_incremento, total_venta, id_cliente, cantidad_productos, metodo_pago)
                VALUES (current_timestamp , {totalProductos.total_descuento}, {totalProductos.total_incremento}, {totalProductos.total_venta}, {totalProductos.id_cliente}, {totalProductos.cantidad_productos}, {totalProductos.metodo_pago});";

            classProductos productos;

            int idProducto = -1;

            conexionDB con = new conexionDB();
            NpgsqlConnection v_con = con.conexion();
            v_con.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(INSERT_PRODUCTOS, v_con);

                int nRegistros = Convert.ToInt32(command.ExecuteNonQuery());

                if (nRegistros > 0)
                {
                    idProducto = nRegistros;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            v_con.Close();

            return idProducto;
        }


        //Método para Registrar venta de productos
        public int registrarProductos(classVentaProd productos)
        {

            string INSERT_PRODUCTOS = @$"INSERT INTO venta_productos (id_producto, cantidad_producto, descuento_producto, incremento_producto, fecha_venta, id_venta)
                VALUES ((SELECT id_producto FROM productos WHERE cod_producto = {productos.cod_producto}), {productos.cant_producto}, {productos.descuento_producto}, {productos.incremento_producto} ,current_timestamp,
                (SELECT id_venta FROM ventas ORDER BY id_venta DESC limit 1));";

            int idProducto = -1;

            conexionDB con = new conexionDB();
            NpgsqlConnection v_con = con.conexion();
            v_con.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(INSERT_PRODUCTOS, v_con);

                int nRegistros = Convert.ToInt32(command.ExecuteNonQuery());

                if (nRegistros > 0)
                {
                    idProducto = nRegistros;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            v_con.Close();

            return idProducto;
        }


        //Método para registrar salidas- gastos
        public int registrarSalida(classSalidas salidas)
        {

            string INSERT_SALIDAS = @$"INSERT INTO salidas (descripcion_salida, valor_salida, metodo_pago_salida, fecha_salida)
                VALUES ({salidas.descripcion_salida}, {salidas.valor_salida}, {salidas.metodo_pago_salida}, current_timestamp);";

            int idSalida = -1;

            conexionDB con = new conexionDB();
            NpgsqlConnection v_con = con.conexion();
            v_con.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(INSERT_SALIDAS, v_con);

                int nRegistros = Convert.ToInt32(command.ExecuteNonQuery());

                if (nRegistros > 0)
                {
                    idSalida = nRegistros;
                }

                v_con.Close();
            }
            catch (Exception ex)
            {
                v_con.Close();
                throw ex;
            }

            return idSalida;
        }

        //Método para listar salidas- gastos
        public List<classSalidas> obtenerSalidas(string fechaIni, string fechaFin, int id_unidad)
        {
            string fecha1 = fechaIni;
            string fecha2 = fechaFin;

            string SELECT_SALIDAS = @$"SELECT id_salida, descripcion_salida, valor_salida, metodo_pago_salida, fecha_salida
                FROM salidas WHERE to_char(fecha_salida, 'yyyy-MM-dd') between '{fecha1}' AND '{fecha2}' AND id_unidad = {id_unidad};";

            List<classSalidas> listSalidas = new List<classSalidas>();

            classSalidas salidas = new classSalidas();

            conexionDB con = new conexionDB();
            NpgsqlConnection v_con = con.conexion();
            v_con.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SELECT_SALIDAS, v_con);

                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    salidas = new classSalidas();
                    salidas.id_salida = reader.GetInt32(0);
                    salidas.descripcion_salida = reader.GetString(1);
                    salidas.valor_salida = reader.GetInt32(2);
                    salidas.metodo_pago_salida = reader.GetInt32(3);
                    salidas.descripcion_metodo_pago = reader.GetString(4);
                    salidas.fecha_registro_salida = reader.GetDateTime(5);
                    listSalidas.Add(salidas);
                }

                v_con.Close();

            }
            catch (Exception ex)
            {
                v_con.Close();
                throw ex;
            }

            return listSalidas;
        }
    }
}
