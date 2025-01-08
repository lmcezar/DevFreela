using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Queries.Skills.GetAllSkills;

public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillViewModel>>>
{
    private readonly DevFreelaDbContext _context;
    
    public GetAllSkillsHandler(DevFreelaDbContext context) 
    {
        _context = context;
    }
    
    public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills =  _context.Skills
            .Where(s => !s.IsDeleted).ToList();

        var model = skills.Select(SkillViewModel.FromEntity).ToList();
        return ResultViewModel<List<SkillViewModel>>.Success(model);
    }
}