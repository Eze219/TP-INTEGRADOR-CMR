public class Historial
{
    public int IdHistorial { get; set; }
    public string TipoMovimiento { get; set; } // "Entrada" o "Salida"
    public DateTime FechaHora { get; set; }
    public int IdUsuario { get; set; }
    public int? IdRepuesto { get; set; }
    public int? IdInsumo { get; set; }

    // Propiedades virtuales para mostrar los nombres en la tabla (no se mapean en la inserción)
    public string NombreUsuario { get; set; }
    public string NombreElemento { get; set; }
}