using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Infrastructure.Data.TypeConfigurations
{
    public class AnalysisTypeConfiguration : IEntityTypeConfiguration<Analysis>
    {
        public void Configure(EntityTypeBuilder<Analysis> builder)
        {
            builder.ToTable("Analyses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProjectKey)
                   .HasMaxLength(512)
                   .IsRequired();

            builder.Property(x => x.AnalysisDate)
                   .IsRequired();

            builder.Property(x => x.CognitiveComplexity)
                   .IsRequired();

            builder.Property(x => x.CyclomaticComplexity)
                   .IsRequired();

            builder.Property(x => x.ReliabilityRating)
                   .IsRequired();

            builder.Property(x => x.Bugs)
                   .IsRequired();

            builder.Property(x => x.Vulnerabilities)
                   .IsRequired();

            builder.Property(x => x.SecurityRating)
                   .IsRequired();

            builder.Property(x => x.CodeSmells)
                   .IsRequired();

            builder.Property(x => x.Coverage)
                   .HasColumnType("money")
                   .IsRequired();

            builder.Property(x => x.NonCommentingLinesOfCode)
                   .IsRequired();

            builder.Property(x => x.DuplicatedLinesDensity)
                   .HasColumnType("money")
                   .IsRequired();

            builder.Property(x => x.OpenIssues)
                   .IsRequired();

            builder.Property(x => x.MaintainabilityRating)
                   .IsRequired();

            builder.Property(x => x.EffortToFixTechnicalDebt)
                   .HasColumnType("money")
                   .IsRequired();

            builder.Property(x => x.QualityGate)
                   .HasMaxLength(64);
        }
    }
}
