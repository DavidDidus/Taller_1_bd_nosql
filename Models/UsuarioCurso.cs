namespace Taller1 {
    public class UsuarioCurso {
        public required string IdUsuario { get; set; }
        public required string IdCurso { get; set; }
        public bool? Completado { get; set; }
        public int? Progreso { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaInicio { get; set; }
    }
}