using Feedback.API.Model;
using Marten.Pagination;

namespace Feedback.API.Disputes.GetDisputesByAccepted;

public record GetDisputesByAcceptedQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetDisputesByAcceptedResult>;

public record GetDisputesByAcceptedResult(IEnumerable<Review> Reviews);

internal class GetDisputesByAcceptedQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetDisputesByAcceptedQuery, GetDisputesByAcceptedResult>
{
    public async Task<GetDisputesByAcceptedResult> Handle(GetDisputesByAcceptedQuery query, CancellationToken cancellationToken)
    {
        var reviews = await session.Query<Review>()
            .Where(x => x.DisputeStatus == Enums.DisputeStatus.Accepted)
            .OrderByDescending(x => x.LastModified)
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetDisputesByAcceptedResult(reviews);
    }
}

