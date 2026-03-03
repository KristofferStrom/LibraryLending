using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Core.Abstractions.Messaging;

public interface IQuery<TResponse> where TResponse : Result { }
