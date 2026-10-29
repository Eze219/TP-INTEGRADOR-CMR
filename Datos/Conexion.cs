using Microsoft.Data.SqlClient;

public class Conexion
{
    private readonly string cadenaConexion =
    @"Server=.\SQLEXPRESS;Database=CMR;Integrated Security=true;TrustServerCertificate=True;";

    public SqlConnection obtenerConexion()
    {
        return new SqlConnection(cadenaConexion);
    }
}