namespace webApiT1.Model;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
}