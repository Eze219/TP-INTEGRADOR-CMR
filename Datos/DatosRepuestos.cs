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
}