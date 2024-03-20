namespace TicTacToe.Models.AI;

public static class SimpleAiPlayer
{
    private static readonly Random Random = new Random();
    public static TimeSpan Timeout { get; set; } = TimeSpan.Zero;

    public static XPlayer FromX() => new XPlayer(GetNextMove);
    public static OPlayer FromO() => new OPlayer(GetNextMove);

    private static async Task<Cell> GetNextMove(Field field)
    {
        await Task.Delay(Timeout);
        var variants = field.GetEmptyCells().ToArray();
        int randomIndex = Random.Next(variants.Length);
        var randomCell = variants[randomIndex];

        return randomCell;
    }
}