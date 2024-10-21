public class UnidadDto {
    public string Nombre { get; set; }
    public List<ClaseDto> Clases { get; set; }
}

public class CreateUnidadDto {
    public string Nombre { get; set; }
    public List<CreateClaseDto> Clases { get; set; }
}