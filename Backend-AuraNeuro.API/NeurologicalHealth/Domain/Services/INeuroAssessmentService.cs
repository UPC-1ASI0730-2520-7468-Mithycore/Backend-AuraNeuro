using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Command;
using Backend_AuraNeuro.API.NeurologicalHealth.Interface.REST.Resources;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services;

public interface INeuroAssessmentService
{
    Task<NeuroAssessment?> Handle(CreateNeuroAssessmentCommand neuroAssessmentCommand);
    Task<NeuroAssessment?> Handle(long id);
    Task<IEnumerable<NeuroAssessment?>> Handle(string number);
}