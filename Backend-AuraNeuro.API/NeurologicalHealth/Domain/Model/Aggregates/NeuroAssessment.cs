using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Command;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;

public class NeuroAssessment
{
    public long Id;
    public long? PatientId { get; set; }
    public long? NeurologistId { get; set; }
    //Métricas cuantitativas (rango recomendado 0–100 o unidades explícitas)
    public short? GaitScore { get; set; }
    public short? BalanceScore { get; set; }
    public short? ReflexScore { get; set; }
    public short? CognitionScore { get; set; }
    public short? MemoryScore { get; set; }
    public short? SpeechScore { get; set; }
    public short? TremorScore { get; set; }
    public short? StrengthScore { get; set; }
    public short? CoordinationScore { get; set; }
    public short? SensoryScore{ get; set; }
    //Texto y metadatos clínicos
    public string? EegSummary { get; set; }
    public string? NeurologistNotes { get; set; }
    //Flags / alertas
    public bool? IsFlagged { get; set; }
    public byte? AlertLevel { get; set; }
    
    protected NeuroAssessment() {}

    public NeuroAssessment(CreateNeuroAssessmentCommand neuroAssessmentCommand)
    {
        this.PatientId = neuroAssessmentCommand.PatientId;
        this.NeurologistId = neuroAssessmentCommand.NeurologistId;
        this.GaitScore = neuroAssessmentCommand.GaitScore;
        this.BalanceScore = neuroAssessmentCommand.BalanceScore;
        this.ReflexScore = neuroAssessmentCommand.ReflexScore;
        this.CognitionScore = neuroAssessmentCommand.CognitionScore;
        this.MemoryScore = neuroAssessmentCommand.MemoryScore;
        this.SpeechScore = neuroAssessmentCommand.SpeechScore;
        this.TremorScore = neuroAssessmentCommand.TremorScore;
        this.StrengthScore = neuroAssessmentCommand.StrengthScore;
        this.CoordinationScore = neuroAssessmentCommand.CoordinationScore;
        this.SensoryScore = neuroAssessmentCommand.SensoryScore;
        this.EegSummary = neuroAssessmentCommand.EegSummary;
        this.NeurologistNotes = neuroAssessmentCommand.NeurologistNotes;
        this.IsFlagged = neuroAssessmentCommand.IsFlagged;
        this.AlertLevel = neuroAssessmentCommand.AlertLevel;
    }

}