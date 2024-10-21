using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class Comentario {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string CursoId { get; set; }
    public string Autor { get; set; }
    public string Titulo { get; set; }
    public string Detalle { get; set; }
    public double Valoracion { get; set; }  // Por ejemplo, 4.5, 3.0, etc.
    public DateTime Fecha { get; set; }
}
