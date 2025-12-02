using Backend_AuraNeuro.API.Patient.Domain.Queries;
using Backend_AuraNeuro.API.Patient.Domain.Repositories;
using Backend_AuraNeuro.API.Patient.Domain.Services.Queries;
using PatientModel = Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates.Patient;

namespace Backend_AuraNeuro.API.Patient.Application.Internal.QueryServices;

/// <summary>
/// Implementation of patient query handlers.
/// </summary>
public class PatientQueryService : IPatientQueryService
{
    private readonly IPatientRepository patientRepository;

    public PatientQueryService(IPatientRepository patientRepository)
    {
        this.patientRepository = patientRepository;
    }

    public async Task<IEnumerable<PatientModel>> Handle(GetAllPatientsQuery query)
    {
        return await patientRepository.ListAsync();
    }

    public async Task<PatientModel?> Handle(GetPatientByIdQuery query)
    {
        return await patientRepository.FindByIdAsync(query.PatientId);
    }

    public async Task<PatientModel?> Handle(GetPatientByUserIdQuery query)
    {
        return await patientRepository.FindByUserIdAsync(query.UserId);
    }
    
    public async Task<IEnumerable<PatientModel>> Handle(GetPatientsByNeurologistIdQuery query)
    {
        return await patientRepository.FindByNeurologistIdAsync(query.NeurologistId);
    }
}