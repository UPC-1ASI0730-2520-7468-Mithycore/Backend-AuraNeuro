namespace Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates;

/// <summary>
/// Audit trail for actions performed on Patient entities.
/// Stores historical records such as creation, updates or deactivation.
/// </summary>
public class PatientAudit
{
    /// <summary>
    /// Primary key for the audit record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The patient this audit entry refers to.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// The action performed (e.g., "Created", "UpdatedContact", "UpdatedAddress", "Deactivated").
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the action occurred.
    /// Automatically set to UtcNow.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Optional notes or metadata describing the action.
    /// </summary>
    public string? Notes { get; set; }
}