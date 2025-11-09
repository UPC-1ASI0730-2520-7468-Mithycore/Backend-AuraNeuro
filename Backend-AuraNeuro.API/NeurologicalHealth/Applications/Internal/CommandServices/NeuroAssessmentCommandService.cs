using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Command;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Repositories;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services.Command;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Applications.Internal.CommandServices;

public class NeuroAssessmentCommandService(
    INeurologicalHealthRepository neurologicalHealthRepository,
    IUnitOfWork unitOfWork
    ): INeuroAssessmentCommandService
{
    
    public async Task<NeuroAssessment?> Handle(CreateNeuroAssessmentCommand neuroAssessmentCommand)
    {
        var newNeuroAssessment = new NeuroAssessment(neuroAssessmentCommand);
        await neurologicalHealthRepository.AddAsync(newNeuroAssessment);
        await unitOfWork.CompleteAsync();
        return newNeuroAssessment;
    }

}