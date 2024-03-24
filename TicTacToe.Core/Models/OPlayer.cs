using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public class OPlayer : IPlayer
{
    private readonly IPlayer _player;

    public OPlayer(IPlayer player)
    {
        if (player.Symbol is not Symbol.O)
            throw new ArgumentException(nameof(player.Symbol));

        _player = player;
    }

    public Symbol Symbol => Symbol.O;

    public ValueTask<Move> GetNextMove(Field field, CancellationToken cancellationToken = default)
    {
        return _player.GetNextMove(field, cancellationToken);
    }
}