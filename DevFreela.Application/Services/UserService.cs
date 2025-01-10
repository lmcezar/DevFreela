using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;

public class UserService : IUserService
{
    private readonly DevFreelaDbContext _context;
    
    public UserService(DevFreelaDbContext context)
    {
        _context = context;
    }
    
    public ResultViewModel<UserViewModel> GetById(int id)
    {
        var user = _context.Users
            .Include(u => u.Skills)
            .ThenInclude(u => u.Skill)
            .SingleOrDefault(u => u.Id == id);
        
        var model = UserViewModel.FromEntity(user);
        
        return ResultViewModel<UserViewModel>.Success(model);
        
    }

    public ResultViewModel<int> Insert(CreateUserInputModel model)
    {
        var user = new User(model.FullName, model.Email, model.BirthDate);
        
        _context.Users.Add(user);
        _context.SaveChanges();
        
        return ResultViewModel<int>.Success(user.Id);

    }

    public ResultViewModel InsertUserSkills(int id, UserSkillsInputModel model)
    {
        var userSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();

        _context.UserSkills.AddRange(userSkills);
        _context.SaveChanges();

        return ResultViewModel.Success();
    }
}