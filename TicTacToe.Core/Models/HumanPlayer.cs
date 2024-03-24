using System.Reactive.Linq;
using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public class HumanPlayer : IPlayer
{
    private readonly IObservable<Move> _moves;

    public HumanPlayer(Symbol symbol, IObservable<Move> moves)
    {
        _moves = moves;
        Symbol = symbol;
    }

    public Symbol Symbol { get; }

    public ValueTask<Move> GetNextMove(Field field, CancellationToken cancellationToken)
    {
        return _moves.Where(x => x.Symbol == Symbol)
            .NextAsync(cancellationToken);
    }
}