using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SonarTrack.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectKey = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    AnalysisDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CognitiveComplexity = table.Column<int>(type: "int", nullable: false),
                    CyclomaticComplexity = table.Column<int>(type: "int", nullable: false),
                    ReliabilityRating = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Bugs = table.Column<int>(type: "int", nullable: false),
                    Vulnerabilities = table.Column<int>(type: "int", nullable: false),
                    SecurityRating = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    CodeSmells = table.Column<int>(type: "int", nullable: false),
                    Coverage = table.Column<decimal>(type: "money", nullable: false),
                    NonCommentingLinesOfCode = table.Column<int>(type: "int", nullable: false),
                    DuplicatedLinesDensity = table.Column<decimal>(type: "money", nullable: false),
                    OpenIssues = table.Column<int>(type: "int", nullable: false),
                    MaintainabilityRating = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    EffortToFixTechnicalDebt = table.Column<decimal>(type: "money", nullable: false),
                    QualityGate = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");
        }
    }
}
