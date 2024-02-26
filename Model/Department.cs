using Lab1.CustomValidation;

namespace Lab1.Model
{
    public class Department
    {
        public int Id { get; set; }
        [UniqueDepartmentName]
        public string Name { get; set; }
       
        public string Location { get; set; }
        
        public string MangerName { get; set; }

       
        public virtual ICollection<Student> Students { get; set; }
            = new HashSet<Student>();


    }
}
