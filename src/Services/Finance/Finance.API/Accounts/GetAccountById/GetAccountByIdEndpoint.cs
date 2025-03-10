using Finance.API.Accounts.GetAccountById;

namespace Finance.API.Accounts.GetAccount;

// public record GetAccountByIdRequest();
public record GetAccountByIdResponse(Account Account);

public class GetAccountByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetAccountByIdQuery(id));

            var response = result.Adapt<GetAccountByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetAccountById")
        .Produces<GetAccountByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Account By Id")
        .WithDescription("Get Account By Id");

    }
}