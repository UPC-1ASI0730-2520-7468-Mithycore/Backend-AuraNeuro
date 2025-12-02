using Backend_AuraNeuro.API.IAM.Application.Internal.OutboundServices;
using Backend_AuraNeuro.API.IAM.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.IAM.Domain.Model.Commands;
using Backend_AuraNeuro.API.IAM.Domain.Repositories;
using Backend_AuraNeuro.API.IAM.Domain.Services;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Command;
using Backend_AuraNeuro.API.Neurologist.Domain.Commands;
using Backend_AuraNeuro.API.Neurologist.Domain.Repositories;
using Backend_AuraNeuro.API.Patient.Domain.Command;
using Backend_AuraNeuro.API.Patient.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

namespace Backend_AuraNeuro.API.IAM.Application.Internal.CommandServices;

/// <summary>
///     Handles user-related commands such as sign-in and sign-up.
/// </summary>
public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IPatientRepository patientRepository,
    INeurologistRepository neurologistRepository
    )
    : IUserCommandService
{
    /// <summary>
    ///     Authenticate a user using the provided credentials.
    /// </summary>
    /// <param name="command">The sign-in command containing username and password.</param>
    /// <returns>A tuple with the authenticated <see cref="User" /> and the generated Token.</returns>
    /// <exception cref="Exception">Thrown when credentials are invalid.</exception>
    public async Task<(long id, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        if (command.Role == "patient")
        {
            var patientFound = await patientRepository.FindByUserIdAsync(user.Id);

            if (patientFound == null)
            {
                throw new Exception("Patient not found");
            }

            var token = tokenService.GenerateTokenPatient(patientFound);

            return (patientFound.Id, token);
        }

        if (command.Role == "neurologist")
        {
            var neurologistFound = await neurologistRepository.FindByUserIdAsync(user.Id);

            if (neurologistFound == null)
            {
                throw new Exception("Neurologist not found");
            }

            var token = tokenService.GenerateTokenNeurologist(neurologistFound);
            return (neurologistFound.Id, token);
        }
        
        throw new Exception("Unknown role");
    }

    /// <summary>
    ///     Create a new user account.
    /// </summary>
    /// <param name="command">The sign-up command with username and password.</param>
    /// <returns>A completed <see cref="Task" /> when the operation succeeds.</returns>
    /// <exception cref="Exception">Thrown when the username is already taken or creation fails.</exception>
    public async Task<(long id, string token)> Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword, command.Role);
        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();
        
        try
        {
            if (command.Role == "patient")
            {
                var patientCommand = new CreatePatientCommand(
                    user.Id,
                    command.FirstName,
                    command.LastName,
                    command.Email,
                    command.PhoneNumber,
                    command.BirthDate,
                    command.Street,
                    command.City,
                    command.Country
                    );
                var patient = new Patient.Domain.Model.Aggregates.Patient(patientCommand);
                await patientRepository.AddAsync(patient);
                await unitOfWork.CompleteAsync();
                var token = tokenService.GenerateTokenPatient(patient);
                return (patient.Id, token);
            }

            if (command.Role == "neurologist")
            {
                var neurologistCommand = new CreateNeurologistCommand(
                    user.Id,
                    command.FirstName,
                    command.LastName,
                    command.LicenseNumber,
                    command.Email,
                    command.PhoneNumber,
                    command.Street,
                    command.Number,
                    command.City,
                    command.PostalCode,
                    command.Country,
                    command.Specialties
                    );
                var neurologist =  new Neurologist.Domain.Model.Aggregates.Neurologist(neurologistCommand);
                await neurologistRepository.AddAsync(neurologist);
                await unitOfWork.CompleteAsync();
                var token = tokenService.GenerateTokenNeurologist(neurologist);
                return (neurologist.Id, token);
            }
            throw new Exception("Unknown role");
        }
        catch (Exception e)
        {
            throw;
        }
    }
}