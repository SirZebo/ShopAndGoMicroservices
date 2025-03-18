namespace Feedback.API.Exceptions;

public class ReviewAlreadySubmittedException : Exception
{
    public ReviewAlreadySubmittedException(Guid Id) : base($"Review {Id} has already been submitted")
    {
    }
}
