namespace TicTacToe.Models.AI;

// https://habr.com/ru/articles/329058/
// https://thecode.media/tic-tac-toe/
public static class MinimaxPlayer
{
    public static XPlayer FromX() => new XPlayer(f => GetNextMove(f, Symbol.X));
    public static OPlayer FromO() => new OPlayer(f => GetNextMove(f, Symbol.O));

    private static Task<Cell> GetNextMove(Field field, Symbol symbol)
    {
        var bestScore = int.MinValue;
        Move bestMove = null;

        foreach (var move in field.GetEmptyCells().Select(x => new Move(x, symbol)))
        {
            int score = field.Scope(move, f => MinimaxRecursive(f, symbol, isMaximizing: false));
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        return Task.FromResult(bestMove.Cell);
    }
    
    private static int MinimaxRecursive(Field field, Symbol symbol, byte depth = 0, bool isMaximizing = true)
    {
        var winner = Game.GetWinner(field);
        if (winner.HasValue)
            return winner == symbol ? 10 - depth : depth - 10;
    
        if (Game.IsDraw(field))
            return 0;

        if (isMaximizing)
            return Game.GetAvailableMoves(field, Symbol.X)
                .Select(x => field.Scope(x, f => MinimaxRecursive(f, symbol, ++depth, false)))
                .Max();
    
        return Game.GetAvailableMoves(field, Symbol.O)
            .Select(x => field.Scope(x, f => MinimaxRecursive(f, symbol, ++depth)))
            .Min();
    }
}