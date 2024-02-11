using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class conexionDB
    {
        public NpgsqlConnection conexion()
        {
            NpgsqlConnection con = new NpgsqlConnection("Server=localhost;Port=5432;Database=sistemaVentas;User ID=postgres;Password=31415926");

            return con;
        }
    }
}
