using Feedback.API.Enums;

namespace Feedback.API.Model;

public class Review
{
    public Guid Id { get; set; }
    public Order Order { get; set; }
    public FeedbackStatus FeedbackStatus { get; set; }
    public string Body { get; set; } = default!;
}
