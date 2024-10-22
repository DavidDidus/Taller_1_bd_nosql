using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Curso
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string? Id { get; set; }

    [BsonElement("nombre")]
    public string Nombre { get; set; }

    [BsonElement("descripcion_breve")]
    public string DescripcionBreve { get; set; }

    [BsonElement("imagen_principal")]
    public string ImagenPrincipal { get; set; }

    [BsonElement("imagen_banner")]
    public string ImagenBanner { get; set; }

    [BsonElement("valoracion")]
    public double Valoracion { get; set; }

    [BsonElement("cantidad_inscritos")]
    public int CantidadInscritos { get; set; }
}
