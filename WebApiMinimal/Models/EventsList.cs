namespace WebApiMinimal.Models
{
    public class EventsList
    {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Tickets { get; set; }
    public string Venue { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? LastUpdated { get; set; }
    public List<string> Description { get; set; }
    }
}
