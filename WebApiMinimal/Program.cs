using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiMinimal;
using WebApiMinimal.Data;
using WebApiMinimal.Models;
using WebApiMinimal.Models.DTO;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(MapperConfig));
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGet("/events", (ILogger<Program> _logger) =>
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all Events data");
            response.Result = Events.eventList;
            response.StatusCode=HttpStatusCode.OK;
            return Results.Ok(response);
        }).WithName("GetEvents").Produces<APIResponse>(200);


        app.MapGet("/events/{id:guid}", (Guid id) =>
        {
            APIResponse response = new();
            response.Result = Events.eventList.FirstOrDefault((u) => u.Id == id);
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }).WithName("GetEvent").Produces<APIResponse>(200);


        app.MapPost("/events/", async (IMapper _mapper, IValidator<EventCreateDTO> _validator, [FromBody] EventCreateDTO event_C_DTO) =>
        {
            APIResponse response = new() { StatusCode = HttpStatusCode.BadRequest };

            var validationResult = await _validator.ValidateAsync(event_C_DTO);
            if (!validationResult.IsValid)
            {
                response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
                return Results.BadRequest(response);
            }
            if (Events.eventList.FirstOrDefault(i => i.Title.ToLower() == event_C_DTO.Title.ToLower()) != null)
            {
                response.ErrorMessages.Add("Event Name already Exists");
                return Results.BadRequest(response);

            }
            EventsList @event = _mapper.Map<EventsList>(event_C_DTO);
            var newId = Guid.NewGuid();
            @event.Id = Events.eventList.OrderByDescending(i => i.Id).FirstOrDefault().Id = newId;
            Events.eventList.Add(@event);
            EventDTO eventDTO = _mapper.Map<EventDTO>(@event);
            response.Result= eventDTO;
            response.StatusCode= HttpStatusCode.Created;
            return Results.Ok(response);
           // return Results.CreatedAtRoute("GetEvent", new { id = @event.Id }, eventDTO);
        }).WithName("CreateEvent").Accepts<EventsList>("application/json").Produces<APIResponse>(201).Produces(400);



        app.MapPut("/events/", async (IMapper _mapper, IValidator<EventUpsertDTO> _validator, [FromBody] EventUpsertDTO event_U_DTO) =>
        {
            APIResponse response = new() { StatusCode = HttpStatusCode.BadRequest };

            var validationResult = await _validator.ValidateAsync(event_U_DTO);
            if (!validationResult.IsValid)
            {
                response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
                return Results.BadRequest(response);
            }
            EventsList @event = Events.eventList.FirstOrDefault(i => i.Id == event_U_DTO.Id);
            @event.Title= event_U_DTO.Title;
            @event.Tickets= event_U_DTO.Tickets;
            @event.LastUpdated=DateTime.Now;



            response.Result = _mapper.Map<EventDTO>(@event);
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }).WithName("UpsertEvent").Accepts<EventsList>("application/json").Produces<APIResponse>(200).Produces(400);

        app.MapDelete("/events/{id:guid}", (Guid id) =>
        {
            APIResponse response = new() { StatusCode = HttpStatusCode.BadRequest };

            EventsList @event = Events.eventList.FirstOrDefault(i => i.Id == id);
            if (@event != null)
            {
                Events.eventList.Remove(@event);
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);

            }
            else
            {
                response.ErrorMessages.Add("Invalid Id");
                return Results.BadRequest(response);
            }


        });

        app.UseHttpsRedirection();

        app.Run();
    }
}