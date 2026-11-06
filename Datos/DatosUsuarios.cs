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

    // Método para la baja lógica de un usuario (asumiendo que manejas el campo activo o un UPDATE equivalente)
    public int EliminarUsuario(int idUsuario)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "DELETE FROM Usuarios WHERE ID_usuario = @id_usuario";

            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@id_usuario", idUsuario);

            return comando.ExecuteNonQuery();
        }
    }
    // Método para verificar duplicados antes de dar de alta (usando tu columna 'usuario')
    public int VerificarUsuarioExistente(string nombreUsuario, int dni)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();

            // 1. Verificar si el nombre de usuario ya existe
            string consultaNombre = "SELECT COUNT(*) FROM Usuarios WHERE usuario = @usuario";
            using (SqlCommand cmdNombre = new SqlCommand(consultaNombre, sql))
            {
                cmdNombre.Parameters.AddWithValue("@usuario", nombreUsuario);
                int existeNombre = (int)cmdNombre.ExecuteScalar();
                if (existeNombre > 0) return 1; // Código 1: Usuario duplicado
            }

            // 2. Verificar si el DNI ya existe
            string consultaDni = "SELECT COUNT(*) FROM Usuarios WHERE dni = @dni";
            using (SqlCommand cmdDni = new SqlCommand(consultaDni, sql))
            {
                cmdDni.Parameters.AddWithValue("@dni", dni);
                int existeDni = (int)cmdDni.ExecuteScalar();
                if (existeDni > 0) return 2; // Código 2: DNI duplicado
            }
        }
        return 0; // Código 0: Todo libre
    }
}

