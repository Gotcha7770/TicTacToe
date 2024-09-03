using TicTacToe.Models;

namespace TicTacToe.Interfaces;

public interface IStrategy
{
    Task<Move> GetNextMove(Field field, Symbol symbol, CancellationToken cancellationToken = default);
}