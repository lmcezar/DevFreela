using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertComment;

public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;
    
    public InsertCommentHandler(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto inexistente");
        }

        var comment = new ProjectComment(request.Content, request.IdUser, request.IdProject);
        
        await _context.ProjectComments.AddAsync(comment);
        await _context.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}