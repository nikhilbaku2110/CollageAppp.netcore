using CollageAppp.Data;
using CollageAppp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollageAppp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly CollageDBContext _dbcontext;
        public StudentController(ILogger<StudentController> logger, CollageDBContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            _logger.LogInformation("getstudents method started");
            var students = _dbcontext.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address,
                DOB = s.DOB
              
            }).ToList();

            //ok - 200 - success
            return Ok(_dbcontext.Students);
        }

        [HttpGet("{id:int}",Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            //bad request - 400 - client error
            if (id <= 0)
            {
                _logger.LogWarning("bad request");
                return BadRequest();
            }
            //not found - 404 - client error
            var student = _dbcontext.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                _logger.LogError("student not found giving this id");
                return NotFound("the student with id is not found");
            }

            var StudentDTO = new StudentDTO()
            {
                Id=student.Id,
                StudentName=student.StudentName,
                Email = student.Email,
                Address =student.Address
                
            };

            //ok - 200 - success
            return Ok(StudentDTO);
        }

        [HttpGet("{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentByName(string name)
        {
            //bad request - 400 - client error
            if (string.IsNullOrEmpty(name))
               return BadRequest();

            //not found - 404 - client error
            var student = _dbcontext.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
               return NotFound("the student with id is not found");

            var StudentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address

            };

            //ok - 200 - success
            return Ok(StudentDTO);
        }

        [HttpPost]
        [Route("create")]
        //api//student//create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
           if(model == null)
              return BadRequest();


            Student student = new Student
            {
                StudentName = model.StudentName,    
                Email = model.Email,
                Address = model.Address,
            };

            _dbcontext.Students.Add(student);
            _dbcontext.SaveChanges();
            model.Id = student.Id;
            //status - 201  
            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
            
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                 BadRequest();

            var existingStudent = _dbcontext.Students.Where(s => s.Id == model.Id).FirstOrDefault();

            if(existingStudent == null)
                return NotFound();

            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;
            _dbcontext.SaveChanges();
            return NoContent();
        }


        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        //api//student/1/updatepartial
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult UpdateStudentPartial(int id,[FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            var existingStudent = _dbcontext.Students.Where(s => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address,

            };

            patchDocument.ApplyTo(studentDTO, ModelState);

            if(!ModelState.IsValid)
                return BadRequest();

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO    .Address;
            _dbcontext.SaveChanges();

            //204 - nocontent
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool>  DeleteStudent(int id)
        {

            //bad request - 400 - client error
            if (id <= 0)
                return BadRequest();

            //not found - 404 - client error
            var student = _dbcontext.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound("the student with id is not found");

            _dbcontext.Students.Remove(student);
            _dbcontext.SaveChanges();
            //ok - 200 - success
            return Ok(true);

           
        }
    }
}
