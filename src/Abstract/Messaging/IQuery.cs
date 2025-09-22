namespace Wait.Abstract.Messaging
{
    /// <summary>
    /// Defines a contract for a query that retrieves data without modifying state.
    /// </summary>
    /// <typeparam name="TResponse">The type of the data returned by the query.</typeparam>
    /// <remarks>
    /// <para>
    /// In CQRS (Command Query Responsibility Segregation), queries are responsible for reading data,
    /// while commands handle state changes. A query should never alter application or domain state.
    /// </para>
    /// 
    /// <para>
    /// Use <see cref="IQuery{TResponse}"/> when you need to encapsulate the intent of retrieving
    /// specific data. This abstraction enables a clean separation between:
    /// <list type="bullet">
    ///   <item><description>What data is requested (the query)</description></item>
    ///   <item><description>How the data is retrieved (the query handler)</description></item>
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// This separation promotes:
    /// <list type="bullet">
    ///   <item><description>Testability — queries can be mocked or stubbed in isolation</description></item>
    ///   <item><description>Maintainability — read and write concerns evolve independently</description></item>
    ///   <item><description>Scalability — queries can be optimized or routed differently from commands</description></item>
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// Example:
    /// <code>
    /// // Define the query
    /// public sealed record GetUserByIdQuery(Guid UserId) 
    ///     : IQuery&lt;UserDto&gt;;
    /// 
    /// // Define the handler
    /// public sealed class GetUserByIdHandler 
    ///     : IQueryHandler&lt;GetUserByIdQuery, UserDto&gt;
    /// {
    ///     private readonly IUserRepository _repository;
    ///     
    ///     public GetUserByIdHandler(IUserRepository repository)
    ///     {
    ///         _repository = repository;
    ///     }
    ///     
    ///     public async Task&lt;UserDto&gt; Handle(GetUserByIdQuery query, CancellationToken ct)
    ///     {
    ///         var user = await _repository.FindByIdAsync(query.UserId, ct);
    ///         return new UserDto(user.Id, user.Username, user.Email);
    ///     }
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public interface IQuery<TResponse> { }
}
