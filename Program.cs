using Microsoft.EntityFrameworkCore;
using ProjetoInterdisciplinarII.Models;
using ProjetoInterdisciplinarII.Models.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>();
builder.Services.AddCors(options =>
{
  options.AddPolicy("MyAllowedOrigins",
      policy =>
      {
        policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
      });
});

var app = builder.Build();
app.UseCors("MyAllowedOrigins");

app.MapGet("/api/usuarios/signin", async (string login, string senha, DataContext dbContext) =>
{
  var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Login == login && u.Senha == senha);
  if (usuario == null)
  {
    return Results.NotFound();
  }

  usuario.Senha = null;
  return Results.Ok(usuario);
});

app.MapGet("/api/usuarios", async (string? ids, DataContext dbContext) =>
{
  if (ids == null)
  {
    var usuarios = await dbContext.Usuarios.ToListAsync();

    foreach (var usuario in usuarios)
    {
      usuario.Senha = null;
    }

    return Results.Ok(usuarios);
  }

  var splittedIds = ids.Split(',').Select(int.Parse).ToList();
  var usuariosPeloId = dbContext.Usuarios.Where(x => splittedIds.Contains(x.Id)).ToList();

  foreach (var usuario in usuariosPeloId)
  {
    usuario.Senha = null;
  }

  return Results.Ok(usuariosPeloId);
});

app.MapPost("/api/usuarios", async (Usuario usuario, DataContext dbContext) =>
{
  dbContext.Usuarios.Add(usuario);
  await dbContext.SaveChangesAsync();
  return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
});

app.MapPut("/api/usuarios/{id}", async (int id, Usuario novoUsuario, DataContext dbContext) =>
{
  var usuarioExistente = await dbContext.Usuarios.FindAsync(id);
  if (usuarioExistente == null)
  {
    return Results.NotFound();
  }

  foreach (var prop in novoUsuario.GetType().GetProperties())
  {
    if (prop.GetValue(novoUsuario) != null && !prop.ToString().Contains("Id"))
      prop.SetValue(usuarioExistente, prop.GetValue(novoUsuario));
  }

  dbContext.Usuarios.Update(usuarioExistente);
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

// 
// 
// 

app.MapGet("/api/postagens", async (DataContext dbContext) =>
{
  var postagens = await dbContext.Postagens.ToListAsync();
  return Results.Ok(postagens);
});

app.MapGet("/api/postagens/{id}", async (int id, DataContext dbContext) =>
{
  var postagem = await dbContext.Postagens.FindAsync(id);

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

app.MapPost("/api/postagens", async (Postagem postagem, DataContext dbContext) =>
{
  dbContext.Postagens.Add(postagem);
  await dbContext.SaveChangesAsync();
  return Results.Created($"/api/postagens/{postagem.Id}", postagem);
});

app.MapPut("/api/postagens/{id}", async (int id, Postagem novaPostagem, DataContext dbContext) =>
{
  var postagemExistente = await dbContext.Postagens.FindAsync(id);
  if (postagemExistente == null)
  {
    return Results.NotFound();
  }

  foreach (var prop in novaPostagem.GetType().GetProperties())
  {
    if (prop.GetValue(novaPostagem) != null && !prop.ToString().Contains("Id"))
      prop.SetValue(postagemExistente, prop.GetValue(novaPostagem));
  }

  dbContext.Postagens.Update(postagemExistente);
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

// 
// 
// 

app.MapGet("/api/curtidas/usuario/{idUsuario}", async (int idUsuario, DataContext dbContext) =>
{
  var curtidasDoUsuario = await dbContext.Curtidas
      .Where(c => c.IdUsuarioFk == idUsuario)
      .ToListAsync();

  return Results.Ok(curtidasDoUsuario);
});

app.MapGet("/api/curtidas/postagem/{idPostagem}", async (int idPostagem, DataContext dbContext) =>
{
  var curtidasDaPostagem = await dbContext.Curtidas
      .Where(c => c.IdPostagemFk == idPostagem)
      .ToListAsync();

  return Results.Ok(curtidasDaPostagem);
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

// 
// 
// 

app.MapGet("/api/comentarios/usuario/{idUsuario}", async (int idUsuario, DataContext dbContext) =>
{
  var comentariosDoUsuario = await dbContext.Comentarios
      .Where(c => c.IdUsuarioFk == idUsuario)
      .ToListAsync();

  return Results.Ok(comentariosDoUsuario);
});

app.MapGet("/api/comentarios/postagem/{idPostagem}", async (int idPostagem, DataContext dbContext) =>
{
  var comentariosDaPostagem = await dbContext.Comentarios
      .Where(c => c.IdPostagemFk == idPostagem)
      .ToListAsync();

  return Results.Ok(comentariosDaPostagem);
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