using FluentValidation;
using Library.Contracts.Book;

namespace LibraryHub.API.Validator
{
    public class CreateBookValidator : AbstractValidator<CreateBookModel>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required");
            RuleFor(x => x.Genre).NotEmpty().WithMessage("Genre is required");
            RuleFor(x => x.Year).InclusiveBetween(1000, DateTime.Now.Year)
                .WithMessage("Year must be a valid publication year");
            RuleFor(x => x.CopiesAvailable).GreaterThanOrEqualTo(0)
                .WithMessage("CopiesAvailable must be 0 or more");
        }
    }
}
