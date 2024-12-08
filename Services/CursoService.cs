using MongoDB.Driver;
using Taller1;

public class CursoService(MongoDbContext context)
{
    private readonly IMongoCollection<Curso> _cursos = context.Cursos;
    private readonly IMongoCollection<Unidad> _unidades = context.Unidades;
    private readonly IMongoCollection<ComentarioCurso> _comentariosCurso = context.ComentariosCurso; 
    private readonly IMongoCollection<Clase> _clases = context.Clases;
    private readonly IMongoCollection<ComentarioClase> _comentariosClase = context.ComentariosClase;

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
        var nuevoCurso = new Curso
        {
            Nombre = curso.Nombre,
            DescripcionBreve = curso.DescripcionBreve,
            ImagenPrincipal = curso.ImagenPrincipal,
            ImagenBanner = curso.ImagenBanner,
            Valoracion = 0,
            CantidadInscritos = 0
        };
        await _cursos.InsertOneAsync(nuevoCurso);
        return curso!;
    }

    public async Task<List<Unidad>> GetCursoContenidoAsyc(string id)
    {
        var curso = await _cursos.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (curso != null)
        {
            var unidades = await _unidades.Find(u =>  u.CursoId == id).SortBy(u => u.NumeroOrden).ToListAsync();
            var unidadesModificadas = new List<Unidad>();

            foreach (var unidad in unidades)
            {
                var clases = await _clases.Find(c => c.UnidadId == unidad.Id).SortBy(c => c.NumeroOrden).ToListAsync();
                unidad.Clases = clases;

                foreach (var clase in clases)
                {
                    var comentarios = await _comentariosClase.Find(c => c.Claseid == clase.Id).ToListAsync();
                    clase.Comentarios = comentarios;
                }
                unidadesModificadas.Add(unidad);
            }
            
            curso.Unidades = unidadesModificadas;

        }
        return curso?.Unidades ?? [];
    }
    
    public async Task<Curso> GetCursoDetalleAsync(string id)
    {
        var curso = await _cursos.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (curso != null)
        {
            var unidades = await _unidades.Find(u =>  u.CursoId == id).SortBy(u => u.NumeroOrden).ToListAsync();
            var unidadesModificadas = new List<Unidad>();

            foreach (var unidad in unidades)
            {
                var clases = await _clases.Find(c => c.UnidadId == unidad.Id).SortBy(c => c.NumeroOrden).ToListAsync();
                unidad.Clases = clases;
                unidadesModificadas.Add(unidad);
            }
            
            curso.Unidades = unidadesModificadas;
            var comentarios = await _comentariosCurso.Find(c => c.CursoId == curso.Id)
                                                     .SortByDescending(c => c.Valoracion)
                                                     .Limit(3)
                                                     .ToListAsync();
            
            curso.Comentarios = comentarios;
        }
        return curso ?? new Curso();
    }

    public async Task PatchCantidadInscritos(string id)
    {
        var curso = await _cursos.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (curso != null)
        {
            curso.CantidadInscritos++;
            await _cursos.ReplaceOneAsync(c => c.Id == id, curso);
        }
    }
}
