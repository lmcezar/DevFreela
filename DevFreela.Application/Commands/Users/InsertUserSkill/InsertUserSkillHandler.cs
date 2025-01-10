using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Users.InsertUser;

public class InsertUserSkillHandler : IRequestHandler<InsertUserSkillsCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;
    public InsertUserSkillHandler(DevFreelaDbContext context)
    {
        _context = context;
    }
    
    
    
    // public async Task<ResultViewModel<int>> Handle(InsertUserSkillsCommand request, CancellationToken cancellationToken)
    // {
    //     // var user = request.ToEntity();
    //     //
    //     // await _context.Users.AddAsync(user);
    //     // await _context.SaveChangesAsync();
    //     var result = request.ToEntity();
    //     
    //     await _context.Users.AddAsync(result);
    //     await _context.SaveChangesAsync();
    //     
    //
    //     //await _context.Users.SingleOrDefaultAsync(p => p.Id == request.IdUser);
    //
    //     return ResultViewModel<int>.Success(result.Id);
    //     
    // }
    
    public async Task<ResultViewModel> Handle(InsertUserSkillsCommand request, CancellationToken cancellationToken)
    {
        var userSkills = request.SkillIds.Select(s => new UserSkill(request.Id, s)).ToList();

        _context.UserSkills.AddRange(userSkills);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}