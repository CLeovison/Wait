namespace Wait.Repositories;


public interface IUserRepositories
{
    Task<bool> CreateUser();
}