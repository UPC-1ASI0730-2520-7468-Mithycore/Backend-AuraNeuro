using Backend_AuraNeuro.API.Neurologist.Domain.Commands;
using Backend_AuraNeuro.API.Neurologist.Domain.Repositories;
using Backend_AuraNeuro.API.Neurologist.Domain.Services.Command;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;
using NeurologistProfile = Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates.Neurologist;

namespace Backend_AuraNeuro.API.Neurologist.Application.Internal.CommandServices;

/// <summary>
/// Implementation of neurologist command handlers.
/// </summary>
public class NeurologistCommandService : INeurologistCommandService
{
    private readonly INeurologistRepository neurologistRepository;
    private readonly IUnitOfWork unitOfWork;

    public NeurologistCommandService(
        INeurologistRepository neurologistRepository,
        IUnitOfWork unitOfWork)
    {
        this.neurologistRepository = neurologistRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<NeurologistProfile?> Handle(CreateNeurologistCommand command)
    {
        var existingByLicense =
            await neurologistRepository.FindByLicenseNumberAsync(command.LicenseNumber);

        if (existingByLicense is not null)
            throw new Exception("Neurologist with this license number already exists.");

        var neurologist = new NeurologistProfile(command);

        try
        {
            await neurologistRepository.AddAsync(neurologist);
            await unitOfWork.CompleteAsync();
            return neurologist;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }

    public async Task<NeurologistProfile?> Handle(UpdateNeurologistProfileCommand command)
    {
        var neurologist = await neurologistRepository.FindByIdAsync(command.NeurologistId);
        if (neurologist is null) return null;

        neurologist.UpdateContact(command.Email, command.Phone);
        neurologist.UpdateAddress(
            command.Street, command.Number, command.City,
            command.PostalCode, command.Country);
        neurologist.UpdateProfessionalInfo(
            neurologist.LicenseNumber.Value,
            command.Specialties,
            command.VerificationStatus);

        neurologistRepository.Update(neurologist);
        await unitOfWork.CompleteAsync();
        return neurologist;
    }

    public async Task<bool> Handle(DeactivateNeurologistCommand command)
    {
        var neurologist = await neurologistRepository.FindByIdAsync(command.NeurologistId);
        if (neurologist is null) return false;

        neurologist.Deactivate();
        neurologistRepository.Update(neurologist);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
