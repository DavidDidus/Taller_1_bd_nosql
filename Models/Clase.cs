using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Clase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("unidad_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UnidadId { get; set; }

    [BsonElement("numero_orden")]
    public int NumeroOrden { get; set; }

    [BsonElement("nombre")]
    public string Nombre { get; set; }

    [BsonElement("video_url")]
    public string VideoUrl { get; set; }

    [BsonElement("descripcion")]
    public string Descripcion { get; set; }

    [BsonElement("adjuntos")]
    public List<string> Adjuntos { get; set; }

    [BsonElement("comentarios")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> Comentarios { get; set; }
}
