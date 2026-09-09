using AutoMapper;
using ToDo.API.DTOs;
using ToDo.API.Models;
using ToDo.API.Repositories;

namespace ToDo.API.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categories");

        group.MapGet("", async (IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                var categories = await unitOfWork.Repository<Category>().GetAllAsync(cancellationToken);
                return Results.Ok(mapper.Map<IEnumerable<CategoryDTO>>(categories));
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving categories.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapGet("/{id:int}", async (int id, IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                var category = await unitOfWork.Repository<Category>().GetByIdAsync(id, cancellationToken);

                if (category is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(mapper.Map<CategoryDTO>(category));
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving the category.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapPost("", async (CategoryDTO dto, IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                if (dto is null || string.IsNullOrWhiteSpace(dto.Name) || !dto.UserId.HasValue)
                {
                    return Results.BadRequest();
                }

                var category = mapper.Map<Category>(dto);
                category.ColorHex = dto.ColorHex ?? "#6c757d";

                await unitOfWork.Repository<Category>().AddAsync(category, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);

                return Results.Created($"/api/categories/{category.Id}", mapper.Map<CategoryDTO>(category));
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while creating the category.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapPut("/{id:int}", async (int id, CategoryDTO dto, IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                var category = await unitOfWork.Repository<Category>().GetByIdAsync(id, cancellationToken);

                if (category is null)
                {
                    return Results.NotFound();
                }

                mapper.Map(dto, category);

                unitOfWork.Repository<Category>().Update(category);
                await unitOfWork.CompleteAsync(cancellationToken);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while updating the category.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapDelete("/{id:int}", async (int id, IUnitOfWork unitOfWork, CancellationToken cancellationToken) =>
        {
            try
            {
                var category = await unitOfWork.Repository<Category>().GetByIdAsync(id, cancellationToken);

                if (category is null)
                {
                    return Results.NotFound();
                }

                unitOfWork.Repository<Category>().Remove(category);
                await unitOfWork.CompleteAsync(cancellationToken);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while deleting the category.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });
    }
}
