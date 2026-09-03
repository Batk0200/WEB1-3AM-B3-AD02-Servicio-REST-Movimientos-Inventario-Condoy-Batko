namespace ServicioRESTInventario.Models
{
    public class MovimientoInventario
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;  // "Entrada" o "Salida"
        public int Cantidad { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string? Observacion { get; set; }
        public bool Estado { get; set; }
    }
}