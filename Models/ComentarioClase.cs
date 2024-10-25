using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ComentarioClase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string? Id { get; set; }

    [BsonElement("claseid")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Claseid { get; set; } 

    [BsonElement("autor")]
    public string Autor { get; set; }

    [BsonElement("fecha")]
    public DateTime Fecha { get; set; }

    [BsonElement("titulo")]
    public string Titulo { get; set; }

    [BsonElement("detalle")]
    public string Detalle { get; set; }

    [BsonElement("valoracion")]
    public int Valoracion { get; set; }
    
    [BsonElement("me_gusta")]
    public int me_gusta { get; set; }
    
    [BsonElement("no_me_gusta")]
    public int no_me_gusta { get; set; }
}
