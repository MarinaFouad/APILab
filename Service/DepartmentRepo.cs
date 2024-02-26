using Lab1.Entity;
using Lab1.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Service
{
    public class DepartmentRepo : IDepartmentRepo
    {
        UnivDbContext db;
        public DepartmentRepo(UnivDbContext _db) 
        {
            db = _db;
        }

        public List<Department> GetAll()
        {
            return db.Departments.Include(s=>s.Students).ToList();
        }

        public Department GetById(int id)
        {
           return db.Departments.Include(s => s.Students).FirstOrDefault(i=>i.Id==id);
        }

        public Department GetByName(string name)
        {
            return db.Departments.Include(s => s.Students).FirstOrDefault(i=>i.Name==name);
        }
        public void Add(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var Dep = db.Departments.Include(s => s.Students).FirstOrDefault(i => i.Id == id);
            db.Departments.Remove(Dep);
            db.SaveChanges();
        }


        public void Update(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            var existingDepartment = db.Departments.Find(department.Id);
            if (existingDepartment != null)
            {
                db.Entry(existingDepartment).State = EntityState.Detached; // Detach existing entity
                db.Departments.Update(department); // Attach the updated entity
                db.SaveChanges();
            }
        }
    }
}
