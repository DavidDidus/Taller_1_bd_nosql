using MongoDB.Driver;

using Taller1;

public class CursoService
{
    private readonly IMongoCollection<Curso> _cursos;
    private readonly IMongoCollection<Unidad> _unidades;
    private readonly IMongoCollection<ComentarioCurso> _comentariosCurso; 
    private readonly IMongoCollection<Clase> _clases;

    public CursoService(MongoDbContext context)
    {
        _cursos = context.Cursos;
        _unidades = context.Unidades;
        _comentariosCurso = context.ComentariosCurso;
        _clases = context.Clases;

    }

    public async Task<List<Curso>> GetCursosAsync()
    {
        return await _cursos.Find(curso => true).ToListAsync();
    }

    public async Task<Curso> GetCursoByIdAsync(string id)
    {
        return await _cursos.Find(curso => curso.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Curso> CreateCursoAsync(Curso curso)
    {
        await _cursos.InsertOneAsync(curso);
        return curso;
    }
    public async Task<Curso> GetCursoDetalleAsync(string id)
    {
        var curso = await _cursos.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (curso != null)
        {
            // Obtener unidades
            var unidades = await _unidades.Find(u =>  u.CursoId == id).ToListAsync();
            var unidadesModificadas = new List<Unidad>();

            foreach (var unidad in unidades)
            {
                // Obtener clases
                var clases = await _clases.Find(c => c.UnidadId == unidad.Id).ToListAsync();
                unidad.Clases = clases;
                unidadesModificadas.Add(unidad);
            }
            
            curso.Unidades = unidadesModificadas;

            // Obtener comentarios más valorados del curso
            List<ComentarioCurso> comentarios = [];
            foreach (var unidad in unidades)
            {
                var comentariosUnidad = await _comentariosCurso.Find(c => c.CursoId == curso.Id)
                                                        .SortByDescending(c => c.Valoracion)
                                                        .Limit(3)
                                                        .ToListAsync();
                comentarios.AddRange(comentariosUnidad);
            }

            // Ordenar los comentarios por valoración y tomar los 3 más valorados
            curso.Comentarios = comentarios.OrderByDescending(c => c.Valoracion).Take(3).ToList();
        }
        return curso;
    }

    public async Task<bool> UpdateCursoAsync(string id, Curso cursoUpdate)
    {
        var result = await _cursos.ReplaceOneAsync(curso => curso.Id == id, cursoUpdate);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteCursoAsync(string id)
    {
        var result = await _cursos.DeleteOneAsync(curso => curso.Id == id);
        return result.DeletedCount > 0;
    }
}
