namespace TicTacToe.Models;

public record Cell
{
    public Cell(byte row, byte column)
    {
        Row = row < 3 ? row : throw new ArgumentOutOfRangeException(nameof(row));
        Column = column < 3 ? column : throw new ArgumentOutOfRangeException(nameof(column));
    }

    public int Row { get; }
    public int Column { get; }
}