using Backend_AuraNeuro.API.Patient.Domain.Command;
using PatientModel = Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates.Patient;

namespace Backend_AuraNeuro.API.Patient.Domain.Services.Command;

/// <summary>
/// Command (write) services for patients.
/// </summary>
public interface IPatientCommandService
{
    Task<PatientModel?> Handle(CreatePatientCommand command);

    Task<PatientModel?> Handle(UpdatePatientProfileCommand command);

    Task<bool> Handle(DeactivatePatientCommand command);
    
    Task<bool> AssignNeurologistAsync(long patientId, long neurologistId);

    Task<bool> RemoveNeurologistAsync(long patientId, long neurologistId);
}