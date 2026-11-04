using Microsoft.Data.SqlClient;

public class DatosRepuestos
{
    // Guardar nuevos repuestos
    public int GuardarRepuestos(Repuestos repuesto)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta =
            "INSERT INTO Repuestos (nombre, cantidad_disponible) VALUES (@nombre, @cantidad_disponible)";
            SqlCommand comando = new SqlCommand(consulta, sql);

            comando.Parameters.AddWithValue("@nombre", repuesto.Nombre);
            comando.Parameters.AddWithValue("@cantidad_disponible", repuesto.CantidadDisponible);

            return comando.ExecuteNonQuery();
        }
    }
    // Obtener repuestos buscando por nombre
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
    // Obtener todos los Repuestos
    public List<Repuestos> ObtenerTodosLosRepuestos()
    {
        List<Repuestos> lista = new List<Repuestos>();
        Conexion conexion = new Conexion();

        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "SELECT nombre, cantidad_disponible FROM Repuestos";
            SqlCommand comando = new SqlCommand(consulta, sql);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Repuestos r = new Repuestos();
                    r.Nombre = reader.GetString(0);
                    r.CantidadDisponible = reader.GetInt32(1);
                    lista.Add(r);
                }
            }
        }
        return lista;
    }
}