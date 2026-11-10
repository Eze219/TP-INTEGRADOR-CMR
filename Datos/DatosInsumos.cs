using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class DatosInsumos
{
    // Guardar insumos nuevos (incluyendo stock mínimo)
    public int GuardarInsumos(Insumos insumo)
    {
        Conexion conexion = new Conexion();
        using (SqlConnection sql = conexion.obtenerConexion())
        {
            sql.Open();
            string consulta =
            "INSERT INTO Insumos (nombre, cantidad_disponible, unidad_medida, stock_minimo) VALUES (@nombre, @cantidad_disponible, @unidad_medida, @stock_minimo)";
            SqlCommand comando = new SqlCommand(consulta, sql);

            comando.Parameters.AddWithValue("@nombre", insumo.Nombre);
            comando.Parameters.AddWithValue("@cantidad_disponible", insumo.CantidadDisponible);
            comando.Parameters.AddWithValue("@unidad_medida", insumo.UnidadMedida);
            comando.Parameters.AddWithValue("@stock_minimo", insumo.StockMinimo);

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
            string consulta = "SELECT ID_insumo, nombre, cantidad_disponible, unidad_medida, stock_minimo FROM Insumos WHERE nombre = @nombre";
            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@nombre", nombreInsumo);
            
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    Insumos insumo = new Insumos();
                    insumo.IdInsumo = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    insumo.Nombre = reader.IsDBNull(1) ? "Sin nombre" : reader.GetString(1);
                    insumo.CantidadDisponible = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                    insumo.UnidadMedida = reader.IsDBNull(3) ? "N/A" : reader.GetString(3);
                    insumo.StockMinimo = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);

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
            string consulta = "SELECT ID_insumo, nombre, cantidad_disponible, unidad_medida, stock_minimo FROM Insumos";
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
                    i.StockMinimo = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);

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
            string consulta = "UPDATE Insumos SET nombre = @nombre, cantidad_disponible = @cantidad_disponible, unidad_medida = @unidad_medida, stock_minimo = @stock_minimo WHERE ID_insumo = @id_insumo";

            SqlCommand comando = new SqlCommand(consulta, sql);
            comando.Parameters.AddWithValue("@nombre", insumo.Nombre);
            comando.Parameters.AddWithValue("@cantidad_disponible", insumo.CantidadDisponible);
            comando.Parameters.AddWithValue("@unidad_medida", insumo.UnidadMedida);
            comando.Parameters.AddWithValue("@stock_minimo", insumo.StockMinimo);
            comando.Parameters.AddWithValue("@id_insumo", insumo.IdInsumo);

            return comando.ExecuteNonQuery();
        }
    }

    // Baja Lógica para Insumos
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