using Feedback.API.Exceptions;
using Feedback.API.Model;

namespace Feedback.API.Reviews.GetReviewsByCustomerId;

public record GetReviewsByCustomerIdQuery(Guid CustomerId, int? PageNumber = 1, int? PageSize = 10) : IQuery<GetReviewsByCustomerIdResult>;

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
                .OrderByDescending(x => x.LastModified)
                .ToListAsync();

        if (reviews.IsEmpty())
        {
            throw new CustomerNotFoundException(query.CustomerId);
        }

        return new GetReviewsByCustomerIdResult(reviews);
    }
}

