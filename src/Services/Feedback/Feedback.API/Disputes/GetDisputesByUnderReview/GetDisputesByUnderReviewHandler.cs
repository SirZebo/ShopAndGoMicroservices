using Feedback.API.Model;
using Marten.Pagination;

namespace Feedback.API.Disputes.GetDisputesByUnderReview;

public class GetDisputesByUnderReviewHandler
{
}

public record GetDisputesByUnderReviewQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetDisputesByUnderReviewResult>;

public record GetDisputesByUnderReviewResult(IEnumerable<Review> Reviews);

internal class GetDisputesByUnderReviewQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetDisputesByUnderReviewQuery, GetDisputesByUnderReviewResult>
{
    public async Task<GetDisputesByUnderReviewResult> Handle(GetDisputesByUnderReviewQuery query, CancellationToken cancellationToken)
    {
        var reviews = await session.Query<Review>()
            .Where(x => x.DisputeStatus == Enums.DisputeStatus.UnderReview)
            .OrderByDescending(x => x.LastModified)
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetDisputesByUnderReviewResult(reviews);
    }
}
