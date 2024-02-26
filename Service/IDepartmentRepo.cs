using Lab1.Model;

namespace Lab1.Service
{
    public interface IDepartmentRepo
    {
        List<Department> GetAll();
        Department GetById(int id);
        Department GetByName(string name);
        void Delete(int? id);
        void Add(Department department);
        void Update(Department department);
    }
}
