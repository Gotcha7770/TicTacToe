namespace TicTacToe.Models;

public class Field
{
    private readonly Symbol?[,] _symbols;

    public Field() : this(new Symbol?[3,3]) { }

    internal Field(Symbol?[,] symbols)
    {
        _symbols = symbols;
    }

    public void Apply(Move move)
    {
        //TODO: проверка
        _symbols[move.Cell.Row, move.Cell.Column] = move.Symbol;
    }

    public IEnumerable<Cell> GetEmptyCells()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_symbols[i, j] is null)
                    yield return new Cell(i, j);
            }
        }
    }
}