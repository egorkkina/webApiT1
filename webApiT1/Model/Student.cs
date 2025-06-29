namespace webApiT1.Model;

public class Student
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public Guid CourseId { get; set; }
}