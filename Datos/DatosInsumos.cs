using Microsoft.Data.SqlClient;

public class DatosInsumos
{
    // Guardar insumos nuevos
    public int GuardarInsumos(Insumos insumo)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta =
            "INSERT INTO Insumos (nombre, cantidad_disponible, unidad_medida) VALUES (@nombre, @cantidad_disponible, @unidad_medida)";
            SqlCommand comando = new SqlCommand(consulta, sql);

            comando.Parameters.AddWithValue("@nombre", insumo.Nombre);
            comando.Parameters.AddWithValue("@cantidad_disponible", insumo.CantidadDisponible);
            comando.Parameters.AddWithValue("@unidad_medida", insumo.UnidadMedida);

            return comando.ExecuteNonQuery();
        }
    }
    // Buscar insumos por nombre
    public Insumos ObtenerInsumoPorNombre(string nombreInsumo)
    {
        Conexion conexion = new Conexion();

        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "SELECT nombre, cantidad_disponible, unidad_medida FROM Insumos WHERE nombreInsumo = @nombre";
            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@nombre", nombreInsumo);
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    Insumos insumo = new Insumos();
                    insumo.Nombre = reader.GetString(0);
                    insumo.CantidadDisponible = reader.GetInt32(1);
                    insumo.UnidadMedida = reader.GetString(2);

                    return insumo;
                }
            }
        }
        return null;
    }
    // Obtener todos los insumos
    public List<Insumos> ObtenerTodosLosInsumos()
    {
        List<Insumos> lista = new List<Insumos>();
        Conexion conexion = new Conexion();

        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "SELECT nombre, cantidad_disponible, unidad_medida FROM Insumos";
            SqlCommand comando = new SqlCommand(consulta, sql);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Insumos i = new Insumos();
                    i.Nombre = reader.GetString(0);
                    i.CantidadDisponible = reader.GetInt32(1);
                    i.UnidadMedida = reader.GetString(2);
                    lista.Add(i);
                }
            }
        }
        return lista;
    }
}