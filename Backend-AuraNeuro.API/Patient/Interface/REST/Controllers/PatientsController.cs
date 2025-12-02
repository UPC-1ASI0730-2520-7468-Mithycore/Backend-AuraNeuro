using System.Net.Mime;
using Backend_AuraNeuro.API.Patient.Domain.Command;
using Backend_AuraNeuro.API.Patient.Domain.Queries;
using Backend_AuraNeuro.API.Patient.Domain.Services.Command;
using Backend_AuraNeuro.API.Patient.Domain.Services.Queries;
using Backend_AuraNeuro.API.Patient.Interface.REST.Resources;
using Backend_AuraNeuro.API.Patient.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend_AuraNeuro.API.Patient.Interface.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Patient profile endpoints.")]
public class PatientsController : ControllerBase
{
    private readonly IPatientCommandService _patientCommandService;
    private readonly IPatientQueryService _patientQueryService;

    public PatientsController(
        IPatientCommandService patientCommandService,
        IPatientQueryService patientQueryService)
    {
        _patientCommandService = patientCommandService;
        _patientQueryService = patientQueryService;
    }

    // POST /api/v1/patients
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create patient account/profile",
        Description = "Creates a patient profile.",
        OperationId = "CreatePatient")]
    [SwaggerResponse(StatusCodes.Status201Created, "Patient created", typeof(PatientResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientResource resource)
    {
        var command = CreatePatientCommandFromResourceAssembler.ToCommandFromResource(resource);
        var patient = await _patientCommandService.Handle(command);

        if (patient is null) return BadRequest();

        var resourceOut = PatientResourceFromEntityAssembler.ToResourceFromEntity(patient);

        return CreatedAtAction(nameof(GetPatientById),
            new { patientId = patient.Id },
            resourceOut);
    }

    // GET /api/v1/patients/{patientId}
    [HttpGet("{patientId:long}")]
    [SwaggerOperation(
        Summary = "Get patient profile",
        Description = "Returns the profile of a patient by Id.",
        OperationId = "GetPatientById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Patient found", typeof(PatientResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
    public async Task<IActionResult> GetPatientById(long patientId)
    {
        var query = new GetPatientByIdQuery(patientId);
        var patient = await _patientQueryService.Handle(query);

        if (patient is null) return NotFound();

        var resource = PatientResourceFromEntityAssembler.ToResourceFromEntity(patient);
        return Ok(resource);
    }

    // GET /api/v1/patients
    [HttpGet]
    [SwaggerOperation(
        Summary = "List all patients",
        Description = "Returns all patient profiles.",
        OperationId = "GetAllPatients")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of patients", typeof(IEnumerable<PatientResource>))]
    public async Task<IActionResult> GetAllPatients()
    {
        var query = new GetAllPatientsQuery();
        var patients = await _patientQueryService.Handle(query);

        var resources = patients.Select(PatientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    // PATCH /api/v1/patients/{patientId}
    [HttpPatch("{patientId:long}")]
    [SwaggerOperation(
        Summary = "Update patient profile",
        Description = "Updates contact info and address of a patient.",
        OperationId = "UpdatePatientProfile")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile updated", typeof(PatientResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
    public async Task<IActionResult> UpdatePatientProfile(
        long patientId,
        [FromBody] UpdatePatientProfileResource resource)
    {
        var command = UpdatePatientProfileCommandFromResourceAssembler
            .ToCommandFromResource(patientId, resource);

        var patient = await _patientCommandService.Handle(command);
        if (patient is null) return NotFound();

        var resourceOut = PatientResourceFromEntityAssembler.ToResourceFromEntity(patient);
        return Ok(resourceOut);
    }

    // DELETE /api/v1/patients/{patientId}
    [HttpDelete("{patientId:long}")]
    [SwaggerOperation(
        Summary = "Soft delete patient",
        Description = "Deactivates the patient profile.",
        OperationId = "DeletePatientProfile")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Patient deactivated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
    public async Task<IActionResult> DeletePatient(long patientId)
    {
        var command = new DeactivatePatientCommand(patientId);
        var success = await _patientCommandService.Handle(command);

        if (!success) return NotFound();

        return NoContent();
    }

    // POST /api/v1/patients/{patientId}/neurologists
    [HttpPost("{patientId:long}/neurologists")]
    [SwaggerOperation(
        Summary = "Assign neurologist to patient",
        Description = "Associates a neurologist with a patient.",
        OperationId = "AssignNeurologistToPatient")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Neurologist assigned")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
    public async Task<IActionResult> AssignNeurologist(
        [FromRoute] long patientId,
        [FromBody] AssignNeurologistResource body)
    {
        // Espera algo como:
        // public class AssignNeurologistResource { public long NeurologistId { get; set; } }

        var success = await _patientCommandService.AssignNeurologistAsync(
            patientId,
            body.NeurologistId);

        if (!success)
            return NotFound("Patient not found.");

        return NoContent();
    }

// DELETE /api/v1/patients/{patientId}/neurologists/{neurologistId}
    [HttpDelete("{patientId:long}/neurologists/{neurologistId:long}")]
    [SwaggerOperation(
        Summary = "Remove neurologist association",
        Description = "Removes neurologist from the patient.",
        OperationId = "RemoveNeurologistFromPatient")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Neurologist removed")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Relation not found")]
    public async Task<IActionResult> RemoveNeurologist(
        [FromRoute] long patientId,
        [FromRoute] long neurologistId)
    {
        var success = await _patientCommandService.RemoveNeurologistAsync(
            patientId,
            neurologistId);

        if (!success)
            return NotFound("Relation not found.");

        return NoContent();
    }
    
    // GET /api/v1/patients/by-neurologist/{neurologistId}
    [HttpGet("by-neurologist/{neurologistId:long}")]
    [SwaggerOperation(
        Summary = "List patients by neurologist",
        Description = "Returns all patients assigned to the specified neurologist.",
        OperationId = "GetPatientsByNeurologistId")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of patients", typeof(IEnumerable<PatientResource>))]
    public async Task<IActionResult> GetPatientsByNeurologist(long neurologistId)
    {
        var query = new GetPatientsByNeurologistIdQuery(neurologistId);
        var patients = await _patientQueryService.Handle(query);

        var resources = patients.Select(PatientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

}
