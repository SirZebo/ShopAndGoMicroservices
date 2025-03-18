using Feedback.API.Exceptions;
using Feedback.API.Model;

namespace Feedback.API.Reviews.GetReviewById;

public record GetReviewByIdQuery(Guid Id) : IQuery<GetReviewByIdResult>;

public record GetReviewByIdResult(Review Review);
internal class GetReviewByIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetReviewByIdQuery, GetReviewByIdResult>
{
    public async Task<GetReviewByIdResult> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken)
    {
        var review = await session.LoadAsync<Review>(query.Id, cancellationToken);

        if (review is null)
        {
            throw new ReviewNotFoundException(query.Id);
        }

        return new GetReviewByIdResult(review);
    }
}
