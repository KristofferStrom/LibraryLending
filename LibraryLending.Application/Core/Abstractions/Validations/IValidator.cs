using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Core.Abstractions.Validations;

public interface IValidator<in TRequest>
{
    Error? Validate(TRequest request);
}
