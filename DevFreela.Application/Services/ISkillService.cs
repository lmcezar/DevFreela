using DevFreela.Application.Models;

namespace DevFreela.Application.Services;

public interface ISkillService
{
    ResultViewModel GetAll();
    ResultViewModel<int> Insert(CreateSkillInputModel model);
}