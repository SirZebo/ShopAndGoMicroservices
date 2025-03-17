using BuildingBlocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions;
public class OrderStatusNotFoundException : NotFoundException
{
    public OrderStatusNotFoundException(Guid id) : base("Order", id)
    {
    }
}