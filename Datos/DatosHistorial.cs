using Microsoft.Data.SqlClient;
using System.Data;

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

            // Manejo seguro para ID_repuesto (si es null, manda DBNull a SQL)
            if (historial.IdRepuesto.HasValue)
            {
                comando.Parameters.AddWithValue("@ID_repuesto", historial.IdRepuesto.Value);
            }
            else
            {
                comando.Parameters.Add(new SqlParameter("@ID_repuesto", SqlDbType.Int)).Value = DBNull.Value;
            }

            // Manejo seguro para ID_insumo (si es null, manda DBNull a SQL)
            if (historial.IdInsumo.HasValue)
            {
                comando.Parameters.AddWithValue("@ID_insumo", historial.IdInsumo.Value);
            }
            else
            {
                comando.Parameters.Add(new SqlParameter("@ID_insumo", SqlDbType.Int)).Value = DBNull.Value;
            }

            return comando.ExecuteNonQuery();
        }
    }

    public Historial ObtenerHistorial(string tipoMovimiento)
    {
        Conexion conexion = new Conexion();

        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "SELECT tipo_movimiento, fecha_hora, ID_usuario, ID_repuesto, ID_insumo FROM Historial WHERE tipo_movimiento = @tipo_movimiento";
            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@tipo_movimiento", tipoMovimiento);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    Historial movimiento = new Historial();
                    movimiento.TipoMovimiento = reader.GetString(0);
                    movimiento.FechaHora = reader.GetDateTime(1);
                    movimiento.IdUsuario = reader.GetInt32(2);
                    
                    // Manejo de nulos para IDs opcionales en lectura
                    movimiento.IdRepuesto = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                    movimiento.IdInsumo = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4);

                    return movimiento;
                }
            }
        }
        return null;
    }
}