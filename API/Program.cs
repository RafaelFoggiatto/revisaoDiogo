using API.models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Projeto de revisão todo");

//cadastrar
app.MapPost("/api/tarefas/cadastrar", ([FromBody] Tarefa tarefa, [FromServices] AppDataContext ctx) => 

{
    ctx.Tarefas.Add(tarefa);
    ctx.SaveChanges();
    return Results.Created("", tarefa);
});

//listar
app.MapGet("/api/tarefas/listar", ([FromServices] AppDataContext ctx) => 

{
   if(ctx.Tarefas.Any()){
        return Results.Ok(ctx.Tarefas.ToList());
   }
   return Results.NotFound();
});

//buscar
app.MapGet("/api/tarefas/buscar/{id}", ([FromRoute] int id, 
[FromServices] AppDataContext ctx) => 

{   
    Tarefa? tarefa = ctx.Tarefas.Find(id);
    if(tarefa is null){
        return Results.NotFound();
    }
    return Results.Ok(tarefa);
});

//remover
app.MapDelete("/api/tarefas/deletar/{id}", ([FromRoute] int id, 
[FromServices] AppDataContext ctx) => 

{   
    Tarefa? tarefa = ctx.Tarefas.Find(id);
    if(tarefa is null){
        return Results.NotFound();
    }
    ctx.Tarefas.Remove(tarefa);
    ctx.SaveChanges();
    return Results.Ok(tarefa);
});

//remover
app.MapPut("/api/tarefas/alterar/{id}", ([FromRoute] int id, 
[FromBody] Tarefa tarefaAlterada, 
[FromServices] AppDataContext ctx) => 

{   
    Tarefa? tarefa = ctx.Tarefas.Find(id);
    if(tarefa is null){
        return Results.NotFound();
    }
    tarefa.Nome = tarefaAlterada.Nome;
    ctx.Tarefas.Update(tarefa);
    ctx.SaveChanges();
    return Results.Ok(tarefa);
});


app.Run();
