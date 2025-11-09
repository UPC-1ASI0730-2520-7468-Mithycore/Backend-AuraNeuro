namespace Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Command;

public record CreateNeuroAssessmentCommand(
    long PatientId,
    long NeurologistId,
    short GaitScore,
    short BalanceScore,
    short ReflexScore,
    short CognitionScore,
    short MemoryScore,
    short SpeechScore,
    short TremorScore,
    short StrengthScore,
    short CoordinationScore,
    short SensoryScore,
    string EegSummary,
    string NeurologistNotes,
    bool IsFlagged,
    byte? AlertLevel
    );