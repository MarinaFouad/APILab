using Lab1.DTO;
using Lab1.Model;
using Lab1.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentRepo StudentRepo;

        public StudentController(IStudentRepo studentRepo)
        {
            StudentRepo = studentRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Students = StudentRepo.GetAll();
            if (Students is null)
            {
                return NotFound();
            }
            var studentDTOs = Students.Select(s => new studentWithDept
            {
                student_Number = s.Id,
                student_Name = s.Name,
                student_Email = s.Email,
                student_Phone = s.Phone,
                student_Image = s.Image,
                department_Name = s.Department != null ? s.Department.Name : string.Empty
            });

            return Ok(studentDTOs);
        }
        [HttpGet("{id:int}")]
        //[Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var Student = StudentRepo.GetById(id);
            if (Student is null)
            {
                return NotFound(new { msg = $"Student with {id} Not Found" });
            }
            studentWithDept studentWithDept = new studentWithDept();
            studentWithDept.student_Number = Student.Id;
            studentWithDept.student_Name = Student.Name;
            studentWithDept.student_Phone = Student.Phone;
            studentWithDept.student_Email = Student.Email;
            studentWithDept.student_Image = Student.Image;
            studentWithDept.department_Name = Student.Department.Name;
            return Ok(new { msg = $"Student with {id} Found", studentWithDept });
        }

        [HttpGet("{name:alpha}")]
        //[Route("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var Student = StudentRepo.GetByName(name);
            if (Student is null)
            {
                return NotFound(new { msg = $"Student with {name} Not Found" });
            }
            studentWithDept studentWithDept = new studentWithDept();
            studentWithDept.student_Number = Student.Id;
            studentWithDept.student_Name = Student.Name;
            studentWithDept.student_Phone = Student.Phone;
            studentWithDept.student_Email = Student.Email;
            studentWithDept.student_Image = Student.Image;
            studentWithDept.department_Name = Student.Department.Name;
            return Ok(new { msg = $"Student with {name} Found", studentWithDept });
        }

        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentRepo.Add(student);
                //var url = Url.Link("GetOne", new { id = student.Id });
                //return Created(url, "");
                return Ok(new { msg = $"Student ADD" });
            }
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var student = StudentRepo.GetById(id.Value);
            if(student is null)
            {
                return NotFound(new { msg = $"Student with {id} Not Found" });
           
            }
            StudentRepo.Delete(id.Value);
            return Ok(new { msg = $"Student with {id} Deleted" });

        }

        [HttpPut]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentRepo.Update(student);
                // var url = Url.Link("GetOne", new { id = student.Id });
                // return Created(url, "");
                return Ok(new { msg = $"Student Updates", student });
            }
            return BadRequest();

        }





    }
}
