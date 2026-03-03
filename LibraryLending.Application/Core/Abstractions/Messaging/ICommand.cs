using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Core.Abstractions.Messaging;

public interface ICommand<TResponse> where TResponse : Result { }
