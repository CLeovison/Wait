using Wait.Abstract.Results;

namespace Wait.Abstract.Messaging;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken ct);
}