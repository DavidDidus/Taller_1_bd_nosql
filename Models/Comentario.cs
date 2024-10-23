using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Comentario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string? Id { get; set; }

    public string ClaseId {get; set;}

    [BsonElement("autor")]
    public string Autor { get; set; }

    [BsonElement("fecha")]
    public DateTime Fecha { get; set; }

    [BsonElement("titulo")]
    public string Titulo { get; set; }

    [BsonElement("detalle")]
    public string Detalle { get; set; }

    public int Valoracion { get; set; }

    public int me_gusta { get; set; }

    public int no_me_gusta { get; set; }
}
