public class CursoDto {
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string DescripcionBreve { get; set; }
    public string Imagen { get; set; }
    public double Valoracion { get; set; }  // Valoración promedio de 1 a 5
}

public class CreateCursoDto {
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string DescripcionBreve { get; set; }
    public string Imagen { get; set; }
    
}

public class UpdateCursoDto {
    public string Nombre { get; set; }
    public string DescripcionBreve { get; set; }
    public string Imagen { get; set; }
    public List<UnidadDto> Unidades { get; set; }
}
