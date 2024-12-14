public class ComentarioCurso
{
    public string? Id { get; set; }
    public string? CursoId { get; set; }
    public string? Autor { get; set; }
    public DateTime Fecha { get; set; }
    public string? Titulo { get; set; }
    public string? Detalle { get; set; }
    public int Valoracion { get; set; }
    public int Me_gusta { get; set; }
    public int No_me_gusta { get; set; }
}

public class ComentarioCursoRequest
{
    public required string CursoId { get; set; }
    public required string Autor { get; set; }
    public required string Titulo { get; set; }
    public required string Detalle { get; set; }
    public int Valoracion { get; set; }
    public int Me_gusta { get; set; }
    public int No_me_gusta { get; set; }
}