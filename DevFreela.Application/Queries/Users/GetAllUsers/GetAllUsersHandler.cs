using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.Users.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<UserViewModel>>>
{
    private readonly DevFreelaDbContext _context;
    
    public GetAllUsersHandler(DevFreelaDbContext context) 
    {
        _context = context;
    }
    
    public async Task<ResultViewModel<List<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        
        var users = await _context.Users
            .Include(u => u.Skills)
            .ThenInclude(u => u.Skill)
            .Where(u => !u.IsDeleted && u.FullName.Contains(request.Search))
             .Skip((request.Page - 1) * request.Size)
             .Take(request.Size)
            .ToListAsync();

        if(users is null)
            return ResultViewModel<List<UserViewModel>>.Error("Projeto não existe");

        var model = users.Select(UserViewModel.FromEntity).ToList();

        return ResultViewModel<List<UserViewModel>>.Success(model);
    }
}