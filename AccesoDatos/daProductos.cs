using System.Text.Json;
using System.Text.Json.Serialization;
using ClasesNegocio;
using Npgsql;

namespace AccesoDatos
{
    public class daProductos
    {
        //Método para obtenerProductos
        public BeRes obtenerProductos()
        {
            string cs = "";
            BeRes res = new();
            List<classProductos> lProductos = new();
            List<classCosto> lCostos = new();
            List<classPrecio> lPrecio = new();
            string SELECT_PRODUCTOS = $@"SELECT pr.id_producto, pr.nombre_producto, pr.aplicacion_productos,
            pr.cant_producto, pr.cod_producto,

            (SELECT json_agg(row_to_json(c))
				FROM (SELECT cs.id_costo, cs.costo_producto, cp.id_producto, to_char(cp.fecha_modificacion, 'yyyy-MM-dd HH24:MI:SS') AS fecha_modificacion
				FROM costos_productos cp
				JOIN costos cs ON cs.id_costo = cp.id_costo
				WHERE cp.id_producto = pr.id_producto ORDER BY cs.id_costo DESC) AS c
			) AS json_costo,

			(SELECT json_agg(row_to_json(p))
				FROM (SELECT ps.id_precio, ps.precio_producto, pp.id_producto, to_char(pp.fecha_modificacion, 'yyyy-MM-dd HH24:MI:SS') AS fecha_modificacion
				FROM precios_productos pp
				JOIN precios ps ON ps.id_precio = pp.id_precio
				WHERE pp.id_producto = pr.id_producto ORDER BY ps.id_precio DESC) AS p
			) AS json_precio,

			/*
            COALESCE((SELECT STRING_AGG(CAST(cs.costo_producto AS text), ';')
			FROM costos_productos cp
			JOIN costos cs ON cs.id_costo = cp.id_costo
			WHERE cp.id_producto = pr.id_producto),'0') AS costo,*/
			/*
            COALESCE((SELECT STRING_AGG(CAST(pc.precio_producto AS text), ';')
			FROM precios_productos pp
			JOIN precios pc ON pc.id_precio = pp.id_precio
			WHERE pp.id_producto = pr.id_producto),'0') AS precio,*/
			pr.id_subcategoria,
            COALESCE((SELECT nombre_subcategoria FROM subcategorias WHERE id_subcategoria = id_subcategoria), 'NA') as nombre_subcategoria
            FROM productos pr
			GROUP BY pr.id_producto, pr.nombre_producto, pr.aplicacion_productos, pr.cant_producto, pr.cod_producto, pr.id_subcategoria
			ORDER BY pr.id_producto DESC;";

            conexionDB con = new();
            using (NpgsqlConnection v_con = con.conexion())
            {
                try
                {
                    v_con.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(SELECT_PRODUCTOS, v_con))
                    {
                        using (NpgsqlDataReader rd = command.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                classProductos productos = new()
                                {
                                    id_producto = rd.IsDBNull(0) ? 0 : rd.GetInt32(0),
                                    nombre_producto = rd.IsDBNull(1) ? "NA" : rd.GetString(1),
                                    aplicacion_productos = rd.IsDBNull(2) ? "NA" : rd.GetString(2),
                                    cant_producto = rd.IsDBNull(3) ? 0 : rd.GetInt32(3),
                                    cod_producto = rd.IsDBNull(4) ? 0 : rd.GetInt32(4),

                                    beCosto = JsonSerializer.Deserialize<List<classCosto>>(rd.GetString(5)),
                                    bePrecio = JsonSerializer.Deserialize<List<classPrecio>>(rd.GetString(6)),
                                   

                                    beSubcategoria = new()
                                    {
                                        id_subcategoria = rd.IsDBNull(7) ? 0 : rd.GetInt32(7),
                                        nombre_subcategoria = rd.IsDBNull(8) ? "NA" : rd.GetString(8)
                                    }
                                };

                                lProductos.Add(productos);
                            }

                            v_con.Dispose();

                            res = new()
                            {
                                code = 200,
                                status = "S",
                                message = lProductos
                            };
                        }
                        ;
                    };
                }
                catch (Exception ex)
                {
                    res = new()
                    {
                        code = 400,
                        status = "F",
                        message = ex.Message
                    };
                }

                finally
                {
                    v_con.Dispose();
                }
            };

            return res;
        }

        //Método para RegistrarVentas
        public BeRes registrarVentas(classTotProductos totalProductos)
        {
            BeRes res = new();
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
                            res = new()
                            {
                                code = 201,
                                status = "S",
                                message = "Ok."
                            };
                        }
                        v_con.Dispose();
                    };

                }
                catch (Exception ex)
                {
                    res = new()
                    {
                        code = 400,
                        status = "F",
                        message = ex.Message
                    };
                }

                finally
                {
                    v_con.Dispose();
                }

            };
            
            return res;
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
