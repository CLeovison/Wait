using Wait.Abstract.Results;

namespace Wait.Abstract.Messaging;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
        Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct);
}