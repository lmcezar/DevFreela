using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.Users.InsertUser;

public class InsertUserCommand : IRequest<ResultViewModel>
{
    public InsertUserCommand(int id, string fullName, string email, DateTime birthDate)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    
    public User ToEntity() => new(FullName, Email, BirthDate);

}