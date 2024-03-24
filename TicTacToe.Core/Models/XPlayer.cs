using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public class XPlayer : IPlayer
{
    private readonly IPlayer _player;

    public XPlayer(IPlayer player)
    {
        if (player.Symbol is not Symbol.X)
            throw new ArgumentException(nameof(player.Symbol));

        _player = player;
    }

    public Symbol Symbol => Symbol.X;

    public ValueTask<Move> GetNextMove(Field field, CancellationToken cancellationToken = default)
    {
        return _player.GetNextMove(field, cancellationToken);
    }
}