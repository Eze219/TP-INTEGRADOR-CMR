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
            "INSERT INTO Historial (tipo_movimiento, fecha_hora) VALUES (@tipo_movimiento, @fecha_hora)";
           
            SqlCommand comando = new SqlCommand(consulta, sql);

            comando.Parameters.AddWithValue("@tipo_movimiento", historial.TipoMovimiento);
            comando.Parameters.AddWithValue("@fecha_hora", historial.FechaHora);

            return comando.ExecuteNonQuery();
        }
    }
}

