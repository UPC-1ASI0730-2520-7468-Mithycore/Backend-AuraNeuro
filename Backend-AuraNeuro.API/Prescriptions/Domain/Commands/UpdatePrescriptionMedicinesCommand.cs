// Summary: Command to replace the medicines list of an existing Prescription.
namespace Backend_AuraNeuro.API.Prescriptions.Domain.Commands;

public record UpdatePrescriptionMedicinesCommand(
    long PrescriptionId,
    IEnumerable<MedicineItemCommandItem> Medicines
);