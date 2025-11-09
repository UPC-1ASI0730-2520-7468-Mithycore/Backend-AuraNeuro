namespace Backend_AuraNeuro.API.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}