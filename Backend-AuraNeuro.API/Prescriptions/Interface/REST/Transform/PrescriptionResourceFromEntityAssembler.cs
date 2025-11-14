// Summary: Maps a Prescription domain entity to its REST resource representation.
using System.Linq;
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Prescriptions.Interface.REST.Resources;

using PrescriptionAggregate = Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates.Prescription;

namespace Backend_AuraNeuro.API.Prescriptions.Interface.REST.Transform;

public static class PrescriptionResourceFromEntityAssembler
{
    public static PrescriptionResource ToResource(PrescriptionAggregate model) =>
        new()
        {
            Id = model.Id,
            PatientId = model.PatientId,
            NeurologistId = model.NeurologistId,
            IssuedAt = model.IssuedAt,
            SignatureHash = model.SignatureHash,
            Revoked = model.Revoked,
            Medicines = model.Medicines
                .Select(m => new MedicineItemResource
                {
                    Name = m.Name,
                    Dosage = m.Dosage,
                    Frequency = m.Frequency
                })
                .ToList()
        };
}