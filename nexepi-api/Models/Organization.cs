namespace NexepiApi.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<UserOrganization> UserOrganizations { get; set; }
    }
}