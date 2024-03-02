using MagicHat.CQRS.Commands;

namespace OnlineShop.CQRS.Commands;

public sealed record FirstCommand(string Value1) : ICommand
{
}
