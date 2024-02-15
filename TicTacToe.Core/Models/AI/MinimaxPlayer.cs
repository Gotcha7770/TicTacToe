namespace TicTacToe.Models.AI;

internal static class MinimaxPlayer
{
    public static XPlayer FromX() => new XPlayer(GetNextMove);
    public static OPlayer FromO() => new OPlayer(GetNextMove);

    private static Task<Cell> GetNextMove(Field field)
    {
        throw new NotImplementedException();
    }
}