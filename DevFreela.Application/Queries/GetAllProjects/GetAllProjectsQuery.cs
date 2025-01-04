using System.Drawing;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
{
    public GetAllProjectsQuery(string search, int size, int page)
    {
        this.Search = search;
        this.Size = size;
        this.Page = page;
    }


    public string? Search { get; set; }
    public int Size { get; set; }
    public int Page { get; set; }
}