// Summary: Handles query objects and reads from the prescription repository.
using PrescriptionAggregate = Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates.Prescription;
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.Repositories;
using Backend_AuraNeuro.API.Prescriptions.Domain.Queries;
using Backend_AuraNeuro.API.Prescriptions.Domain.Services;

namespace Backend_AuraNeuro.API.Prescriptions.Application.Internal.QueryServices;

public class PrescriptionQueryService(IPrescriptionRepository prescriptionRepository)
    : IPrescriptionQueryService
{
    public async Task<PrescriptionAggregate?> Handle(GetPrescriptionByIdQuery query) =>
        await prescriptionRepository.FindByIdAsync(query.PrescriptionId);


    public async Task<IEnumerable<PrescriptionAggregate>> Handle(GetPrescriptionsByPatientIdQuery query) =>
        await prescriptionRepository.FindByPatientIdAsync(query.PatientId);

    public async Task<IEnumerable<PrescriptionAggregate>> Handle(GetPrescriptionsByNeurologistIdQuery query) =>
        await prescriptionRepository.FindByNeurologistIdAsync(query.NeurologistId);
}