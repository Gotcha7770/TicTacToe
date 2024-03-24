using TicTacToe.Models;

namespace TicTacToe.Interfaces;

public interface IPlayer
{
    Symbol Symbol { get; }

    ValueTask<Move> GetNextMove(Field field, CancellationToken cancellationToken = default);
}