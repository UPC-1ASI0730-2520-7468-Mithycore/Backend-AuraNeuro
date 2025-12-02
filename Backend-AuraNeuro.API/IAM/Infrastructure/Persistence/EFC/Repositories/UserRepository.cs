using Backend_AuraNeuro.API.IAM.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.IAM.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Backend_AuraNeuro.API.IAM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework Core repository for <see cref="User" /> aggregates.
/// </summary>
/// <remarks>
///     Provides common user-specific queries on top of <see cref="BaseRepository{TEntity}" />.
/// </remarks>
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    /// <summary>
    ///     Finds a user by username asynchronously.
    /// </summary>
    /// <param name="username">The username to search.</param>
    /// <returns>The matching <see cref="User" /> or <c>null</c> if none found.</returns>
    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    /// <summary>
    ///     Checks whether a user exists with the given username.
    /// </summary>
    /// <param name="username">The username to search.</param>
    /// <returns><c>true</c> if a user with the username exists; otherwise <c>false</c>.</returns>
    public bool ExistsByUsername(string username)
    {
        return context.Set<User>().Any(user => user.Username.Equals(username));
    }
}