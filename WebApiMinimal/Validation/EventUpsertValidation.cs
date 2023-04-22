using FluentValidation;
using WebApiMinimal.Models.DTO;

namespace WebApiMinimal.Validation
{

        public class EventUpsertValidation : AbstractValidator<EventUpsertDTO>
        {
            public EventUpsertValidation()
            {
            RuleFor(model => model.Id).NotEmpty();
            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Tickets).InclusiveBetween(1, 500);
            }
        }
    
}