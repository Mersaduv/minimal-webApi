using System;
using WebApiMinimal.Models;

namespace WebApiMinimal.Data
{
    public static class Events
    {
        public static List<EventsList> eventList = new List<EventsList>
        {
            new EventsList{
            Id = Guid.NewGuid(),
            Title = "Event 1",
            Tickets = 100,
            Venue = "Venue 1",
            Created = DateTime.Now,
            LastUpdated = DateTime.Now,
            Description = new List<string> { "Description 1", "Description 2" }
        }, new EventsList{
            Id = Guid.NewGuid(),
            Title = "Event 2",
            Tickets = 200,
            Venue = "Venue 2",
            Created = DateTime.Now,
            LastUpdated = DateTime.Now,
            Description = new List<string> { "Description 1", "Description 2" }
        }, new EventsList{
            Id = Guid.NewGuid(),
            Title = "Event 3",
            Tickets = 300,
            Venue = "Venue 3",
            Created = DateTime.Now,
            LastUpdated = DateTime.Now,
            Description = new List<string> { "Description 1", "Description 2" }
        }
        };
    }
}
