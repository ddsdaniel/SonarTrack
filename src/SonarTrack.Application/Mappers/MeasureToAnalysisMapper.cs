using SonarTrack.Application.Abstractions.Mappers;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Application.Enums;
using SonarTrack.Application.Extensions;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Application.Mappers
{
    public class MeasureToAnalysisMapper : IMeasureToAnalysisMapper
    {
        public void Map(IEnumerable<MeasureDto> measures, IEnumerable<Analysis> analyses)
        {
            foreach (var measure in measures)
            {
                var analysis = analyses.FirstOrDefault(a => a.ProjectKey == measure.Component);
                if (analysis != null)
                {
                    SetMetric(measure, analysis);
                }
            }
        }

        private void SetMetric(MeasureDto measure, Analysis analysis)
        {
            if (measure.Metric.TryGetEnum(out MetricKey metricKey))
            {
                switch (metricKey)
                {
                    case MetricKey.alert_status:
                        break;
                    case MetricKey.bugs:
                        analysis.Bugs = int.Parse(measure.Value);
                        break;
                    case MetricKey.reliability_rating:
                        break;
                    case MetricKey.vulnerabilities:
                        analysis.Vulnerabilities = int.Parse(measure.Value);
                        break;
                    case MetricKey.security_rating:
                        analysis.SecurityRating = measure.Value[0];
                        break;
                    case MetricKey.security_hotspots_reviewed:
                        break;
                    case MetricKey.security_review_rating:
                        break;
                    case MetricKey.code_smells:
                        analysis.CodeSmells = int.Parse(measure.Value);
                        break;
                    case MetricKey.sqale_rating:
                        break;
                    case MetricKey.duplicated_lines_density:
                        analysis.DuplicatedLinesDensity = decimal.Parse(measure.Value);
                        break;
                    case MetricKey.coverage:
                        analysis.Coverage = decimal.Parse(measure.Value);
                        break;
                    case MetricKey.ncloc:
                        analysis.NonCommentingLinesOfCode = int.Parse(measure.Value);
                        break;
                    case MetricKey.ncloc_language_distribution:
                        break;
                    case MetricKey.maintainability:
                        analysis.MaintainabilityRating = measure.Value[0];
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
