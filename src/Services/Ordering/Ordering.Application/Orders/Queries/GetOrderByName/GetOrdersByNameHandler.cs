﻿
namespace Ordering.Application.Orders.Queries.GetOrderByName;
public class GetOrdersByNameHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking() // Read only, no need track (increase performance)
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(o => o.OrderName)
            .ToListAsync();

        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }
}
