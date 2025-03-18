namespace Feedback.API.Reviews.CreateReview;

public record CreateReviewCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price, TimeSpan MaxCompletionTime, string Language)
    : ICommand<CreateReviewResult>;

public record CreateReviewResult(Guid Id);

public class CreateReviewHandler
{
}

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price, TimeSpan MaxCompletionTime, string Language)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
    }

}
//internal class CreateProductCommandHandler
//    (IDocumentSession session)
//    : ICommandHandler<CreateProductCommand, CreateProductResult>
//{
//    // Business logic to create a product
//    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
//    {
//        var product = new Product
//        {
//            Name = command.Name,
//            Category = command.Category,
//            Description = command.Description,
//            ImageFile = command.ImageFile,
//            Price = command.Price,
//            MaxCompletionTime = command.MaxCompletionTime,
//            Language = command.Language,
//        };

//        // save to database
//        session.Store(product);
//        await session.SaveChangesAsync(cancellationToken);

//        // return result
//        return new CreateProductResult(product.Id);
//    }
//}