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
    return Results.NotFound("Usuário ou senha incorretos");
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

  var userExists = await dbContext.Usuarios.FirstOrDefaultAsync(x => x.Login == usuario.Login);

  if (userExists != null)
  {
    return Results.Conflict("Já existe um usuário com esse login. Tente outro");
  }

  dbContext.Usuarios.Add(usuario);
  await dbContext.SaveChangesAsync();
  usuario.Senha = null;
  return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
});

app.MapPut("/api/usuarios/{id}", async (int id, Usuario novoUsuario, DataContext dbContext) =>
{
  var usuarioExistente = await dbContext.Usuarios.FindAsync(id);
  if (usuarioExistente == null)
  {
    return Results.NotFound();
  }

  if (usuarioExistente.Login != novoUsuario.Login)
  {
    var userExists = await dbContext.Usuarios.FirstOrDefaultAsync(x => x.Login == novoUsuario.Login);

    if (userExists != null)
    {
      return Results.Conflict("Já existe um usuário com esse login. Tente outro");
    }
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

app.MapGet("/api/postagens", async (bool? last, bool? inactive, string? ids, DataContext dbContext) =>
{
  if (last == true)
  {
    var postagem = dbContext.Postagens.Where(x => x.Ativo == 1).OrderBy(x => x.Id).LastOrDefault();
    return Results.Ok(postagem);
  }

  if (inactive == true)
  {
    var todasPostagens = await dbContext.Postagens.ToListAsync();
    return Results.Ok(todasPostagens);
  }

  if (ids == null)
  {
    var postagens = await dbContext.Postagens.Where(x => x.Ativo == 1).ToListAsync();
    return Results.Ok(postagens);
  }

  var splittedIds = ids.Split(',').Select(int.Parse).ToList();
  var postagensPeloId = dbContext.Postagens.Where(x => splittedIds.Contains(x.Id)).ToList();

  return Results.Ok(postagensPeloId);
});

app.MapGet("/api/postagens/usuario/{idUsuario}", async (int idUsuario, bool? inactive, DataContext dbContext) =>
{
  if (inactive == true)
  {
    var postagensDoUsuario = await dbContext.Postagens
        .Where(p => p.IdUsuarioFk == idUsuario)
        .ToListAsync();

    return Results.Ok(postagensDoUsuario);
  }

  var postagensAtivasDoUsuario = await dbContext.Postagens
        .Where(p => p.IdUsuarioFk == idUsuario && p.Ativo == 1)
        .ToListAsync();

  return Results.Ok(postagensAtivasDoUsuario);
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
  var comentarios = await dbContext.Comentarios.Where(c => c.IdPostagemFk == postagem.Id).ToListAsync();
  var curtidas = await dbContext.Curtidas.Where(c => c.IdPostagemFk == postagem.Id).ToListAsync();

  dbContext.Postagens.Remove(postagem);

  foreach (var comentario in comentarios)
  {
    dbContext.Comentarios.Remove(comentario);
  }

  foreach (var curtida in curtidas)
  {
    dbContext.Curtidas.Remove(curtida);
  }

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

app.MapGet("/api/comentarios", async (DataContext dbContext) =>
{
  var comentariosDoUsuario = await dbContext.Comentarios.ToListAsync();

  return Results.Ok(comentariosDoUsuario);
});

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