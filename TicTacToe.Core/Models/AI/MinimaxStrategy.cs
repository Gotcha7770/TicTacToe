using TicTacToe.Interfaces;

namespace TicTacToe.Models.AI;

// https://habr.com/ru/articles/329058/
// https://thecode.media/tic-tac-toe/
public class MinimaxStrategy : IAIStrategy
{
    public static TimeSpan Timeout { get; set; } = TimeSpan.Zero;

    public async ValueTask<Move> GetNextMove(Field field, Symbol symbol, CancellationToken cancellationToken = default)
    {
        await Task.Delay(Timeout, cancellationToken);
        var bestScore = int.MinValue;
        Move bestMove = null;

        foreach (var move in Game.GetAvailableMoves(field, symbol))
        {
            int score = field.Scope(move, f => Minimax(f, symbol, symbol));
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        return bestMove;
    }
    
    private int GetScore(Symbol symbol, Symbol winner, int depth) => winner == symbol ? 10 - depth : depth - 10;
    
    private int Minimax(Field field, Symbol symbol, Symbol currentPlayer, byte depth = 0)
    {
        var maybe = field.GetWinner();
        if (maybe is { } winner)
            return winner == currentPlayer ? 10 - depth : depth - 10;
    
        if (Game.IsDraw(field))
            return 0;

        symbol = symbol.Reverse();
        depth++;

        if (symbol == currentPlayer)
        {
            return Game.GetAvailableMoves(field, symbol)
                .Select(x => field.Scope(x, f => Minimax(f, symbol, currentPlayer, depth)))
                .Max();
        }

        return Game.GetAvailableMoves(field, symbol)
            .Select(x => field.Scope(x, f => Minimax(f, symbol, currentPlayer, depth)))
            .Min();
    }
}