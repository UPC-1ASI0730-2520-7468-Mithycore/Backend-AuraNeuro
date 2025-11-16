using Backend_AuraNeuro.API.Patient.Domain.Command;
using Backend_AuraNeuro.API.Patient.Domain.Repositories;
using Backend_AuraNeuro.API.Patient.Domain.Services.Command;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;
using PatientModel = Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates.Patient;

namespace Backend_AuraNeuro.API.Patient.Application.Internal.CommandServices;

public class PatientCommandService : IPatientCommandService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PatientCommandService(
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PatientModel?> Handle(CreatePatientCommand command)
    {
        var existingByEmail = await _patientRepository.FindByEmailAsync(command.Email);
        if (existingByEmail is not null)
            throw new Exception("A patient with this email already exists.");

        var patient = new PatientModel(
            command.UserId,
            command.FirstName,
            command.LastName,
            command.Email,
            command.PhoneNumber,
            command.BirthDate,
            command.Street,
            command.City,
            command.Country
        );

        await _patientRepository.AddAsync(patient);
        await _unitOfWork.CompleteAsync();
        return patient;
    }

    public async Task<PatientModel?> Handle(UpdatePatientProfileCommand command)
    {
        var patient = await _patientRepository.FindByIdAsync(command.PatientId);
        if (patient is null) return null;

        patient.UpdateContactInfo(command.Email, command.PhoneNumber);
        patient.UpdateAddress(command.Street, command.City, command.Country);

        _patientRepository.Update(patient);
        await _unitOfWork.CompleteAsync();
        return patient;
    }

    public async Task<bool> Handle(DeactivatePatientCommand command)
    {
        var patient = await _patientRepository.FindByIdAsync(command.PatientId);
        if (patient is null) return false;

        patient.Deactivate();

        _patientRepository.Update(patient);
        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<bool> AssignNeurologistAsync(long patientId, long neurologistId)
    {
        var success = await _patientRepository.AssignNeurologistAsync(patientId, neurologistId);
        if (!success) return false;

        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<bool> RemoveNeurologistAsync(long patientId, long neurologistId)
    {
        var success = await _patientRepository.RemoveNeurologistAsync(patientId, neurologistId);
        if (!success) return false;

        await _unitOfWork.CompleteAsync();
        return true;
    }
}
