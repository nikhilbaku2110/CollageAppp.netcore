using CollageAppp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CollageAppp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            _logger.LogInformation("getstudents method started");
            var students = CollageRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address
              
            });

            //ok - 200 - success
            return Ok(CollageRepository.Students);
        }

        [HttpGet("{id:int}")]
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
            var student = CollageRepository.Students.Where(n => n.Id == id).FirstOrDefault();
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
            var student = CollageRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
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


            int newid = CollageRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newid,
                StudentName = model.StudentName,    
                Email = model.Email,
                Address = model.Address,
            };

            CollageRepository.Students.Add(student);
            
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

            var existingStudent = CollageRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();

            if(existingStudent == null)
                return NotFound();

            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;

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

            var existingStudent = CollageRepository.Students.Where(s => s.Id == id).FirstOrDefault();

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
            var student = CollageRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound("the student with id is not found");

            CollageRepository.Students.Remove(student); 

            //ok - 200 - success
            return Ok(true);

           
        }
    }
}
