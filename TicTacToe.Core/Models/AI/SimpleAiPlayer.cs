namespace TicTacToe.Models.AI;

internal static class SimpleAiPlayer
{
    private static readonly Random Random = new Random();

    private class X : XPlayer
    {
        public X(Func<Field, Task<Cell>> moveStrategy) : base(moveStrategy) { }
    }

    private class O : OPlayer
    {
        public O(Func<Field, Task<Cell>> moveStrategy) : base(moveStrategy) { }
    }

    public static XPlayer FromX() => new X(GetNextMove);
    public static OPlayer FromO() => new O(GetNextMove);

    private static Task<Cell> GetNextMove(Field field)
    {
        var variants = field.GetEmptyCells().ToArray();
        int randomIndex = Random.Next(variants.Length);
        var randomCell = variants[randomIndex];

        return Task.FromResult(randomCell);
    }
}