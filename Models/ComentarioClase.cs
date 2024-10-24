using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ComentarioClase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string? Id { get; set; }

    [BsonElement("unidadId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UnidadId { get; set; }

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
