using Data.Interfaces.IRepository;

namespace Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    
    public ISpecialtyRepository Specialty { get; private set; }
    public IDoctorRepository Doctor { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Specialty = new SpecialtyRepository(db);
        Doctor = new DoctorRepository(db);
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
}