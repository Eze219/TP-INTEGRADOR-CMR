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
}