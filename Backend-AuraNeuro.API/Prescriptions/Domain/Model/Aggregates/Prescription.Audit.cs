// Summary: Audit fields for Prescription aggregate, mapped as CreatedAt / UpdatedAt.
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates;

public partial class Prescription : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")]
    public DateTimeOffset? CreatedDate { get; set; }

    [Column("UpdatedAt")]
    public DateTimeOffset? UpdatedDate { get; set; }
}