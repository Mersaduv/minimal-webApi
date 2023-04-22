using FluentValidation;
using WebApiMinimal.Models.DTO;

namespace WebApiMinimal.Validation
{
    public class EventCreateValidation : AbstractValidator<EventCreateDTO>
    {
        public EventCreateValidation()
        {
            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Tickets).InclusiveBetween(1,500);
        }
    }
}
