// src/Core/Analysis/Rules/LongMethodRule.cs
using CSharpGuardian.Core.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpGuardian.Core.Analysis.Rules;

public class LongMethodRule : ICodeInspectionRule
{
    public const string RuleId = "CSG002";
    private const int MaxLines = 20;

    public IEnumerable<CodeIssue> Inspect(SyntaxNode syntaxNode)
    {
        return syntaxNode.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(IsMethodTooLong)
            .Select(CreateIssue);

        bool IsMethodTooLong(MethodDeclarationSyntax method)
        {
            return method.Body?.GetText().Lines.Count > MaxLines ||
                   method.ExpressionBody?.GetText().Lines.Count > MaxLines;
        }

        CodeIssue CreateIssue(MethodDeclarationSyntax method)
        {
            var lineSpan = method.GetLocation().GetLineSpan();
            var lineCount = method.Body?.GetText().Lines.Count
                ?? method.ExpressionBody!.GetText().Lines.Count;

            return new CodeIssue(
                Line: lineSpan.StartLinePosition.Line + 1,
                Message: $"方法过长: {lineCount} 行（最大允许 {MaxLines} 行）",
                Severity: IssueSeverity.Warning,
                RuleId: RuleId
            );
        }
    }
}