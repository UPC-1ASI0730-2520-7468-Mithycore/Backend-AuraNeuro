// Summary: Application boundary for read operations related to prescriptions.
using PrescriptionAggregate = Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates.Prescription;
using Backend_AuraNeuro.API.Prescriptions.Domain.Queries;

namespace Backend_AuraNeuro.API.Prescriptions.Domain.Services;

public interface IPrescriptionQueryService
{
    Task<PrescriptionAggregate?> Handle(GetPrescriptionByIdQuery query);
    Task<IEnumerable<PrescriptionAggregate>> Handle(GetPrescriptionsByPatientIdQuery query);
    Task<IEnumerable<PrescriptionAggregate>> Handle(GetPrescriptionsByNeurologistIdQuery query);
}