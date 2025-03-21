// src/Core/Analysis/Rules/ICodeInspectionRule.cs
using CSharpGuardian.Core.Models;
using Microsoft.CodeAnalysis;


namespace CSharpGuardian.Core.Analysis.Rules;

public interface ICodeInspectionRule
{
    /// <summary>
    /// 分析语法节点并返回代码问题
    /// </summary>
    /// <param name="syntaxNode">待分析的语法树节点</param>
    /// <returns>检测到的问题集合</returns>
    IEnumerable<CodeIssue> Inspect(SyntaxNode syntaxNode);
}