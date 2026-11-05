using Microsoft.Data.SqlClient;

public class DatosUsuarios
{
    public int GuardarUsuario(Usuarios usuario)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta =
            "INSERT INTO Usuarios (nombre, usuario, contraseña, dni, rol) VALUES (@nombre, @usuario, @contraseña, @dni, @rol)";
            SqlCommand comando = new SqlCommand(consulta, sql);

            comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@usuario", usuario.UsuarioNombre);
            comando.Parameters.AddWithValue("@contraseña", usuario.Contrasena);
            comando.Parameters.AddWithValue("@dni", usuario.Dni);
            comando.Parameters.AddWithValue("@rol", usuario.Rol);

            return comando.ExecuteNonQuery();
        }
    }
   public Usuarios ObtenerUsuarioPorNombre(string nombreUsuario)
{
    Conexion conexion = new Conexion();
    using (SqlConnection sql = conexion.obtenerConexion())
    {
        sql.Open();
        string consulta = "SELECT ID_usuario, usuario, contraseña, rol, nombre, dni FROM Usuarios WHERE usuario = @usuario";
        SqlCommand comando = new SqlCommand(consulta, sql);
        comando.Parameters.AddWithValue("@usuario", nombreUsuario);

        using (SqlDataReader reader = comando.ExecuteReader())
        {
            if (reader.Read())
            {
                Usuarios usuario = new Usuarios();
                
                // Aquí lees el ID que ya generó y guardó SQL Server de forma autoincremental:
                usuario.IdUsuario = reader.GetInt32(0); 
                usuario.UsuarioNombre = reader.GetString(1);
                usuario.Contrasena = reader.GetString(2);
                usuario.Rol = reader.GetString(3);
                usuario.Nombre = reader.GetString(4);
                usuario.Dni = reader.GetInt32(5);

                return usuario;
            }
        }
    }
    return null;
}
}

