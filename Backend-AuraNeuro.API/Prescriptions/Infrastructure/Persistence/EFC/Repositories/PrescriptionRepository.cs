// Summary: EF Core implementation of IPrescriptionRepository.
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Backend_AuraNeuro.API.Prescriptions.Infrastructure.Persistence.EFC.Repositories;

using PrescriptionAggregate = Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates.Prescription;

public class PrescriptionRepository : BaseRepository<PrescriptionAggregate>, IPrescriptionRepository
{
    private readonly AppDbContext _context;

    public PrescriptionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PrescriptionAggregate>> FindByPatientIdAsync(Guid patientId)
        => await _context.Set<PrescriptionAggregate>()
            .Where(p => p.PatientId == patientId)
            .ToListAsync();

    public async Task<IEnumerable<PrescriptionAggregate>> FindByNeurologistIdAsync(Guid neurologistId)
        => await _context.Set<PrescriptionAggregate>()
            .Where(p => p.NeurologistId == neurologistId)
            .ToListAsync();
}