using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Command;
using Backend_AuraNeuro.API.NeurologicalHealth.Interface.REST.Resources;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Interface.REST.Transform;

public class CreateNeuroAssessmentFromResource
{
    public static CreateNeuroAssessmentCommand ToCommandFromResource(CreateNeuroAssessmentCommandResource neuroAssessmentCommandResource)
    {
        return new CreateNeuroAssessmentCommand(
            neuroAssessmentCommandResource.PatientId,
            neuroAssessmentCommandResource.NeurologistId,
            neuroAssessmentCommandResource.GaitScore,
            neuroAssessmentCommandResource.BalanceScore,
            neuroAssessmentCommandResource.ReflexScore,
            neuroAssessmentCommandResource.CognitionScore,
            neuroAssessmentCommandResource.MemoryScore,
            neuroAssessmentCommandResource.SpeechScore,
            neuroAssessmentCommandResource.TremorScore,
            neuroAssessmentCommandResource.StrengthScore,
            neuroAssessmentCommandResource.CoordinationScore,
            neuroAssessmentCommandResource.SensoryScore,
            neuroAssessmentCommandResource.EegSummary,
            neuroAssessmentCommandResource.NeurologistNotes,
            neuroAssessmentCommandResource.IsFlagged,
            neuroAssessmentCommandResource.AlertLevel
            );
    }
}