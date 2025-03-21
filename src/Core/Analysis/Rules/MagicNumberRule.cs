// src/Core/Analysis/Rules/MagicNumberRule.cs
using CSharpGuardian.Core.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpGuardian.Core.Analysis.Rules;

public class MagicNumberRule : ICodeInspectionRule
{
    public const string RuleId = "CSG001";
    
    public IEnumerable<CodeIssue> Inspect(SyntaxNode syntaxNode)
    {
        var allowedNumbers = new HashSet<object> { 0, 1, -1 };
        
        return syntaxNode.DescendantNodes()
            .OfType<LiteralExpressionSyntax>()
            .Where(IsMagicNumber)
            .Select(literal => CreateIssue(literal));

        bool IsMagicNumber(LiteralExpressionSyntax literal)
        {
            return literal.Kind() == SyntaxKind.NumericLiteralExpression &&
                   !allowedNumbers.Contains(literal.Token.Value!);
        }

        CodeIssue CreateIssue(LiteralExpressionSyntax literal)
        {
            var lineSpan = literal.GetLocation().GetLineSpan();
            return new CodeIssue(
                Line: lineSpan.StartLinePosition.Line + 1,
                Message: $"发现魔术数字: {literal.Token.Value}",
                Severity: IssueSeverity.Warning,
                RuleId: RuleId
            );
        }
    }
}