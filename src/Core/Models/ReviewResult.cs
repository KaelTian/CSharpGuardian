// src/Core/Models/ReviewResult.cs
namespace CSharpGuardian.Core.Models;

public record ReviewResult(
    IEnumerable<CodeIssue> Issues,
    string AiSuggestion = "",
    MetricsData? Metrics = null
);

public record CodeIssue(
    int Line,
    string Message,
    IssueSeverity Severity,
    string RuleId
);

public enum IssueSeverity
{
    Information,
    Warning,
    Error
}

public record MetricsData(
    double AnalysisDurationMs,
    int TotalRulesExecuted
);