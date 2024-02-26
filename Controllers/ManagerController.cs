using Lab1.Model;
using Lab1.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        IManagerRepo ManagerRepo;

        public ManagerController(IManagerRepo managerRepo)
        {
            ManagerRepo = managerRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Managers = ManagerRepo.GetAll();
            if (Managers is null)
            {
                return NotFound();
            }
            return Ok(Managers);

        }
        [HttpGet("{id:int}")]
        //[Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var Manager = ManagerRepo.GetById(id);
            if (Manager is null)
            {
                return NotFound(new { msg = $"Student with {id} Not Found" });
            }
            return Ok(new { msg = $"Manager with {id} Found", Manager });
        }

        [HttpGet("{name:alpha}")]
        //[Route("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var Manager = ManagerRepo.GetByName(name);
            if (Manager is null)
            {
                return NotFound(new { msg = $"Manager with {name} Not Found" });
            }
            return Ok(new { msg = $"Manager with {name} Found", Manager });
        }

        [HttpPost]
        public IActionResult Add(Manager Manager)
        {
            if (ModelState.IsValid)
            {
                ManagerRepo.Add(Manager);
                //var url = Url.Link("GetOne", new { id = student.Id });
                //return Created(url, "");
                return Ok(new { msg = $"Manager ADD" });
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
            var Manager = ManagerRepo.GetById(id.Value);
            if (Manager is null)
            {
                return NotFound(new { msg = $"Manager with {id} Not Found" });

            }
            ManagerRepo.Delete(id.Value);
            return Ok(new { msg = $"Manager with {id} Deleted" });

        }

        [HttpPut]
        public IActionResult Update(Manager Manager)
        {
            if (ModelState.IsValid)
            {
                ManagerRepo.Update(Manager);
                // var url = Url.Link("GetOne", new { id = student.Id });
                // return Created(url, "");
                return Ok(new { msg = $"Manager Updates", Manager });
            }
            return BadRequest();

        }
    }
}
