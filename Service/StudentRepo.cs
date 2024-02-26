using Lab1.Entity;
using Lab1.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Service
{
    public class StudentRepo : IStudentRepo
    {
        UnivDbContext db;

        public StudentRepo(UnivDbContext _db)
        {
            db = _db;
        }

        public List<Student> GetAll()
        {
            return db.Students.Include(d=>d.Department).ToList();
        }
        public Student GetById(int id)
        {
            return db.Students.Include(d=>d.Department).FirstOrDefault(d => d.Id == id);
        }
        public Student GetByName(string name)
        {
            return db.Students.Include(d => d.Department).FirstOrDefault(s => s.Name == name);
        }
        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }
        public void Update(Student student)
        {
            db.Students.Update(student);
            db.SaveChanges();
        }
        public void Delete(int? id)
        {
            var student = db.Students.Include(d => d.Department).FirstOrDefault(i=>i.Id == id);
            db.Students.Remove(student);
            db.SaveChanges();
        }
    }
}
