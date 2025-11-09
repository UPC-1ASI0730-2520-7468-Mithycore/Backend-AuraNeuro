using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Command;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Repositories;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services;
using Backend_AuraNeuro.API.NeurologicalHealth.Interface.REST.Resources;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Applications.Internal.CommandServices;

public class NeuroAssessmentService(
    INeurologicalHealthRepository neurologicalHealthRepository,
    IUnitOfWork unitOfWork
    ): INeuroAssessmentService
{
    
    public async Task<NeuroAssessment?> Handle(CreateNeuroAssessmentCommand neuroAssessmentCommand)
    {
        var newNeuroAssessment = new NeuroAssessment(neuroAssessmentCommand);
        await neurologicalHealthRepository.AddAsync(newNeuroAssessment);
        await unitOfWork.CompleteAsync();
        return newNeuroAssessment;
    }

    public async Task<NeuroAssessment?> Handle(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<NeuroAssessment?>> Handle(string number)
    {
        throw new NotImplementedException();
    }
}