using DevFreela.Application.Commands.Skills.InsertSkill;
using DevFreela.Application.Queries.Skills.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("/api/skills")]
[ApiController]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllSkillsQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(InsertSkillCommand command)
    {
        var result = await _mediator.Send(command);

        return NoContent();
    }
}