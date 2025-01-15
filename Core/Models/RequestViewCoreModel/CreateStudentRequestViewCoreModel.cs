namespace Core.Models.RequestViewCoreModel
{
    public class CreateStudentRequestViewCoreModel
    {
        public string StudentName { get; set; } = default!;
        public string StudentLastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
