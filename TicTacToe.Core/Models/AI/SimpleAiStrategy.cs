using TicTacToe.Interfaces;

namespace TicTacToe.Models.AI;

public class SimpleAiStrategy : IAIStrategy
{
    private static readonly Random Random = new();
    public static TimeSpan Timeout { get; set; } = TimeSpan.Zero;

    public async ValueTask<Move> GetNextMove(Field field, Symbol symbol, CancellationToken cancellationToken = default)
    {
        await Task.Delay(Timeout, cancellationToken);
        var variants = field.GetEmptyCells().ToArray();
        int randomIndex = Random.Next(variants.Length);
        var randomCell = variants[randomIndex];

        return new Move(randomCell, symbol);
    }

    public static XPlayer FromX() => new(new SimpleAiStrategy());

    public static OPlayer FromO() => new OPlayer(new SimpleAiStrategy());
}