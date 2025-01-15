namespace Core.Models.RequestViewCoreModel
{
    public class GetListOfCourseRequestViewCoreModel
    {
        public string CourseName { get; set; } = default!;
        public DateTimeOffset Created { get; set; } = default!;
        public string CourseInternalGuid { get; set; } = default!;
        public List<GetListOfStudentCourseRequestViewCoreModel> Students { get; set; } = new();
    }

    public class GetListOfStudentCourseRequestViewCoreModel
    {
        public string StudentInternalGuid { get; set; } = default!;
        public string StudentName { get; set; } = default!;
    }
}
