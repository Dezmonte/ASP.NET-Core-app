using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using WebApplication2.Models;
using WebApplication2.Services;
namespace WebApplication2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=todo.db"));
            builder.Services.AddOpenApi();
            var app = builder.Build();
          
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "v1");
                });
            }

            app.MapPost("/todos", async (CreateTodoDto? dto, ITodoService service) =>
            {
                if (dto == null)
                    return Results.BadRequest("Название не может быть пустым");
                var newItem = await service.CreateAsync(dto);
                if (newItem == null)
                    return Results.BadRequest("Название не может быть пустым");
                return Results.Created($"/todos/{newItem.Id}", newItem);
            })
            .WithSummary("Вводишь данные, он сохраняет в БД");

            app.MapGet("/todos", async (ITodoService service) =>
            {
                var todos = await service.GetAllAsync();
                return Results.Ok(todos);
            });

            app.MapGet("/todos/{id:int}", async (ITodoService service, int id) =>
            {
                var item = await service.GetByIdAsync(id);
                if (item == null)
                    return Results.NotFound("Такой задачи нет");
                return Results.Ok(item);
            });

            app.MapPut("/todos/{id:int}", async (int id,[FromBody] UpdateTodoDto dto, ITodoService service) =>
            {
                var existItem = await service.UpdateAsync(dto, id);
                if (existItem == true)
                    return Results.Ok("Запись сохранена");
                else if (existItem == false)
                    return Results.NotFound("Задача не найдена");
                else
                    return Results.BadRequest("Название не может быть пустым");                
            });

            app.MapDelete("/todos/{id:int}", async (int id, ITodoService service) =>
            {
                var idDeleted = await service.DeleteAsync(id);
                if (idDeleted)
                    return Results.NoContent();
                else
                    return Results.NotFound("Задача не обнаружена");
            });
            app.Run();
            /*
            Person a = new("Данька", "ASP.NET Core");                    

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/admin")
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "text/plain; charset=utf-8";
                    await context.Response.WriteAsync("Тебя сюда нельзя!");
                }
                else
                    await next(context);
            });

            app.Use(async (context, next) =>
            {
                context.Response.ContentType = "text/plain; charset=utf-8";
                Console.WriteLine($"{context.Request.Method} {context.Request.Path}");
                await next(context);
            });

            app.Use(async (context, next) =>
            {
                context.Response.ContentType = "text/plain; charset=utf-8";
                Console.WriteLine("--- Проверка второго логгера: Начало ---");
                await next(context);
                Console.WriteLine("--- Проверка второго логгера: Конец ---");
            });


            app.MapGet("/about", () => a);

            app.MapGet("/greet/{name}", (string name) =>
            {
                return $"Привет, {name}!";
            });

            app.MapGet("/calculate", (int a , int b) =>
            {
                return $"Сумма: = {a+b}";
            });
            app.MapFallback(async (context) =>
            {
                context.Response.ContentType = "text/plain; charset=utf-8";
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Такой страницы не существует!");
            });
            app.Run();
            */
        }
    }
}
