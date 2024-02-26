
using System.Text.Json.Serialization;

namespace Lab1.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Image {  get; set; }
        public int? DepartmentId { get; set; }
       // [JsonIgnore]
        public virtual Department? Department { get; set; }
    }
}
