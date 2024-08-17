using SonarTrack.Domain.Abstractions.Domain;

namespace SonarTrack.Domain.Entities
{
    public class Analysis : Entity
    {
        public string ProjectKey { get; set; } = string.Empty;
        public DateTime AnalysisDate { get; set; }
        public int CognitiveComplexity { get; set; }
        public int CyclomaticComplexity { get; set; }
        public char ReliabilityRating { get; set; }
        public int Bugs { get; set; }
        public int Vulnerabilities { get; set; }
        public char SecurityRating { get; set; }
        public int CodeSmells { get; set; }
        public decimal Coverage { get; set; }
        public int NonCommentingLinesOfCode { get; set; }
        public decimal DuplicatedLinesDensity { get; set; }
        public int OpenIssues { get; set; }
        public char MaintainabilityRating { get; set; }
        public decimal EffortToFixTechnicalDebt { get; set; }
        public string? QualityGate { get; set; }
    }
}
