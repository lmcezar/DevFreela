using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.Users.GetAllUsers;

public class GetAllUsersQuery : Pagination, IRequest<ResultViewModel<List<UserViewModel>>>
{
    public GetAllUsersQuery(string search, int size, int page) : base(search, size, page)
    {
    }
}