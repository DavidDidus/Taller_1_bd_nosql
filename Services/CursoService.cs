using MongoDB.Driver;

using Taller1;

public class CursoService
{
    private readonly IMongoCollection<Curso> _cursos;
    private readonly IMongoCollection<Unidad> _unidades;
    private readonly IMongoCollection<ComentarioCurso> _comentariosCurso; 
    private readonly IMongoCollection<Clase> _clases;
    private readonly IMongoCollection<ComentarioClase> _comentariosClase;

    public CursoService(MongoDbContext context)
    {
        _cursos = context.Cursos;
        _unidades = context.Unidades;
        _comentariosCurso = context.ComentariosCurso;
        _clases = context.Clases;
        _comentariosClase = context.ComentariosClase;

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
    public async Task<List<Unidad>> GetCursoContenidoAsyc(string id)
    {
        var curso = await _cursos.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (curso != null)
        {
            // Obtener unidades
            var unidades = await _unidades.Find(u =>  u.CursoId == id).SortBy(u => u.NumeroOrden).ToListAsync();
            var unidadesModificadas = new List<Unidad>();

            foreach (var unidad in unidades)
            {
                // Obtener clases
                var clases = await _clases.Find(c => c.UnidadId == unidad.Id).SortBy(c => c.NumeroOrden).ToListAsync();
                unidad.Clases = clases;

                foreach (var clase in clases)
                {
                    // Obtener comentarios de la clase
                    var comentarios = await _comentariosClase.Find(c => c.Claseid == clase.Id).ToListAsync();
                    clase.Comentarios = comentarios;
                }
                unidadesModificadas.Add(unidad);
            }
            
            curso.Unidades = unidadesModificadas;

        }
        return curso.Unidades;
    }
    
    public async Task<Curso> GetCursoDetalleAsync(string id)
    {
        var curso = await _cursos.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (curso != null)
        {
            // Obtener unidades
            var unidades = await _unidades.Find(u =>  u.CursoId == id).SortBy(u => u.NumeroOrden).ToListAsync();
            var unidadesModificadas = new List<Unidad>();

            foreach (var unidad in unidades)
            {
                // Obtener clases
                var clases = await _clases.Find(c => c.UnidadId == unidad.Id).SortBy(c => c.NumeroOrden).ToListAsync();
                unidad.Clases = clases;
                unidadesModificadas.Add(unidad);
            }
            
            curso.Unidades = unidadesModificadas;

            // Obtener comentarios del curso
            var comentarios = await _comentariosCurso.Find(c => c.CursoId == curso.Id)
                                                     .SortByDescending(c => c.Valoracion)
                                                     .Limit(3)
                                                     .ToListAsync();
            
            curso.Comentarios = comentarios;
        }
        return curso;
    }
    
}
