//using Lab1.CustomFilter;
using Lab1.DTO;
using Lab1.Model;
using Lab1.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentRepo DepartmentRepo;
        IStudentRepo StudentRepo;
        public DepartmentController
            (IDepartmentRepo _departmentRepo , IStudentRepo _studentRepo) 
        { 
            DepartmentRepo = _departmentRepo ;
            StudentRepo = _studentRepo ;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Departments = DepartmentRepo.GetAll();
            if (Departments is null)
            {
                return NotFound();
            }

            var deptWithStudents = Departments.Select(d => new DeptWithStudents
            {
                Department_Name = d.Name,
                Department_Number = d.Id,
                Location = d.Location,
                Manager_Name = d.MangerName,
                Student_Name = d.Students.Select(s => s.Name).ToList()
            });
       
            return Ok(deptWithStudents);

        }
        [HttpGet("{id:int}")]
        //[Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var Department = DepartmentRepo.GetById(id);
            if (Department is null)
            {
                return NotFound(new { msg = $"Department with {id} Not Found" });
            }
            DeptWithStudents deptWithStudents = new DeptWithStudents();
            deptWithStudents.Department_Number = id;
            deptWithStudents.Department_Name = Department.Name;
            deptWithStudents.Location = Department.Location;
            deptWithStudents.Manager_Name=Department.MangerName;
            foreach (var student in Department.Students)
            {
                deptWithStudents.Student_Name.Add(student.Name);
            }

            return Ok(new { msg = $"Department with {id} Found", deptWithStudents });
        }

        [HttpGet("{name:alpha}")]
        //[Route("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var Department = DepartmentRepo.GetByName(name);
            if (Department is null)
            {
                return NotFound(new { msg = $"Department with {name} Not Found" });
            }
            DeptWithStudents deptWithStudents = new DeptWithStudents();
            deptWithStudents.Department_Number = Department.Id;
            deptWithStudents.Department_Name = Department.Name;
            deptWithStudents.Location = Department.Location;
            deptWithStudents.Manager_Name = Department.MangerName;
            foreach (var student in Department.Students)
            {
                deptWithStudents.Student_Name.Add(student.Name);
            }
            return Ok(new { msg = $"Department with {name} Found", deptWithStudents });
        }

        [HttpPost]
        [LocationFilter]
        public IActionResult Add(Department Department)
        {
            if (ModelState.IsValid)
            {
                DepartmentRepo.Add(Department);
                //      var url = Url.Link("GetOne", new { id = Department.Id });
                //      return Created(url, "");
                return Ok(new { msg = $"Department ADD" });
            }
            
            return BadRequest(ModelState);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var Department = DepartmentRepo.GetById(id.Value);
            if (Department is null)
            {
                return NotFound(new { msg = $"Department with {id} Not Found" });

            }
            DepartmentRepo.Delete(id.Value);
            return Ok(new { msg = $"Department with {id} Deleted" });

        }

        [HttpPut]
        [LocationFilter]
        public IActionResult Update(Department Department)
        {
            if (ModelState.IsValid)
            {
                DepartmentRepo.Update(Department);
                // var url = Url.Link("GetOne", new { id = student.Id });
                // return Created(url, "");
                return Ok(new { msg = $"Department Updates", Department });
            }
            return BadRequest();

        }
    }
}
