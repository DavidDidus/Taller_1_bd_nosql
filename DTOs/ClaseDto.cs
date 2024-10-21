public class ClaseDto {
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string VideoUrl { get; set; }  // URL del video almacenado externamente
    public List<string> MaterialesAdjuntos { get; set; }  // Archivos descargables
}

public class CreateClaseDto {
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string VideoUrl { get; set; }
    public List<string> MaterialesAdjuntos { get; set; }
}