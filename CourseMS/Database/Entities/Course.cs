namespace CourseMS.Database.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseInternalGuid { get; set; } = default!;
        public string CourseName { get; set; } = default!;
        public DateTimeOffset Created { get; set; }
        public List<CourseStudent> Students { get; set; } = new();
    }
}
