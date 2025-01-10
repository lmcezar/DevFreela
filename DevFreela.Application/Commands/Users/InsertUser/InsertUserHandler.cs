using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.Users.InsertUser;

public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;
    
    public InsertUserHandler(DevFreelaDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return ResultViewModel<int>.Success(user.Id);
    }
}