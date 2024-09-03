using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public class OPlayer : IPlayer
{
    private readonly IStrategy _strategy;

    public OPlayer(IStrategy strategy)
    {
        _strategy = strategy;
    }

    public Symbol Symbol => Symbol.O;

    public Task<Move> GetNextMove(Field field, CancellationToken cancellationToken = default)
    {
        return _strategy.GetNextMove(field, Symbol, cancellationToken);
    }
}