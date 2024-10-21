public class ComentarioDto {
    public string Id { get; set; }
    public string Autor { get; set; }
    public DateTime Fecha { get; set; }
    public string Titulo { get; set; }
    public string Detalle { get; set; }
    public double Valoracion { get; set; }  // De 1.0 a 5.0
    public int MeGusta { get; set; }
    public int NoMeGusta { get; set; }
}

public class CreateComentarioDto {
    public string Autor { get; set; }
    public string Titulo { get; set; }
    public string Detalle { get; set; }
    public double Valoracion { get; set; }  // Valoración inicial
}
