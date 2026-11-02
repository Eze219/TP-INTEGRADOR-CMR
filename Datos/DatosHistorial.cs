using Microsoft.Data.SqlClient;

public class DatosHistorial
{
    public int GuardarMovimientos(Historial historial)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta =
            "INSERT INTO Historial (tipo_movimiento, fecha_hora, ID_usuario, ID_repuesto, ID_insumo) VALUES (@tipo_movimiento, @fecha_hora, @ID_usuario, @ID_repuesto, @ID_insumo)";
           
            SqlCommand comando = new SqlCommand(consulta, sql);

            comando.Parameters.AddWithValue("@tipo_movimiento", historial.TipoMovimiento);
            comando.Parameters.AddWithValue("@fecha_hora", historial.FechaHora);
            comando.Parameters.AddWithValue("@ID_usuario", historial.IdUsuario);
            comando.Parameters.AddWithValue("@ID_repuesto", historial.IdRepuesto);
            comando.Parameters.AddWithValue("@ID_insumo", historial.IdInsumo);

            return comando.ExecuteNonQuery();
        }
    }
        public Historial ObtenerHistorial(string TipoMovimiento)
    {
        Conexion conexion = new Conexion();

        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "SELECT tipo_movimiento, fecha_hora, ID_usuario, ID_repuesto, ID_insumo FROM Historial WHERE tipoMovimiento = @tipo_movimiento";
            SqlCommand comando = new SqlCommand(consulta, sql);
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    Historial movimiento = new Historial();
                    movimiento.TipoMovimiento = reader.GetString(0);
                    movimiento.FechaHora = reader.GetDateTime(1);
                    movimiento.IdUsuario = reader.GetInt32(2);
                    movimiento.IdRepuesto = reader.GetInt32(3);
                    movimiento.IdInsumo = reader.GetInt32(4);

                    return movimiento;
                }
            }
        }
        return null;
    }
}

