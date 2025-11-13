namespace Backend_AuraNeuro.API.Appointments.Interfaces.REST;

using Backend_AuraNeuro.API.Appointments.Domain.Model.Commands;
using Backend_AuraNeuro.API.Appointments.Domain.Model.Queries;
using Backend_AuraNeuro.API.Appointments.Domain.Services.Command;
using Backend_AuraNeuro.API.Appointments.Domain.Services.Queries;
using Backend_AuraNeuro.API.Appointments.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentCommandService commandService;
    private readonly IAppointmentQueryService queryService;

    public AppointmentController(
        IAppointmentCommandService commandService,
        IAppointmentQueryService queryService)
    {
        this.commandService = commandService;
        this.queryService = queryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentResource resource)
    {
        var command = new CreateAppointmentCommand(
            resource.NeurologistId,
            resource.PatientId,
            resource.Date,
            resource.Notes);

        var result = await commandService.Handle(command);

        if (result is null) return BadRequest();

        return Ok(new AppointmentResource(
            result.Id,
            result.NeurologistId,
            result.PatientId,
            result.Date,
            result.Status,
            result.Notes));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await queryService.Handle(new GetAllAppointmentsQuery());

        return Ok(list.Select(a =>
            new AppointmentResource(
                a.Id, a.NeurologistId, a.PatientId,
                a.Date, a.Status, a.Notes)));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var appointment = await queryService.Handle(new GetAppointmentByIdQuery(id));

        if (appointment is null) return NotFound();

        return Ok(new AppointmentResource(
            appointment.Id,
            appointment.NeurologistId,
            appointment.PatientId,
            appointment.Date,
            appointment.Status,
            appointment.Notes));
    }
}
