namespace Core.Models.DTOs
{
    public class GetCourseEntityDTO
    {
        public string CourseInternalGuid { get; set; } = default!;
        public string CourseName { get; set; } = default!;
        public DateTimeOffset Created { get; set; }
        public List<GetCourseStudentEntityDTO> Students { get; set; } = new();
    }
}
