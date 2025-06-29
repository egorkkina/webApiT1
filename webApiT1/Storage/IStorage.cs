using webApiT1.Model;
using webApiT1.ModelDto;

namespace webApiT1.Storage;

public interface IStorage
{
    List<Course> GetAllCourses();
    void AddCourse(Course course);
    bool CourseExists(Guid courseId);
    void AddStudent(Student student);
    bool RemoveCourse(Guid id);
}