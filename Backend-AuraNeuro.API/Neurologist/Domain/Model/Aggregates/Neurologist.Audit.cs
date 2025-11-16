using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates;

/// <summary>
/// Audit fields for neurologist.
/// </summary>
public partial class Neurologist : IEntityWithCreatedUpdatedDate
{
    [Column("created_at")]
    public DateTimeOffset? CreatedDate { get; set; }

    [Column("updated_at")]
    public DateTimeOffset? UpdatedDate { get; set; }
}