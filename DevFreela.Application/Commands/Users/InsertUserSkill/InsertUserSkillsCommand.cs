using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.Users.InsertUser;

public class InsertUserSkillsCommand : IRequest<ResultViewModel>
{
        public int[] SkillIds { get; set; }
        public int Id { get; set; }
    
}
