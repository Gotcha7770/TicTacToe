using TicTacToe.Interfaces;

namespace TicTacToe.Models.AI;

public class SimpleAiPlayer : IPlayer
{
    private static readonly Random Random = new Random();
    public static TimeSpan Timeout { get; set; } = TimeSpan.Zero;

    public SimpleAiPlayer(Symbol symbol)
    {
        Symbol = symbol;
    }

    public Symbol Symbol { get; }

    public async ValueTask<Move> GetNextMove(Field field, CancellationToken cancellationToken = default)
    {
        await Task.Delay(Timeout, cancellationToken);
        var variants = field.GetEmptyCells().ToArray();
        int randomIndex = Random.Next(variants.Length);
        var randomCell = variants[randomIndex];

        return new Move(randomCell, Symbol);
    }

    public static XPlayer FromX() => new SimpleAiPlayer(Symbol.X).AsXPlayer();

    public static OPlayer FromO() => new SimpleAiPlayer(Symbol.O).AsOPlayer();
}