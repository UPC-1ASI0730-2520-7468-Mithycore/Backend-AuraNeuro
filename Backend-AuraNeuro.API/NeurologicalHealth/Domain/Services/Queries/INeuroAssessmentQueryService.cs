using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Queries;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services.Queries;

public interface INeuroAssessmentQueryService
{
    Task<IEnumerable<NeuroAssessment>> Handle(GetNeuroAssessmentsByPatientIdQuery query);
    Task<IEnumerable<NeuroAssessment>> Handle(GetNeuroAssessmentsByNeurologistIdQuery query);
}