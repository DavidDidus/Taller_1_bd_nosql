using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ComentarioCurso
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("cursoId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CursoId { get; set; }

    [BsonElement("autor")]
    public string? Autor { get; set; }

    [BsonElement("fecha")]
    public DateTime Fecha { get; set; }

    [BsonElement("titulo")]
    public string? Titulo { get; set; }

    [BsonElement("detalle")]
    public string? Detalle { get; set; }

    [BsonElement("valoracion")]
    public int Valoracion { get; set; }
    
    [BsonElement("me_gusta")]
    public int Me_gusta { get; set; }
    
    [BsonElement("no_me_gusta")]
    public int No_me_gusta { get; set; }
}