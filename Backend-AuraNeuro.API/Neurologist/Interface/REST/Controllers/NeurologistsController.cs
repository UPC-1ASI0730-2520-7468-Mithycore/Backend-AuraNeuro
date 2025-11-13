using System.Net.Mime;
using Backend_AuraNeuro.API.Neurologist.Domain.Model.Commands;
using Backend_AuraNeuro.API.Neurologist.Domain.Model.Queries;
using Backend_AuraNeuro.API.Neurologist.Domain.Services.Command;
using Backend_AuraNeuro.API.Neurologist.Domain.Services.Queries;
using Backend_AuraNeuro.API.Neurologist.Interfaces.REST.Resources;
using Backend_AuraNeuro.API.Neurologist.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend_AuraNeuro.API.Neurologist.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Neurologist profile endpoints.")]
public class NeurologistsController : ControllerBase
{
    private readonly INeurologistCommandService neurologistCommandService;
    private readonly INeurologistQueryService neurologistQueryService;

    public NeurologistsController(
        INeurologistCommandService neurologistCommandService,
        INeurologistQueryService neurologistQueryService)
    {
        this.neurologistCommandService = neurologistCommandService;
        this.neurologistQueryService = neurologistQueryService;
    }

    // POST /api/v1/neurologists
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create neurologist account/profile",
        Description = "Creates a neurologist profile.",
        OperationId = "CreateNeurologist")]
    [SwaggerResponse(StatusCodes.Status201Created, "Profile created", typeof(NeurologistResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    public async Task<IActionResult> CreateNeurologist([FromBody] CreateNeurologistResource resource)
    {
        var command = CreateNeurologistCommandFromResourceAssembler.ToCommandFromResource(resource);
        var neurologist = await neurologistCommandService.Handle(command);
        if (neurologist is null) return BadRequest();

        var neurologistResource = NeurologistResourceFromEntityAssembler.ToResourceFromEntity(neurologist);
        return CreatedAtAction(nameof(GetNeurologistById),
            new { neurologistId = neurologist.Id },
            neurologistResource);
    }

    // GET /api/v1/neurologists/{neurologistId}
    [HttpGet("{neurologistId:long}")]
    [SwaggerOperation(
        Summary = "Get neurologist profile",
        Description = "Gets the profile of a neurologist by Id.",
        OperationId = "GetNeurologistById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile found", typeof(NeurologistResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> GetNeurologistById(long neurologistId)
    {
        var query = new GetNeurologistByIdQuery(neurologistId);
        var neurologist = await neurologistQueryService.Handle(query);
        if (neurologist is null) return NotFound();

        var resource = NeurologistResourceFromEntityAssembler.ToResourceFromEntity(neurologist);
        return Ok(resource);
    }

    // GET /api/v1/neurologists
    [HttpGet]
    [SwaggerOperation(
        Summary = "List neurologists",
        Description = "Returns all neurologist profiles.",
        OperationId = "GetAllNeurologists")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of neurologists", typeof(IEnumerable<NeurologistResource>))]
    public async Task<IActionResult> GetAllNeurologists()
    {
        var query = new GetAllNeurologistsQuery();
        var neurologists = await neurologistQueryService.Handle(query);
        var resources = neurologists.Select(NeurologistResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    // PATCH /api/v1/neurologists/{neurologistId}
    [HttpPatch("{neurologistId:long}")]
    [SwaggerOperation(
        Summary = "Update neurologist profile",
        Description = "Updates contact info, address, specialties and verification status.",
        OperationId = "UpdateNeurologistProfile")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile updated", typeof(NeurologistResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> UpdateNeurologistProfile(
        long neurologistId,
        [FromBody] UpdateNeurologistProfileResource resource)
    {
        var command = UpdateNeurologistProfileCommandFromResourceAssembler
            .ToCommandFromResource(neurologistId, resource);

        var neurologist = await neurologistCommandService.Handle(command);
        if (neurologist is null) return NotFound();

        var neurologistResource = NeurologistResourceFromEntityAssembler.ToResourceFromEntity(neurologist);
        return Ok(neurologistResource);
    }

    // DELETE /api/v1/neurologists/{neurologistId} (soft delete)
    [HttpDelete("{neurologistId:long}")]
    [SwaggerOperation(
        Summary = "Soft delete neurologist profile",
        Description = "Deactivates the neurologist profile.",
        OperationId = "DeleteNeurologistProfile")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Profile deactivated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> DeleteNeurologist(long neurologistId)
    {
        var command = new DeactivateNeurologistCommand(neurologistId);
        var success = await neurologistCommandService.Handle(command);
        if (!success) return NotFound();

        return NoContent();
    }

    // GET /api/v1/neurologists/{neurologistId}/patients
    // Stub for future integration with Patients/Assessments bounded contexts.
    [HttpGet("{neurologistId:long}/patients")]
    [SwaggerOperation(
        Summary = "List patients associated with neurologist",
        Description = "Returns the list of patients associated with the neurologist (stub for now).",
        OperationId = "GetNeurologistPatients")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    public Task<IActionResult> GetPatientsForNeurologist(long neurologistId)
    {
        var empty = Array.Empty<object>();
        return Task.FromResult<IActionResult>(Ok(empty));
    }
}
