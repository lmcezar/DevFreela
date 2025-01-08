using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsHandler() : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    private readonly DevFreelaDbContext _context;
    
    public GetAllProjectsHandler(DevFreelaDbContext context) : this()
    {
        _context = context;
    }
    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var pagination = new Pagination(request.Search, request.Size, request.Page);
        var projects = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Where(p => !p.IsDeleted 
                        && (pagination.Search == "" || p.Title.Contains(pagination.Search) || p.Description.Contains(pagination.Search)))
            .Skip((pagination.Page * pagination.Size))
            .Take(pagination.Size)
            .ToListAsync();

        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();
        
        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }

    
}