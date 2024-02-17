using TicTacToe.Models;

namespace TicTacToe.Interfaces;

public interface IPlayer
{
    Symbol Symbol { get; }

    Task<Move> GetNextMove(Field field, CancellationToken cancellationToken);
}