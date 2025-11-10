namespace Backend_AuraNeuro.API.NeurologicalHealth.Interface.REST.Resources;

public record CreateNeuroAssessmentCommandResource(
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
    byte AlertLevel
    );