namespace Wait.Abstract.Messaging;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<Results> Handle(TCommand command, CancellationToken ct);
}