using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository;

public class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    private readonly ApplicationDbContext _db;
    
    public DoctorRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Doctor doctor)
    {
        var doctorDb = _db.Doctors.FirstOrDefault(d => d.Id == doctor.Id);

        if (doctorDb != null)
        {
            doctorDb.Lastname = doctor.Lastname;
            doctorDb.Firstname = doctor.Firstname;
            doctorDb.Direction = doctor.Direction;
            doctorDb.Phone = doctor.Phone;
            doctorDb.State = doctor.State;
            doctorDb.Genre = doctor.Genre;
            doctorDb.SpecialtyId = doctor.SpecialtyId;
            doctorDb.UpdateDate = DateTime.Now;

            _db.SaveChanges();
        }
    }
}