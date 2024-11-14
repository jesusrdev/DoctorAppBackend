namespace Data.Interfaces.IRepository;

public interface IUnitOfWork : IDisposable
{
    ISpecialtyRepository Specialty { get; }

    Task Save();
}