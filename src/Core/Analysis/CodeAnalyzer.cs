// src/Core/Analysis/CodeAnalyzer.cs
using CSharpGuardian.Core.Analysis.Rules;
using CSharpGuardian.Core.Models;
using CSharpGuardian.Core.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpGuardian.Core.Analysis;

public class CodeAnalyzer
{
    private readonly List<ICodeInspectionRule> _rules;
    private readonly AIService _aiService;

    public CodeAnalyzer(
        IEnumerable<ICodeInspectionRule> rules,
        AIService aiService)
    {
        _rules = rules.ToList();
        _aiService = aiService;
    }

    public ReviewResult Analyze(string code)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        // 解析代码语法树
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var root = syntaxTree.GetRoot();
        
        // 并行执行所有规则检查
        var issues = _rules
            .AsParallel()
            .SelectMany(rule => rule.Inspect(root))
            .ToList();

        // 生成AI建议
        var aiSuggestion = _aiService.GenerateSuggestion(issues);
        
        watch.Stop();
        
        return new ReviewResult(
            Issues: issues,
            AiSuggestion: aiSuggestion,
            Metrics: new MetricsData(
                AnalysisDurationMs: watch.Elapsed.TotalMilliseconds,
                TotalRulesExecuted: _rules.Count
            )
        );
    }
}