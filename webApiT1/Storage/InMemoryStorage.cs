using webApiT1.Model;
using webApiT1.ModelDto;

namespace webApiT1.Storage;

public class InMemoryStorage : IStorage
{
    private readonly List<Course> _courses = new();
    private readonly List<Student> _students = new();

    public List<Course> GetAllCourses() => _courses;

    public void AddCourse(Course course)
    {
        _courses.Add(course);
        course.Students = new List<Student>();
    }

    public bool CourseExists(Guid courseId) => 
        _courses.Any(c => c.Id == courseId);

    public void AddStudent(Student student)
    {
        _students.Add(student);
        var course = _courses.FirstOrDefault(c => c.Id == student.CourseId);
        if (course == null)
        {
            throw new ArgumentException("Курс не найден");
        }
        course.Students.Add(student);
    }

    public bool RemoveCourse(Guid id)
    {
        var course = _courses.FirstOrDefault(c => c.Id == id);
        if (course == null) return false;
        
        var studentsToRemove = _students.Where(s => s.CourseId == id).ToList();
        foreach (var student in studentsToRemove)
        {
            _students.Remove(student);
        }
        
        return _courses.Remove(course);
    }
}