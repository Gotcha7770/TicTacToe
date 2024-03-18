using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public class HumanPlayer : IPlayer
{
    public Symbol Symbol { get; }
    public Task<Move> GetNextMove(Field field, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}