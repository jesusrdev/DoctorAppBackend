using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository;

public class SpecialtyRepository : Repository<Specialty>, ISpecialtyRepository
{
    private readonly ApplicationDbContext _db;
    
    public SpecialtyRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Specialty specialty)
    {
        var specialtyDb = _db.Specialties.FirstOrDefault(s => s.Id == specialty.Id);

        if (specialty != null)
        {
            specialtyDb.NameSpecialty = specialty.NameSpecialty;
            specialtyDb.Description = specialty.Description;
            specialtyDb.State = specialty.State;

            _db.SaveChanges();
        }
    }
}