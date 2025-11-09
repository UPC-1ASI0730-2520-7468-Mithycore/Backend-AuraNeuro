using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Queries;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Repositories;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services.Queries;
using Backend_AuraNeuro.API.NeurologicalHealth.Infrastructure.Persistence.EFC.Repositories;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Applications.Internal.QueryServices;

public class NeuroAssessmentQueryService(
    INeurologicalHealthRepository neurologicalHealthRepository
    ) : INeuroAssessmentQueryService 
{
    public async Task<IEnumerable<NeuroAssessment>> Handle(GetNeuroAssessmentsByPatientIdQuery query)
    {
        var neuroAssessments = await neurologicalHealthRepository.FindByPatientIdAsync(query.PatientId);

        return neuroAssessments;
    }

    public async Task<IEnumerable<NeuroAssessment>> Handle(GetNeuroAssessmentsByNeurologistIdQuery query)
    {
        var neuroAssessment = await neurologicalHealthRepository.FindByNeurologistIdAsync(query.NeurologistId);
        return neuroAssessment;
    }
}