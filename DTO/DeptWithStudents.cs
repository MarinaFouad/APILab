using Lab1.Model;

namespace Lab1.DTO
{
    public class DeptWithStudents
    {
        public int Department_Number { get; set; }
        public string Department_Name { get; set; }
        public string Manager_Name { get; set; }
        public string Location { get; set; }
        public List<string> Student_Name { get; set; }= new List<string>();

    }
}
