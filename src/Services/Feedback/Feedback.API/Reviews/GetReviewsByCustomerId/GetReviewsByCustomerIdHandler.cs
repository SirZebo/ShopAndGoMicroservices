using Feedback.API.Model;

namespace Feedback.API.Reviews.GetReviewsByCustomerId;

public record GetReviewsByCustomerIdQuery(Guid CustomerId) : IQuery<GetReviewsByCustomerIdResult>;

public record GetReviewsByCustomerIdResult(IEnumerable<Review> Reviews);
public class GetReviewsByCustomerIdHandler
{
}
internal class GetReviewsByCustomerIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetReviewsByCustomerIdQuery, GetReviewsByCustomerIdResult>
{
    public async Task<GetReviewsByCustomerIdResult> Handle(GetReviewsByCustomerIdQuery query, CancellationToken cancellationToken)
    {
        var reviews = await session.Query<Review>()
                .Where(x => x.Order.CustomerId == query.CustomerId)
                .ToListAsync();

        if (reviews.IsEmpty())
        {
            //throw new ProductNotFoundException(query.Id);
        }

        return new GetReviewsByCustomerIdResult(reviews);
    }
}

