using TicTacToe.Models;

namespace TicTacToe.Core.Tests.TestCases;

public class GetWinnerTestCases : TheoryData<Field, Symbol?>
{
    private static readonly Symbol? _ = null;

    public GetWinnerTestCases()
    {
        Add(new Field(new[,]
        {
            {_, _, _},
            {_, _, _},
            {_, _, _}
        }), null);
        Add(new Field(new[,]
        {
            {X, _, O},
            {_, O, _},
            {X, _, X}
        }), null);
        Add(new Field(new Symbol?[,]
        {
            {X, O, X},
            {O, X, O},
            {O, X, O}
        }), null);

        foreach (var symbol in Enum.GetValues<Symbol>())
        {
            for (int i = 0; i < 3; i++)
            {
                Add(FieldFactory.FillRow(i, symbol), symbol);
                Add(FieldFactory.FillColumn(i, symbol), symbol);
            }
            
            Add(FieldFactory.FillLeftDiagonal(symbol), symbol);
            Add(FieldFactory.FillRightDiagonal(symbol), symbol);
        }
    }
}