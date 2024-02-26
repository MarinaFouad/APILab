using System.Text.Json.Serialization;

namespace Lab1.Model
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public int? DepartmentId { get; set; }
        //[JsonIgnore]
        //public virtual Department Department { get; set; }
    }
}
