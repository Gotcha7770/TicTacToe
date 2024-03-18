namespace TicTacToe.Models.AI;

public static class SimpleAiPlayer
{
    private static readonly Random Random = new Random();

    public static XPlayer FromX() => new XPlayer(GetNextMove);
    public static OPlayer FromO() => new OPlayer(GetNextMove);

    private static Task<Cell> GetNextMove(Field field)
    {
        var variants = field.GetEmptyCells().ToArray();
        int randomIndex = Random.Next(variants.Length);
        var randomCell = variants[randomIndex];

        return Task.FromResult(randomCell);
    }
}