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
            "INSERT INTO Repuestos (nombre, cantidad_disponible, stock_minimo) VALUES (@nombre, @cantidad_disponible, @stock_minimo)";
            SqlCommand comando = new SqlCommand(consulta, sql);

            comando.Parameters.AddWithValue("@nombre", repuesto.Nombre);
            comando.Parameters.AddWithValue("@cantidad_disponible", repuesto.CantidadDisponible);
            comando.Parameters.AddWithValue("@stock_minimo", repuesto.StockMinimo);

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

            // 1. Aquí actualizas la consulta (cambia 'nombreRepuesto' por 'nombre' 
            // y agregas 'id_repuesto' al principio):
            string consulta = "SELECT id_repuesto, nombre, cantidad_disponible, stock_minimo FROM Repuestos WHERE nombre = @nombre";

            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@nombre", nombreRepuesto);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    Repuestos repuesto = new Repuestos();

                    // 2. Asignamos los valores teniendo en cuenta el orden de las columnas del SELECT:
                    repuesto.IdRepuesto = reader.GetInt32(0);
                    repuesto.Nombre = reader.GetString(1);
                    repuesto.CantidadDisponible = reader.GetInt32(2);
                    repuesto.StockMinimo = reader.GetInt32(3); // Asegúrate de que la columna 'stock_minimo' esté incluida en tu SELECT si deseas obtenerla.

                    return repuesto;
                }
            }
        }
        return null;
    }
    // Obtener todos los Repuestos
    // Obtener solo los repuestos activos
    public List<Repuestos> ObtenerTodosLosRepuestos()
    {
        List<Repuestos> lista = new List<Repuestos>();
        Conexion conexion = new Conexion();

        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "SELECT id_repuesto, nombre, cantidad_disponible, stock_minimo FROM Repuestos";
            SqlCommand comando = new SqlCommand(consulta, sql);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Repuestos r = new Repuestos();

                    r.IdRepuesto = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    r.Nombre = reader.IsDBNull(1) ? "Sin nombre" : reader.GetString(1);

                    // Protegemos campos numéricos que puedan venir nulos en la BD
                    r.CantidadDisponible = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                    r.StockMinimo = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);

                    lista.Add(r);
                }
            }
        }
        return lista;
    }

    // Actualizar el stock o nombre de un repuesto existente
    public int ActualizarRepuesto(Repuestos repuesto)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "UPDATE Repuestos SET nombre = @nombre, cantidad_disponible = @cantidad_disponible, stock_minimo = @stock_minimo WHERE id_repuesto = @id_repuesto";

            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@nombre", repuesto.Nombre);
            comando.Parameters.AddWithValue("@cantidad_disponible", repuesto.CantidadDisponible);
            comando.Parameters.AddWithValue("@stock_minimo", repuesto.StockMinimo);
            comando.Parameters.AddWithValue("@id_repuesto", repuesto.IdRepuesto);

            return comando.ExecuteNonQuery();
        }
    }
    // Baja Lógica (Actualiza activo a 0 en vez de hacer DELETE)
    public int EliminarRepuesto(int idRepuesto)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "UPDATE Repuestos SET activo = 0 WHERE id_repuesto = @id";
            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@id", idRepuesto);
            return comando.ExecuteNonQuery();
        }
    }
}