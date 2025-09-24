namespace StudentApplication.Models
{
    public class Student
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Remarks { get; set; }
        public bool Subscribed { get; set; }


    }
}
