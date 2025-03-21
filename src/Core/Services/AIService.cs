// src/Core/Services/AIService.cs
using CSharpGuardian.Core.Analysis;
using CSharpGuardian.Core.Models;

namespace CSharpGuardian.Core.Services;

public class AIService
{
    private const string DefaultSuggestion = "No AI suggestions available";
    
    public string GenerateSuggestion(IEnumerable<CodeIssue> issues)
    {
        if (!issues.Any()) return "Code looks clean!";
        
        // TODO: 集成实际AI模型
        // 此处为模拟实现
        var topIssues = issues
            .GroupBy(i => i.RuleId)
            .OrderByDescending(g => g.Count())
            .Take(3);
        
        var suggestion = "建议优先修复以下问题：\n";
        foreach (var group in topIssues)
        {
            suggestion += $"- {group.First().Message} (共{group.Count()}处)\n";
        }
        
        return string.IsNullOrWhiteSpace(suggestion) 
            ? DefaultSuggestion 
            : suggestion;
    }
}