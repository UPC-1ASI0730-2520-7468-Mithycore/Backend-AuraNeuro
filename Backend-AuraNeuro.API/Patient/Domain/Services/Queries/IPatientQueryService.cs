using Backend_AuraNeuro.API.Patient.Domain.Queries;
using PatientModel = Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates.Patient;

namespace Backend_AuraNeuro.API.Patient.Domain.Services.Queries;

/// <summary>
/// Query (read) services for patients.
/// </summary>
public interface IPatientQueryService
{
    Task<IEnumerable<PatientModel>> Handle(GetAllPatientsQuery query);

    Task<PatientModel?> Handle(GetPatientByIdQuery query);

    Task<PatientModel?> Handle(GetPatientByUserIdQuery query);
}