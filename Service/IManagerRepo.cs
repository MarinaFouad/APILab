using Lab1.Model;

namespace Lab1.Service
{
    public interface IManagerRepo
    {
        List<Manager> GetAll();
        Manager GetById(int id);
        Manager GetByName(string name);
        void Delete(int? id);
        void Add(Manager Manager);
        void Update(Manager Manager);
    }
}
