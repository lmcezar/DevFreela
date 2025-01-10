using DevFreela.Application.Commands.Users.InsertUser;
using DevFreela.Application.Queries.Users.GetAllUsers;
using DevFreela.Application.Queries.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll(string search, int size, int page)
    {
        var result = await _mediator.Send(new GetAllUsersQuery(search, size, page));

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));


        if (!result.IsSuccess) return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(InsertUserCommand command)
    {
        //var user = new User(command.FullName, command.Email, command.BirthDate);

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = command.FullName }, command);

    }

    [HttpPost("{id}/skills")]
    public async Task<IActionResult> PostSkills(int id, InsertUserSkillsCommand command)
    {
        var result = await _mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{id}/profile-picture")]
    public IActionResult UploadProfilePicture(int id, IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";
        return Ok(description);
    }
}