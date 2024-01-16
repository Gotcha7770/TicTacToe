using TicTacToe.Models;

namespace TicTacToe;

public static class Extensions
{
    public static bool IsAllSymbolsAre(this IEnumerable<Symbol?> line, Symbol symbol)
    {
        return line.All(x => x == symbol);
    }
}