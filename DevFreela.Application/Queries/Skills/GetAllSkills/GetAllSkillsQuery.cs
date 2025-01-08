using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.Skills.GetAllSkills;

public class GetAllSkillsQuery :  IRequest<ResultViewModel<List<SkillViewModel>>>
{
    
}