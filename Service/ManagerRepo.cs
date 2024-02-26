using Lab1.Entity;
using Lab1.Model;

namespace Lab1.Service
{
    public class ManagerRepo : IManagerRepo
    {
        UnivDbContext db;
        public ManagerRepo(UnivDbContext _db)
        {
            db = _db;
        }

        public List<Manager> GetAll()
        {
            return db.Managers.ToList();
        }

        public Manager GetById(int id)
        {
            return db.Managers.FirstOrDefault(i => i.Id == id);
        }

        public Manager GetByName(string name)
        {
            return db.Managers.FirstOrDefault(i => i.Name == name);
        }
        public void Add(Manager manager)
        {
            db.Managers.Add(manager);
            db.SaveChanges();
        }

        public void Delete(int? id)
        {
            var Mng = db.Managers.FirstOrDefault(i => i.Id == id);
            db.Managers.Remove(Mng);
            db.SaveChanges();
        }


        public void Update(Manager manager)
        {
            db.Managers.Update(manager);
            db.SaveChanges();
        }
    }
}
