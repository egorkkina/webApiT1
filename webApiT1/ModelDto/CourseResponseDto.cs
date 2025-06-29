namespace webApiT1.ModelDto;

public class CourseResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<StudentResponseDto> Students { get; set; } = new();
}