namespace Core.Models.RequestViewCoreModel
{
    public class AddStudentToCourseRequestViewCoreModel
    {
        public string CourseInternalGuid { get; set; } = default!;
        public List<StudentCourseRequestViewCoreModel> Students { get; set; } = default!;
    }
}
