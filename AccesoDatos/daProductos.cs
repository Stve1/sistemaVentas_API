using ClasesNegocio;
using Npgsql;

namespace AccesoDatos
{
    public class daProductos
    {
        //Método para obtenerProductos
        public List<classProductos> obtenerProductos()
        {

            string SELECT_PRODUCTOS = "SELECT * FROM productos order by id_producto desc";

            List<classProductos> lProductos = new List<classProductos>();

            classProductos productos;

            conexionDB con = new conexionDB();
            using (NpgsqlConnection v_con = con.conexion())
            {
                try
                {
                    v_con.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(SELECT_PRODUCTOS, v_con))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos = new classProductos();
                                productos.id_producto = reader.GetInt32(0);
                                productos.nombre_producto = reader.GetString(1);
                                productos.aplicacion_productos = reader.GetString(2);
                                productos.costo_producto = reader.GetInt32(3);
                                productos.precio_producto = reader.GetInt32(4);
                                productos.cant_producto = reader.GetInt32(5);
                                productos.cod_producto = reader.GetInt32(6);
                                productos.id_subcategoria = reader.GetInt32(7);
                                productos.costo2_producto = reader.GetInt32(8);
                                productos.precio2_producto = reader.GetInt32(9);

                                lProductos.Add(productos);
                            }

                            v_con.Dispose();
                        };
                    };

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    v_con.Dispose();
                }


            };

            return lProductos;
        }

        //Método para RegistrarVentas
        public int registrarVentas(classTotProductos totalProductos)
        {

            string INSERT_PRODUCTOS = @$"INSERT INTO VENTAS (fecha_venta, total_descuento, total_incremento, total_venta, id_cliente, cantidad_productos, metodo_pago)
                VALUES (current_timestamp , {totalProductos.total_descuento}, {totalProductos.total_incremento}, {totalProductos.total_venta}, {totalProductos.id_cliente}, {totalProductos.cantidad_productos}, {totalProductos.metodo_pago});";

            classProductos productos;

            int idProducto = -1;

            conexionDB con = new conexionDB();
            using (NpgsqlConnection v_con = con.conexion())
            {
                
                try
                {
                    v_con.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(INSERT_PRODUCTOS, v_con))
                    {
                        int nRegistros = Convert.ToInt32(command.ExecuteNonQuery());

                        if (nRegistros > 0)
                        {
                            idProducto = nRegistros;
                        }
                        v_con.Dispose();
                    };

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    v_con.Dispose();
                }

            };
            
            return idProducto;
        }


        //Método para Registrar venta de productos
        public int registrarProductos(classVentaProd productos)
        {

            string INSERT_PRODUCTOS = @$"INSERT INTO venta_productos (id_producto, cantidad_producto, descuento_producto, incremento_producto, fecha_venta, id_venta)
                VALUES ((SELECT id_producto FROM productos WHERE cod_producto = {productos.cod_producto}), {productos.cant_producto}, {productos.descuento_producto}, {productos.incremento_producto} ,current_timestamp,
                (SELECT id_venta FROM ventas ORDER BY id_venta DESC limit 1));";

            string CANT_PRODUCTOS = @$"UPDATE productos SET CANT_PRODUCTO = (
                (SELECT cant_producto WHERE cod_producto = {productos.cod_producto}) - {productos.cant_producto})
                WHERE cod_producto = {productos.cod_producto};";

            int idProducto = -1;

            conexionDB con = new conexionDB();
            using(NpgsqlConnection v_con = con.conexion())
            {
                
                try
                {
                    v_con.Open();

                    NpgsqlCommand command = new NpgsqlCommand(INSERT_PRODUCTOS, v_con);
                    int nRegistros = Convert.ToInt32(command.ExecuteNonQuery());

                    if (nRegistros > 0)
                    {
                        conexionDB conCP = new conexionDB();
                        using (NpgsqlConnection v_conCP = conCP.conexion())
                        {

                            try
                            {
                                v_conCP.Open();

                                NpgsqlCommand command1 = new NpgsqlCommand(CANT_PRODUCTOS, v_conCP);
                                nRegistros = Convert.ToInt32(command1.ExecuteNonQuery());

                                if (nRegistros > 0)
                                {
                                    idProducto = nRegistros;
                                }
                                v_conCP.Dispose();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                            finally
                            {
                                v_conCP.Dispose();
                            }
                        };
                    }

                    v_con.Dispose();
                }
                catch (Exception ex)
                {
                    //v_con.Close();
                    throw ex;
                }

                finally
                {
                    v_con.Dispose();
                }

            };
            
            return idProducto;
        }


        //Método para obtenerProductos
        public List<classMetodosPagos> obtenerMetodosPagos(int id_unidad)
        {

            string SELECT_METODOS = $@"SELECT
            ID_METODO_PAGO, DESCRIPCION_METODO, ID_UNIDAD
            FROM PUBLIC.METODO_PAGOS
            WHERE ID_UNIDAD = {id_unidad}
            ORDER BY ID_METODO_PAGO ASC;";

            List<classMetodosPagos> lMetodosPagos = new List<classMetodosPagos>();

            classMetodosPagos metodosPagos;

            conexionDB con = new conexionDB();
            using (NpgsqlConnection v_con = con.conexion())
            {
                try
                {
                    v_con.Open();

                    NpgsqlCommand command = new NpgsqlCommand(SELECT_METODOS, v_con);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            metodosPagos = new classMetodosPagos();
                            metodosPagos.id_metodo_pago = reader.GetInt32(0);
                            metodosPagos.descripcion_metodo = reader.GetString(1);
                            metodosPagos.id_unidad = reader.GetInt32(2);

                            lMetodosPagos.Add(metodosPagos);
                        }
                        v_con.Dispose();
                    };

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    v_con.Dispose();
                }

            };

            return lMetodosPagos;
        }
    }
}
