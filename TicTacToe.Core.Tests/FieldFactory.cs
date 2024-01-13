using TicTacToe.Models;

namespace TicTacToe.Core.Tests;

public static class FieldFactory
{
    private static readonly Symbol? _ = null;
    
    public static Field FillRow(int index, Symbol p) => index switch
    {
        0 => new Field(new[,]
        {
            {p, p, p},
            {_, _, _},
            {_, _, _}
        }),
        1 => new Field(new[,]
        {
            {_, _, _},
            {p, p, p},
            {_, _, _}
        }),
        2 => new Field(new[,]
        {
            {_, _, _},
            {_, _, _},
            {p, p, p}
        }),
        _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
    };

    public static Field FillColumn(int index, Symbol p) => index switch
    {
        0 => new Field(new[,]
        {
            {p, _, _},
            {p, _, _},
            {p, _, _}
        }),
        1 => new Field(new[,]
        {
            {_, p, _},
            {_, p, _},
            {_, p, _}
        }),
        2 => new Field(new[,]
        {
            {_, _, p},
            {_, _, p},
            {_, _, p}
        }),
        _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
    };

    public static Field FillLeftDiagonal(Symbol p)
    {
        return new Field(new [,]
        {
            {p, _, _},
            {_, p, _},
            {_, _, p}
        });
    }
    
    public static Field FillRightDiagonal(Symbol p)
    {
        return new Field(new [,]
        {
            {_, _, p},
            {_, p, _},
            {p, _, _}
            
        });
    }
}