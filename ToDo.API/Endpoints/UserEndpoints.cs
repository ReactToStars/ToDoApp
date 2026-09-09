using AutoMapper;
using ToDo.API.DTOs;
using ToDo.API.Models;
using ToDo.API.Repositories;

namespace ToDo.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users");

        group.MapGet("", async (IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                var users = await unitOfWork.Repository<User>().GetAllAsync(cancellationToken);
                return Results.Ok(mapper.Map<IEnumerable<UserDTO>>(users));
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving users.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapGet("/{id:guid}", async (Guid id, IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                var user = await unitOfWork.Repository<User>().GetByIdAsync(id, cancellationToken);

                if (user is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(mapper.Map<UserDTO>(user));
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving the user.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapPost("", async (UserDTO dto, IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                if (dto is null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.PasswordHash))
                {
                    return Results.BadRequest();
                }

                var user = mapper.Map<User>(dto);
                user.Id = Guid.NewGuid();
                user.CreatedDate = dto.CreatedDate ?? DateTime.UtcNow;

                await unitOfWork.Repository<User>().AddAsync(user, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);

                return Results.Created($"/api/users/{user.Id}", mapper.Map<UserDTO>(user));
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while creating the user.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapPut("/{id:guid}", async (Guid id, UserDTO dto, IUnitOfWork unitOfWork, IMapper mapper, CancellationToken cancellationToken) =>
        {
            try
            {
                var user = await unitOfWork.Repository<User>().GetByIdAsync(id, cancellationToken);

                if (user is null)
                {
                    return Results.NotFound();
                }

                mapper.Map(dto, user);
                user.UpdatedDate = DateTime.UtcNow;

                unitOfWork.Repository<User>().Update(user);
                await unitOfWork.CompleteAsync(cancellationToken);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while updating the user.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, IUnitOfWork unitOfWork, CancellationToken cancellationToken) =>
        {
            try
            {
                var user = await unitOfWork.Repository<User>().GetByIdAsync(id, cancellationToken);

                if (user is null)
                {
                    return Results.NotFound();
                }

                unitOfWork.Repository<User>().Remove(user);
                await unitOfWork.CompleteAsync(cancellationToken);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while deleting the user.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        });
    }
}
