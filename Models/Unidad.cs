using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Unidad
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("curso_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CursoId { get; set; }

    [BsonElement("numero_orden")]
    public int NumeroOrden { get; set; }

    [BsonElement("nombre")]
    public string? Nombre { get; set; }

    [BsonElement("clases")]
    public List<Clase>? Clases { get; set; }
}