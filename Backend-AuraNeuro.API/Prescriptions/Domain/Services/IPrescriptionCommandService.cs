// Summary: Application boundary for write operations related to prescriptions.
using Backend_AuraNeuro.API.Prescriptions.Domain.Commands;
using PrescriptionAggregate = Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates.Prescription;

namespace Backend_AuraNeuro.API.Prescriptions.Domain.Services;

public interface IPrescriptionCommandService
{
    Task<PrescriptionAggregate?> Handle(CreatePrescriptionCommand command);
    Task<PrescriptionAggregate?> Handle(UpdatePrescriptionMedicinesCommand command);
    Task<PrescriptionAggregate?> Handle(RevokePrescriptionCommand command);
}