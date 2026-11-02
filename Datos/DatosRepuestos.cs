using Microsoft.Data.SqlClient;

public class DatosRepuestos
{
    public int GuardarRepuestos(Repuestos repuesto)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta =
            "INSERT INTO Insumos (nombre, cantidad_disponible) VALUES (@nombre, @cantidad_disponible)";
            SqlCommand comando = new SqlCommand(consulta, sql);

            comando.Parameters.AddWithValue("@nombre", repuesto.Nombre);
            comando.Parameters.AddWithValue("@cantidad_disponible", repuesto.CantidadDisponible);

            return comando.ExecuteNonQuery();
        }
    }
    public Repuestos ObtenerRepuestoPorNombre(string nombreRepuesto)
    {
        Conexion conexion = new Conexion();

        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "SELECT nombre, cantidad_disponible FROM Repuestos WHERE nombreRepuesto = @nombre";
            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@nombre", nombreRepuesto);
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    Repuestos repuesto = new Repuestos();
                    repuesto.Nombre = reader.GetString(0);
                    repuesto.CantidadDisponible = reader.GetInt32(1);

                    return repuesto;
                }
            }
        }
        return null;
    }
}