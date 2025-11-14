using Backend_AuraNeuro.API.Prescriptions.Domain.Commands;
using Backend_AuraNeuro.API.Prescriptions.Domain.Queries;
using Backend_AuraNeuro.API.Prescriptions.Domain.Services;
using Backend_AuraNeuro.API.Prescriptions.Interface.REST.Resources;
using Backend_AuraNeuro.API.Prescriptions.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Backend_AuraNeuro.API.Prescriptions.Interface.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionCommandService _commandService;
    private readonly IPrescriptionQueryService _queryService;

    public PrescriptionsController(
        IPrescriptionCommandService commandService,
        IPrescriptionQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    // POST /api/v1/prescriptions
    [HttpPost]
    public async Task<ActionResult<PrescriptionResource>> Create([FromBody] CreatePrescriptionResource resource)
    {
        var command = new CreatePrescriptionCommand(
            resource.PatientId,
            resource.NeurologistId,
            resource.Medicines.Select(m => new MedicineItemCommandItem(m.Name, m.Dosage, m.Frequency)),
            resource.IssuedAt
        );

        var result = await _commandService.Handle(command);
        if (result is null) return BadRequest();

        var prescriptionResource = PrescriptionResourceFromEntityAssembler.ToResource(result);
        return CreatedAtAction(nameof(GetById), new { prescriptionId = result.Id }, prescriptionResource);
    }

    // GET /api/v1/prescriptions/{prescriptionId}
    [HttpGet("{prescriptionId:long}")]
    public async Task<ActionResult<PrescriptionResource>> GetById(long prescriptionId)
    {
        var query = new GetPrescriptionByIdQuery(prescriptionId);
        var result = await _queryService.Handle(query);
        if (result is null) return NotFound();

        return Ok(PrescriptionResourceFromEntityAssembler.ToResource(result));
    }

    // GET /api/v1/patients/{patientId}/prescriptions
    [HttpGet("/api/v1/patients/{patientId:guid}/prescriptions")]
    public async Task<ActionResult<IEnumerable<PrescriptionResource>>> GetByPatientId(Guid patientId)
    {
        var query = new GetPrescriptionsByPatientIdQuery(patientId);
        var result = await _queryService.Handle(query);

        var resources = result.Select(PrescriptionResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    // GET /api/v1/neurologists/{neurologistId}/prescriptions
    [HttpGet("/api/v1/neurologists/{neurologistId:guid}/prescriptions")]
    public async Task<ActionResult<IEnumerable<PrescriptionResource>>> GetByNeurologistId(Guid neurologistId)
    {
        var query = new GetPrescriptionsByNeurologistIdQuery(neurologistId);
        var result = await _queryService.Handle(query);

        var resources = result.Select(PrescriptionResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    // PATCH /api/v1/prescriptions/{prescriptionId}
    [HttpPatch("{prescriptionId:long}")]
    public async Task<ActionResult<PrescriptionResource>> UpdateMedicines(
        long prescriptionId,
        [FromBody] UpdatePrescriptionMedicinesResource resource)
    {
        var command = new UpdatePrescriptionMedicinesCommand(
            prescriptionId,
            resource.Medicines.Select(m => new MedicineItemCommandItem(m.Name, m.Dosage, m.Frequency))
        );

        var result = await _commandService.Handle(command);
        if (result is null) return NotFound();

        return Ok(PrescriptionResourceFromEntityAssembler.ToResource(result));
    }

    // DELETE /api/v1/prescriptions/{prescriptionId}
    [HttpDelete("{prescriptionId:long}")]
    public async Task<IActionResult> Revoke(long prescriptionId)
    {
        var command = new RevokePrescriptionCommand(prescriptionId);
        var result = await _commandService.Handle(command);
        if (result is null) return NotFound();

        return NoContent();
    }
}
