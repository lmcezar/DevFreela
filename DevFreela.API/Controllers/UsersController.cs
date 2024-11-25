using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Post(CreateUserInputModel model)
    {
        return Ok();
    }

    [HttpPut("{id}/skills")]
    public IActionResult PostSkills(UserSkillsInputModel model)
    {
        return NoContent();
    }

    [HttpPut("{id}/profile-picture")]
    public IActionResult UploadProfilePicture(IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";
        return Ok(description);
    }
    
}