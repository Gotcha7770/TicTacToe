using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public class XPlayer : IPlayer
{
    private readonly IStrategy _strategy;

    public XPlayer(IStrategy strategy)
    {
        _strategy = strategy;
    }

    public Symbol Symbol => Symbol.X;

    public ValueTask<Move> GetNextMove(Field field, CancellationToken cancellationToken = default)
    {
        return _strategy.GetNextMove(field, Symbol, cancellationToken);
    }
}