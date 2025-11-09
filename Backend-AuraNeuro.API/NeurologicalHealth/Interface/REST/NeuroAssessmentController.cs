using System.Net.Mime;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services;
using Backend_AuraNeuro.API.NeurologicalHealth.Interface.REST.Resources;
using Backend_AuraNeuro.API.NeurologicalHealth.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Interface.REST;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class NeuroAssessmentController(INeuroAssessmentService neuroAssessmentService): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateNeuroAssessment([FromBody] CreateNeuroAssessmentCommandResource neuroAssessmentCommandResource)
    {
        var result =
            await neuroAssessmentService.Handle(
                CreateNeuroAssessmentFromResource.ToCommandFromResource(neuroAssessmentCommandResource));
        if (result is null) return BadRequest();
        return Ok(result);
    }
} 