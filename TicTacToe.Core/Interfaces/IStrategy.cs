using TicTacToe.Models;

namespace TicTacToe.Interfaces;

public interface IStrategy
{
    ValueTask<Move> GetNextMove(Field field, Symbol symbol, CancellationToken cancellationToken = default);
}