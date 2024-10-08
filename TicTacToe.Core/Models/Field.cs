﻿using System.Diagnostics;

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
    
    internal Field(Symbol?[] symbols)
    {
        _symbols = new Symbol?[3,3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _symbols[i, j] = symbols[i * 3 + j];
            }
        }
    }

    public void Apply(Move move)
    {
        if (this[move.Cell] is not null)
            throw new InvalidOperationException();

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

    public override string ToString()
    {
        return $"""
               {GetSymbol(0, 0)} {GetSymbol(0, 1)} {GetSymbol(0, 2)}
               {GetSymbol(1, 0)} {GetSymbol(1, 1)} {GetSymbol(1, 2)}
               {GetSymbol(2, 0)} {GetSymbol(2, 1)} {GetSymbol(2, 2)}
               """;

        string GetSymbol(int row, int column) => _symbols[row, column] is Symbol symbol ? symbol.ToString() : "_";
    }

    internal Symbol? GetWinner()
    {
        foreach (var symbol in Enum.GetValues<Symbol>())
        {
            if(GetAllLines().Any(x => x.IsAllSymbolsAre(symbol)))
                return symbol;
        }

        return null;
    }

    private Symbol? this[Cell cell]
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