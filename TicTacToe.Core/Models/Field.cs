using System.Diagnostics;

namespace TicTacToe.Models;

[DebuggerTypeProxy(typeof(FieldDebugView))]
public class Field
{
    //TODO: consider option
    private readonly Symbol?[,] _symbols;

    public Field() : this(new Symbol?[3,3]) { }

    internal Field(Symbol?[,] symbols)
    {
        _symbols = symbols;
    }

    public void Apply(Move move)
    {
        this[move.Cell] = move.Symbol;
    }

    public IEnumerable<Cell> GetEmptyCells()
    {
        for (byte i = 0; i < 3; i++)
        {
            for (byte j = 0; j < 3; j++)
            {
                if (_symbols[i, j] is null)
                    yield return new Cell(i, j);
            }
        }
    }

    public Symbol? GetWinner()
    {
        foreach (var symbol in Enum.GetValues<Symbol>())
        {
            if(GetAllLines().Any(x => x.IsAllSymbolsAre(symbol)))
                return symbol;
        }

        return null;
    }

    public IEnumerable<IEnumerable<Symbol?>> GetAllLines()
    {
        foreach (var row in GetRows())
        {
            yield return row;
        }

        foreach (var column in GetColumns())
        {
            yield return column;
        }

        yield return GetLeftDiagonal();
        yield return GetRightDiagonal();
    }
    
    //TODO: naming?
    public T Scope<T>(Move move, Func<Field, T> selector)
    {
        try
        {
            this[move.Cell] = move.Symbol;
            return selector(this);
        }
        finally
        {
            this[move.Cell] = null;
        }
    }

    protected Symbol? this[Cell cell]
    {
        get => _symbols[cell.Row, cell.Column];
        set => _symbols[cell.Row, cell.Column] = value;
    }

    private IEnumerable<IEnumerable<Symbol?>> GetRows()
    {
        for (byte i = 0; i < 3; i++)
        {
            yield return new[] { _symbols[i, 0], _symbols[i, 1], _symbols[i, 2] };
        }
    }

    private IEnumerable<IEnumerable<Symbol?>> GetColumns()
    {
        for (byte i = 0; i < 3; i++)
        {
            yield return new[] { _symbols[0, i], _symbols[1, i], _symbols[2, i] };
        }
    }

    private IEnumerable<Symbol?> GetLeftDiagonal()
    {
        return new[] { _symbols[0, 0], _symbols[1, 1], _symbols[2, 2] };
    }

    private IEnumerable<Symbol?> GetRightDiagonal()
    {
        return new[] { _symbols[2, 0], _symbols[1, 1], _symbols[0, 2] };
    }
    
    internal class FieldDebugView
    {
        private readonly Field _field;

        public FieldDebugView(Field field)
        {
            _field = field;
        }

        public string View => string.Concat(_field.GetRows().Select(GetString));

        private string GetString(IEnumerable<Symbol?> symbols)
        {
            return string.Concat(symbols.Select(GetString)) + Environment.NewLine;
        }

        private string GetString(Symbol? symbol) => symbol switch
        {
            Symbol.X => "❌",
            Symbol.O => "⭕",
            _ => "⬛"
        };
    }
}