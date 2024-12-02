using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers;

[Route("/api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{

    private readonly DevFreelaDbContext _context;
    
    public ProjectsController(DevFreelaDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Get(string search = "", int page = 0, int size = 1)
    {
        var projects = _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Where(p => !p.IsDeleted 
                        && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
            .Skip((page * size))
            .Take(size)
            .ToList();

        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();
        
        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var project = _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Include(p => p.Comments)
            .SingleOrDefault(p => p.Id == id);
        
        var model = ProjectViewModel.FromEntity(project);
        
        return Ok(model);
    }

    [HttpPost]
    public IActionResult Post(CreateProjectInputModel model)
    {
        var project = model.ToEntity();

        _context.Projects.Add(project);
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdateProjectInputModel model)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }
        
        project.Update(model.Title, model.Description, model.TotalCost);
        
        _context.Update(project);
        _context.SaveChanges();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }
        
        project.SetAsDeleted();
        _context.Update(project);
        _context.SaveChanges();
        
        return NoContent();
    }

    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }
        
        project.Start();
        _context.Update(project);
        _context.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPut("{id}/complete")]
    public IActionResult Complete(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }
        
        project.Complete();
        _context.Update(project);
        _context.SaveChanges();
        
        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }

        var comment = new ProjectComment(model.Content, model.IdUser, model.IdProject);
        
        _context.ProjectComments.Add(comment);
        _context.SaveChanges();
        
        return Ok();
    }
    
    
}