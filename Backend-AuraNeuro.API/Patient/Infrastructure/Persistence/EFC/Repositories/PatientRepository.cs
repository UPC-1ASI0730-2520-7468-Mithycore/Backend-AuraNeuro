using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_AuraNeuro.API.Patient.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

// Alias de entidades
using PatientEntity = Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates.Patient;
using PatientNeurologist = Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates.PatientNeurologist;

namespace Backend_AuraNeuro.API.Patient.Infrastructure.Persistence.EFC.Repositories
{
    /// <summary>
    /// Patient repository implementation.
    /// </summary>
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<PatientEntity> _patients;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
            _patients = context.Set<PatientEntity>();
        }

        // ===== IBaseRepository<PatientEntity> implementation =====

        public async Task<IEnumerable<PatientEntity>> ListAsync()
        {
            return await _patients.ToListAsync();
        }

        public async Task<PatientEntity?> FindByIdAsync(long id)
        {
            return await _patients.FindAsync(id);
        }

        public async Task AddAsync(PatientEntity entity)
        {
            await _patients.AddAsync(entity);
        }

        public void Update(PatientEntity entity)
        {
            _patients.Update(entity);
        }

        public void Remove(PatientEntity entity)
        {
            _patients.Remove(entity);
        }

        // ===== Custom methods from IPatientRepository =====

        public async Task<PatientEntity?> FindByUserIdAsync(long userId)
        {
            return await _patients.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<PatientEntity?> FindByEmailAsync(string email)
        {
            return await _patients.FirstOrDefaultAsync(p => p.Email.Address == email);
        }

        // -------------------------------------------------------
        //       NUEVOS MÉTODOS PARA PACIENTE–NEURÓLOGO
        // -------------------------------------------------------

        public async Task<bool> AssignNeurologistAsync(long patientId, long neurologistId)
        {
            var exists = await _context.Set<PatientNeurologist>()
                .AnyAsync(r => r.PatientId == patientId && r.NeurologistId == neurologistId);

            if (exists) return true; // Ya asociado

            var relation = new PatientNeurologist
            {
                PatientId = patientId,
                NeurologistId = neurologistId,
                IsActive = true
            };

            await _context.Set<PatientNeurologist>().AddAsync(relation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveNeurologistAsync(long patientId, long neurologistId)
        {
            var relation = await _context.Set<PatientNeurologist>()
                .FirstOrDefaultAsync(r => r.PatientId == patientId && r.NeurologistId == neurologistId);

            if (relation == null)
                return false;

            _context.Set<PatientNeurologist>().Remove(relation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
