namespace StudentMS.Database.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentInternalGuid { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTimeOffset Created {  get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}
