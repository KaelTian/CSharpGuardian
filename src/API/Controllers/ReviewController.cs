using CSharpGuardian.Core.Analysis;
using Microsoft.AspNetCore.Mvc;

namespace CSharpGuardian.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly CodeAnalyzer _analyzer;

    public ReviewController(CodeAnalyzer analyzer)
    {
        _analyzer = analyzer;
    }

    [HttpPost]
    public IActionResult ReviewCode([FromBody] ReviewRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
        {
            return BadRequest("Code cannot be empty");
        }

        var result = _analyzer.Analyze(request.Code);
        return Ok(result);
    }
}

public record ReviewRequest(string Code);