using System.Reactive.Linq;
using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public class HumanStrategy : IStrategy
{
    private readonly IObservable<Move> _moves;

    public HumanStrategy(IObservable<Move> moves)
    {
        _moves = moves;
    }

    public Task<Move> GetNextMove(Field field, Symbol symbol, CancellationToken cancellationToken)
    {
        return _moves.Where(x => x.Symbol == symbol).NextAsync(cancellationToken).AsTask();
    }
}