namespace CourseMS.Database.Entities
{
    public class CourseStudent
    {
        public int Id { get; set; }
        public string StudentInternalGuid { get; set; } = default!;
        public string StudentName { get; set; } = default!;
        public Course? Course { get; set; }
    }
}
