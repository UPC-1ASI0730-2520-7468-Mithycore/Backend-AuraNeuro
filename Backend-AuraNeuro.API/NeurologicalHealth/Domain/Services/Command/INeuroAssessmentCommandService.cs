using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Command;
using Backend_AuraNeuro.API.NeurologicalHealth.Interface.REST.Resources;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services.Command;

public interface INeuroAssessmentCommandService
{
    Task<NeuroAssessment?> Handle(CreateNeuroAssessmentCommand neuroAssessmentCommand);
    
}