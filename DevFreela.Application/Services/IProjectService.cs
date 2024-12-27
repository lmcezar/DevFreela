using DevFreela.Application.Models;


namespace DevFreela.Application.Services;

public interface IProjectService
{
    ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "");
    ResultViewModel<ProjectViewModel> GetById(int id);
    ResultViewModel<int> Insert(CreateProjectInputModel model);
    ResultViewModel Update(UpdateProjectInputModel model);
    ResultViewModel Delete(int id);
    ResultViewModel Start(int id);
    ResultViewModel Complete(int id);
    ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model);
}