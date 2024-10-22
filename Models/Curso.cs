using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class Curso {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Nombre { get; set; }
    public string DescripcionCorta { get; set; }
    public string DescripcionLarga { get; set; }
    public string Imagen { get; set; }
    public string Banner { get; set; }
    public double Valoracion { get; set; }
    public int Inscritos { get; set; }
   

}