using Microsoft.AspNetCore.Mvc;
using webApiT1.Model;
using webApiT1.ModelDto;
using webApiT1.Storage;

namespace webApiT1.Controller;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IStorage _inMemoryStorage;
    
    public CoursesController(IStorage storage)
    {
        _inMemoryStorage = storage;
    }
    
    [HttpGet]
    public IActionResult GetCourses()
    {
        var courses = _inMemoryStorage.GetAllCourses()
            .Select(c => new CourseResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Students = c.Students.Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    FullName = s.FullName
                }).ToList()
            });
    
        return Ok(courses);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody]CreateEntityRequest courseDto)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = courseDto.Name
        };
        
        _inMemoryStorage.AddCourse(course);

        return Ok(new EntityCreatedResponse
        {
            Id = course.Id
        });
    }

    [HttpPost("{courseId:guid}/students")]
    public IActionResult CreateStudent([FromRoute] Guid courseId, [FromBody] CreateEntityRequest studentDto)
    {
        if (!_inMemoryStorage.CourseExists(courseId))
        {
            return NotFound(new { Message = "Курс не найден" });
        }
        
        var newStudent = new Student
        {
            Id = Guid.NewGuid(),
            FullName = studentDto.Name,
            CourseId = courseId
        };
        
        _inMemoryStorage.AddStudent(newStudent);
        
        return Ok(new EntityCreatedResponse
        {
            Id = newStudent.Id
        });
    }

    [HttpDelete("{courseId:guid}")]
    public IActionResult DeleteCourse([FromRoute] Guid courseId)
    {
        var removeCourse = _inMemoryStorage.RemoveCourse(courseId);
        if (!removeCourse) return BadRequest("Ошибка удаления курса");
        return Ok("Курс успешно удалён");
    }
}