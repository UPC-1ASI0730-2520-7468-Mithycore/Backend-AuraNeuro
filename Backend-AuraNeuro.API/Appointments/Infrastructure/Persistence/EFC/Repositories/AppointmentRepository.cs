namespace Backend_AuraNeuro.API.Appointments.Infrastructure.Persistence.EFC.Repositories;

using Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Appointments.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext context;
    private readonly DbSet<Appointment> set;

    public AppointmentRepository(AppDbContext context)
    {
        this.context = context;
        this.set = context.Set<Appointment>();
    }

    public async Task<IEnumerable<Appointment>> ListAsync()
        => await set.ToListAsync();

    public async Task<Appointment?> FindByIdAsync(long id)
        => await set.FindAsync(id);

    public async Task AddAsync(Appointment entity)
        => await set.AddAsync(entity);

    public void Update(Appointment entity)
        => set.Update(entity);

    public void Remove(Appointment entity)
        => set.Remove(entity);

    public async Task<IEnumerable<Appointment>> FindByNeurologistIdAsync(long neurologistId)
        => await set.Where(a => a.NeurologistId == neurologistId).ToListAsync();
}