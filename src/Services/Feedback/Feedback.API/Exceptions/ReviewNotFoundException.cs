using BuildingBlocks.Exceptions;

namespace Feedback.API.Exceptions;

public class ReviewNotFoundException : NotFoundException
{
    public ReviewNotFoundException(Guid Id) : base("Review", Id)
    {
    }
}

