namespace Wait.Abstract.Messaging;

/// <summary>
/// Represents the base contract for all command messages in a CQRS (Command Query Responsibility Segregation) architecture.
/// </summary>
/// <remarks>
/// <para>
/// In CQRS, commands are requests to perform state-changing operations. They do not return data beyond an acknowledgment
/// of success or failure. The <see cref="IBaseCommand"/> interface serves as a marker for all command types, ensuring
/// a clear separation from queries.
/// </para>
/// <para>
/// This interface is intentionally left without members to provide flexibility. It acts as a common type constraint,
/// allowing command handlers, middleware, and pipelines to consistently recognize and process commands.
/// </para>
/// </remarks>
public interface IBaseCommand { }

/// <summary>
/// Defines a contract for a command that does not produce a response.
/// </summary>
/// <remarks>
/// <para>
/// Use <see cref="ICommand"/> when the outcome of a command is either implicit (e.g., an update, insert, or delete)
/// or when the caller does not require additional data to be returned.  
/// </para>
/// <para>
/// This pattern is common for operations such as:
/// <list type="bullet">
///   <item><description>Creating or updating a database record</description></item>
///   <item><description>Publishing a domain event</description></item>
///   <item><description>Triggering side effects without returning results</description></item>
/// </list>
/// </para>
/// <para>
/// Example:
/// <code>
/// public sealed record CreateUserCommand(string Username, string Email) : ICommand;
/// </code>
/// </para>
/// </remarks>
public interface ICommand : IBaseCommand { }

/// <summary>
/// Defines a contract for a command that produces a typed response.
/// </summary>
/// <typeparam name="TResponse">The type of the response returned after executing the command.</typeparam>
/// <remarks>
/// <para>
/// Use <see cref="ICommand{TResponse}"/> when the execution of a command produces a meaningful result.
/// This enables commands to not only request a state change but also provide feedback to the caller.  
/// </para>
/// <para>
/// This pattern is useful for operations such as:
/// <list type="bullet">
///   <item><description>Creating an entity and returning its generated identifier</description></item>
///   <item><description>Executing a business workflow that returns a status object</description></item>
///   <item><description>Processing input and returning a computed result</description></item>
/// </list>
/// </para>
/// <para>
/// Example:
/// <code>
/// public sealed record CreateOrderCommand(Guid UserId, IReadOnlyList&lt;OrderItem&gt; Items) 
///     : ICommand&lt;Guid&gt;; // Returns the OrderId
/// </code>
/// </para>
/// </remarks>
public interface ICommand<TResponse> : IBaseCommand { }
