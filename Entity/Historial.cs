public class Historial
{
    public int IdHistorial { get; set; }
    public string TipoMovimiento { get; set; } // "Entrada" o "Salida"
    public DateTime FechaHora { get; set; }
    public int IdUsuario { get; set; }
    public int? IdRepuesto { get; set; }
    public int? IdInsumo { get; set; }
}