using Microsoft.Data.SqlClient;

public class DatosInsumos
{
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
}