using TicTacToe.Interfaces;

namespace TicTacToe.Models.AI;

// https://habr.com/ru/articles/329058/
// https://thecode.media/tic-tac-toe/
public class MinimaxPlayer : IPlayer
{
    public static TimeSpan Timeout { get; set; } = TimeSpan.Zero;

    public MinimaxPlayer(Symbol symbol)
    {
        Symbol = symbol;
    }

    public Symbol Symbol { get; }

    public async ValueTask<Move> GetNextMove(Field field, CancellationToken cancellationToken = default)
    {
        await Task.Delay(Timeout, cancellationToken);
        var bestScore = int.MinValue;
        Move bestMove = null;

        foreach (var move in field.GetEmptyCells().Select(x => new Move(x, Symbol)))
        {
            int score = field.Scope(move, f => Minimax(f, Symbol, isMaximizing: false));
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        return bestMove;
    }
    
    private int Minimax(Field field, Symbol symbol, byte depth = 0, bool isMaximizing = true)
    {
        var winner = field.GetWinner();
        if (winner.HasValue)
            return winner == Symbol ? 10 - depth : depth - 10;
    
        if (Game.IsDraw(field))
            return 0;

        symbol = symbol.Reverse();
        ++depth;
        if (isMaximizing)
            return Game.GetAvailableMoves(field, symbol)
                .Select(x => field.Scope(x, f => Minimax(f, symbol, depth, false)))
                .Max();

        return Game.GetAvailableMoves(field, symbol)
            .Select(x => field.Scope(x, f => Minimax(f, symbol, depth)))
            .Min();
    }
}