// Summary: Handles commands and persists Prescription aggregates via the repository.
using Backend_AuraNeuro.API.Prescriptions.Domain.Commands;
using PrescriptionAggregate = Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates.Prescription;
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.Repositories;
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.ValueObjects;
using Backend_AuraNeuro.API.Prescriptions.Domain.Services;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

namespace Backend_AuraNeuro.API.Prescriptions.Application.Internal.CommandServices;

public class PrescriptionCommandService(
    IPrescriptionRepository prescriptionRepository,
    IUnitOfWork unitOfWork)
    : IPrescriptionCommandService
{
    public async Task<PrescriptionAggregate?> Handle(CreatePrescriptionCommand command)
    {
        var prescription = new PrescriptionAggregate(command);

        try
        {
            await prescriptionRepository.AddAsync(prescription);
            await unitOfWork.CompleteAsync();
            return prescription;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<PrescriptionAggregate?> Handle(UpdatePrescriptionMedicinesCommand command)
    {
        var prescription = await prescriptionRepository.FindByIdAsync(command.PrescriptionId);
        if (prescription is null) return null;

        var medicines = command.Medicines
            .Select(m => new MedicineItem(m.Name, m.Dosage, m.Frequency));

        prescription.UpdateMedicines(medicines);
        await unitOfWork.CompleteAsync();
        return prescription;
    }

    public async Task<PrescriptionAggregate?> Handle(RevokePrescriptionCommand command)
    {
        var prescription = await prescriptionRepository.FindByIdAsync(command.PrescriptionId);
        if (prescription is null) return null;

        prescription.Revoke();
        await unitOfWork.CompleteAsync();
        return prescription;
    }
}