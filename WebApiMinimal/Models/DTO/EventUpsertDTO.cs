namespace WebApiMinimal.Models.DTO
{
    public class EventUpsertDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Tickets { get; set; }
        public string Venue { get; set; }
        public List<string> Description { get; set; }
    }
}
