namespace TicTacToe.Models;

public record Move
{
    public Move(Cell cell, Symbol symbol)
    {
        Cell = cell;
        Symbol = Enum.IsDefined(symbol)
            ? symbol 
            : throw new ArgumentOutOfRangeException(nameof(symbol));
    }

    public Cell Cell { get; }
    public Symbol Symbol { get; }
};