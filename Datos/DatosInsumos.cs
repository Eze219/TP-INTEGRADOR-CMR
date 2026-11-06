using Microsoft.Data.SqlClient;
using System.Collections.Generic;

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

    // Buscar insumos por nombre (¡Agregado ID_insumo para que funcione el historial!)
    public Insumos ObtenerInsumoPorNombre(string nombreInsumo)
    {
        Conexion conexion = new Conexion();

        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "SELECT ID_insumo, nombre, cantidad_disponible, unidad_medida FROM Insumos WHERE nombre = @nombre";
            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@nombre", nombreInsumo);
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    Insumos insumo = new Insumos();
                    insumo.IdInsumo = reader.GetInt32(0);
                    insumo.Nombre = reader.GetString(1);
                    insumo.CantidadDisponible = reader.GetInt32(2);
                    insumo.UnidadMedida = reader.GetString(3);

                    return insumo;
                }
            }
        }
        return null;
    }

    // Obtener todos los insumos
    // Obtener solo los insumos activos
   public List<Insumos> ObtenerTodosLosInsumos()
{
    List<Insumos> lista = new List<Insumos>();
    Conexion conexion = new Conexion();

    using (SqlConnection sql = conexion.obtenerConexion())
    {
        sql.Open();
        string consulta = "SELECT ID_insumo, nombre, cantidad_disponible, unidad_medida FROM Insumos";
        SqlCommand comando = new SqlCommand(consulta, sql);

        using (SqlDataReader reader = comando.ExecuteReader())
        {
            while (reader.Read())
            {
                Insumos i = new Insumos();
                
                i.IdInsumo = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                i.Nombre = reader.IsDBNull(1) ? "Sin nombre" : reader.GetString(1);
                i.CantidadDisponible = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                i.UnidadMedida = reader.IsDBNull(3) ? "N/A" : reader.GetString(3);

                lista.Add(i);
            }
        }
    }
    return lista;
}

    // Actualizar el stock o datos de un insumo existente
    public int ActualizarInsumo(Insumos insumo)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "UPDATE Insumos SET nombre = @nombre, cantidad_disponible = @cantidad_disponible, unidad_medida = @unidad_medida WHERE ID_insumo = @id_insumo";

            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@nombre", insumo.Nombre);
            comando.Parameters.AddWithValue("@cantidad_disponible", insumo.CantidadDisponible);
            comando.Parameters.AddWithValue("@unidad_medida", insumo.UnidadMedida);
            comando.Parameters.AddWithValue("@id_insumo", insumo.IdInsumo);

            return comando.ExecuteNonQuery();
        }
    }
    // Baja Lógica para Insumos (UPDATE en vez de DELETE)
    public int EliminarInsumo(int idInsumo)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta = "UPDATE Insumos SET activo = 0 WHERE ID_insumo = @id";
            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@id", idInsumo);
            return comando.ExecuteNonQuery();
        }
    }
}