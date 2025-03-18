using Feedback.API.Model;
using Marten.Pagination;

namespace Feedback.API.Disputes.GetDisputesByRejected;

public record GetDisputesByRejectedQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetDisputesByRejectedResult>;

public record GetDisputesByRejectedResult(IEnumerable<Review> Reviews);
internal class GetDisputesByRejectedQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetDisputesByRejectedQuery, GetDisputesByRejectedResult>
{
    public async Task<GetDisputesByRejectedResult> Handle(GetDisputesByRejectedQuery query, CancellationToken cancellationToken)
    {
        var reviews = await session.Query<Review>()
            .Where(x => x.DisputeStatus == Enums.DisputeStatus.Rejected)
            .OrderByDescending(x => x.LastModified)
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetDisputesByRejectedResult(reviews);
    }
}