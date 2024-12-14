using Neo4j.Driver;

public class ComentarioCursoService
{
    private readonly IDriver _driver;

    public ComentarioCursoService(IDriver driver)
    {
        _driver = driver;
    }

    public async Task CrearComentarioCursoAsync(string cursoId, string autor, string titulo, string detalle, int valoracion, int meGusta, int noMeGusta)
    {
        var session = _driver.AsyncSession();
        try
        {
            await session.ExecuteWriteAsync(async tx =>
            {
                Console.WriteLine($"Parametros: cursoId={cursoId}, autor={autor}, titulo={titulo}, detalle={detalle}, valoracion={valoracion}, meGusta={meGusta}, noMeGusta={noMeGusta}");

                var result = await tx.RunAsync(
                    "CREATE (com:ComentarioCurso {id: apoc.create.uuid(), cursoId: $cursoId, autor: $autor, titulo: $titulo, detalle: $detalle, valoracion: $valoracion, me_gusta: $meGusta, no_me_gusta: $noMeGusta, fecha: datetime()})",
                    new { cursoId, autor, titulo, detalle, valoracion, meGusta, noMeGusta });

                var summary = await result.ConsumeAsync();
                Console.WriteLine($"Comentario creado: {summary.Counters.NodesCreated} nodos creados.");
            });
        }
        catch (Exception ex)
        {
            // Manejar la excepción
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public async Task<ComentarioCurso[]> ObtenerComentariosCursoAsync(string cursoId)
    {
        var session = _driver.AsyncSession();
        try
        {
            var result = await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(
                    "MATCH (com:ComentarioCurso {cursoId: $cursoId}) " +
                    "RETURN com.id AS id, com.cursoId AS cursoId, com.autor AS autor, com.titulo AS titulo, com.detalle AS detalle, com.valoracion AS valoracion, com.me_gusta AS meGusta, com.no_me_gusta AS noMeGusta, com.fecha AS fecha",
                    new { cursoId });

                var comentarios = await cursor.ToListAsync(record => new ComentarioCurso
                {
                    Id = record["id"].As<string>(),
                    CursoId = record["cursoId"].As<string>(),
                    Autor = record["autor"].As<string>(),
                    Titulo = record["titulo"].As<string>(),
                    Detalle = record["detalle"].As<string>(),
                    Valoracion = record["valoracion"].As<int>(),
                    Me_gusta = record["meGusta"].As<int>(),
                    No_me_gusta = record["noMeGusta"].As<int>(),
                    Fecha = record["fecha"].As<ZonedDateTime>().ToDateTimeOffset().DateTime
                });

                return comentarios;
            });

            return result.ToArray();
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public async Task<ComentarioCurso?> ObtenerComentarioCursoPorIdAsync(string id)
    {
        var session = _driver.AsyncSession();
        try
        {
            var result = await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(
                    "MATCH (com:ComentarioCurso {id: $id}) " +
                    "RETURN com.id AS id, com.cursoId AS cursoId, com.autor AS autor, com.titulo AS titulo, com.detalle AS detalle, com.valoracion AS valoracion, com.me_gusta AS meGusta, com.no_me_gusta AS noMeGusta, com.fecha AS fecha",
                    new { id });

                var records = await cursor.ToListAsync();
                var record = records.SingleOrDefault();
                if (record == null)
                {
                    return null;
                }

                return new ComentarioCurso
                {
                    Id = record["id"].As<string>(),
                    CursoId = record["cursoId"].As<string>(),
                    Autor = record["autor"].As<string>(),
                    Titulo = record["titulo"].As<string>(),
                    Detalle = record["detalle"].As<string>(),
                    Valoracion = record["valoracion"].As<int>(),
                    Me_gusta = record["meGusta"].As<int>(),
                    No_me_gusta = record["noMeGusta"].As<int>(),
                    Fecha = record["fecha"].As<ZonedDateTime>().ToDateTimeOffset().DateTime
                };
            });

            return result;
        }
        finally
        {
            await session.CloseAsync();
        }
    }
}
