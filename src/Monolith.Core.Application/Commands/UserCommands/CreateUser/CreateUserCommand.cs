using Monolith.Core.Application.Abstractions.Commands;

namespace Monolith.Core.Application.Commands.UserCommands.CreateUser;

public sealed record class CreateUserCommand : ICommand
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActivated { get; set; }
}
