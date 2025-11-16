namespace Backend_AuraNeuro.API.Prescriptions.Interface.REST.Resources;


public class UpdatePrescriptionMedicinesResource
{
    public IEnumerable<MedicineItemResource> Medicines { get; set; } = new List<MedicineItemResource>();
}