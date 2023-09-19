using Microsoft.EntityFrameworkCore;
using ProjetoInterdisciplinarII.Models;
using ProjetoInterdisciplinarII.Models.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>();
var app = builder.Build();

app.MapGet("/api/usuarios", async (DataContext dbContext) =>
{
  var usuarios = await dbContext.Usuarios.ToListAsync();
  return Results.Ok(usuarios);
});

app.MapPost("/api/usuarios", async (Usuario usuario, DataContext dbContext) =>
{
  dbContext.Usuarios.Add(usuario);
  await dbContext.SaveChangesAsync();
  return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
});

app.MapGet("/api/usuarios/{id}", async (int id, DataContext dbContext) =>
{
  var usuario = await dbContext.Usuarios.FindAsync(id);
  if (usuario == null)
  {
    return Results.NotFound();
  }
  return Results.Ok(usuario);
});

app.MapGet("/api/usuarios/signin", async (string login, string senha, DataContext dbContext) =>
{
  var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Login == login && u.Senha == senha);
  if (usuario == null)
  {
    return Results.NotFound();
  }
  return Results.Ok(usuario);
});

app.MapPut("/api/usuarios/{id}", async (int id, Usuario novoUsuario, DataContext dbContext) =>
{
  var usuarioExistente = await dbContext.Usuarios.FindAsync(id);
  if (usuarioExistente == null)
  {
    return Results.NotFound();
  }

  usuarioExistente = novoUsuario;

  await dbContext.SaveChangesAsync();
  return Results.NoContent();
});

app.MapDelete("/api/usuarios/{id}", async (int id, DataContext dbContext) =>
{
  var usuario = await dbContext.Usuarios.FindAsync(id);
  if (usuario == null)
  {
    return Results.NotFound();
  }

  dbContext.Usuarios.Remove(usuario);
  await dbContext.SaveChangesAsync();
  return Results.NoContent();
});

app.MapGet("/api/postagens", async (DataContext dbContext) =>
{
  var postagens = await dbContext.Postagens.ToListAsync();
  return Results.Ok(postagens);
});

app.MapPost("/api/postagens", async (Postagem postagem, DataContext dbContext) =>
{
  dbContext.Postagens.Add(postagem);
  await dbContext.SaveChangesAsync();
  return Results.Created($"/api/postagens/{postagem.Id}", postagem);
});

app.MapGet("/api/postagens/{id}", async (int id, DataContext dbContext) =>
{
  var postagem = await dbContext.Postagens
      .Include(p => p.Curtidas)
      .Include(p => p.Comentarios)
      .SingleOrDefaultAsync(p => p.Id == id);

  if (postagem == null)
  {
    return Results.NotFound();
  }

  return Results.Ok(postagem);
});

app.MapGet("/api/postagens/usuario/{idUsuario}", async (int idUsuario, DataContext dbContext) =>
{
  var postagensDoUsuario = await dbContext.Postagens
      .Where(p => p.IdUsuarioFk == idUsuario)
      .ToListAsync();

  return Results.Ok(postagensDoUsuario);
});

app.MapPut("/api/postagens/{id}", async (int id, Postagem novaPostagem, DataContext dbContext) =>
{
  var postagemExistente = await dbContext.Postagens.FindAsync(id);
  if (postagemExistente == null)
  {
    return Results.NotFound();
  }

  postagemExistente = novaPostagem;

  await dbContext.SaveChangesAsync();
  return Results.NoContent();
});

app.MapDelete("/api/postagens/{id}", async (int id, DataContext dbContext) =>
{
  var postagem = await dbContext.Postagens.FindAsync(id);
  if (postagem == null)
  {
    return Results.NotFound();
  }

  dbContext.Postagens.Remove(postagem);
  await dbContext.SaveChangesAsync();
  return Results.NoContent();
});

app.MapPost("/api/curtidas", async (Curtida curtida, DataContext dbContext) =>
{
  dbContext.Curtidas.Add(curtida);
  await dbContext.SaveChangesAsync();
  return Results.Created($"/api/curtidas/{curtida.Id}", curtida);
});

app.MapDelete("/api/curtidas/{id}", async (int id, DataContext dbContext) =>
{
  var curtida = await dbContext.Curtidas.FindAsync(id);
  if (curtida == null)
  {
    return Results.NotFound();
  }

  dbContext.Curtidas.Remove(curtida);
  await dbContext.SaveChangesAsync();
  return Results.NoContent();
});

app.MapPost("/api/comentarios", async (Comentario comentario, DataContext dbContext) =>
{
  dbContext.Comentarios.Add(comentario);
  await dbContext.SaveChangesAsync();
  return Results.Created($"/api/comentarios/{comentario.Id}", comentario);
});

app.MapDelete("/api/comentarios/{id}", async (int id, DataContext dbContext) =>
{
  var comentario = await dbContext.Comentarios.FindAsync(id);
  if (comentario == null)
  {
    return Results.NotFound();
  }

  dbContext.Comentarios.Remove(comentario);
  await dbContext.SaveChangesAsync();
  return Results.NoContent();
});


app.Run();
