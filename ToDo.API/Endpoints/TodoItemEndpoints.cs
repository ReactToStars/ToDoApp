using AutoMapper;
using ToDo.API.DTOs;
using ToDo.API.Models;
using ToDo.API.Repositories;

namespace ToDo.API.Endpoints;

public static class TodoItemEndpoints
{
    public static void MapTodoItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/todoitems");

        group.MapGet("", async (IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                var items = await unitOfWork.Repository<TodoItem>().GetAllAsync(cancellationToken);
                return Results.Ok(mapper.Map<IEnumerable<TodoItemDTO>>(items));
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving todo items.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapGet("/{id:int}", async (int id, IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                var item = await unitOfWork.Repository<TodoItem>().GetByIdAsync(id, cancellationToken);

                if (item is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(mapper.Map<TodoItemDTO>(item));
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving the todo item.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapPost("", async (TodoItemDTO dto, IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                if (dto is null || string.IsNullOrWhiteSpace(dto.Title) || !dto.UserId.HasValue)
                {
                    return Results.BadRequest();
                }

                var item = mapper.Map<TodoItem>(dto);
                item.CreatedDate = dto.CreatedDate ?? DateTime.UtcNow;
                item.IsCompleted = dto.IsCompleted ?? false;
                item.Priority = dto.Priority ?? Priority.Medium;
                item.CategoryId = dto.CategoryId;

                await unitOfWork.Repository<TodoItem>().AddAsync(item, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);

                return Results.Created($"/api/todoitems/{item.Id}", mapper.Map<TodoItemDTO>(item));
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while creating the todo item.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapPut("/{id:int}", async (int id, TodoItemDTO dto, IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                var item = await unitOfWork.Repository<TodoItem>().GetByIdAsync(id, cancellationToken);

                if (item is null)
                {
                    return Results.NotFound();
                }

                mapper.Map(dto, item);
                item.UpdatedDate = DateTime.UtcNow;

                unitOfWork.Repository<TodoItem>().Update(item);
                await unitOfWork.CompleteAsync(cancellationToken);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while updating the todo item.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapDelete("/{id:int}", async (int id, IUnitOfWork unitOfWork, CancellationToken cancellationToken) =>
        {
            try
            {
                var item = await unitOfWork.Repository<TodoItem>().GetByIdAsync(id, cancellationToken);

                if (item is null)
                {
                    return Results.NotFound();
                }

                unitOfWork.Repository<TodoItem>().Remove(item);
                await unitOfWork.CompleteAsync(cancellationToken);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while deleting the todo item.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });
    }
}
