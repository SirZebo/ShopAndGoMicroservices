using Feedback.API.Model;
using Feedback.API.Reviews.GetReviewsByCustomerId;

namespace Feedback.API.Reviews.GetReviewByCustomer;

public record GetReviewsByCustomerIdRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetReviewsByCustomerIdResponse(List<Review> Reviews);

public class GetReviewsByCustomerIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reviews/customer/{id}", async ([AsParameters] GetReviewsByCustomerIdRequest request, Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetReviewsByCustomerIdQuery(id, request.PageNumber, request.PageSize));

            var response = result.Adapt<GetReviewsByCustomerIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetReviewsByCustomerId")
        .Produces<GetReviewsByCustomerIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Reviews By CustomerId")
        .WithDescription("Get Reviews By CustomerId");

    }
}