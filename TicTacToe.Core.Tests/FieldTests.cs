using FluentAssertions;
using TicTacToe.Models;

namespace TicTacToe.Core.Tests;

public class FieldTests
{
    [Theory]
    [ClassData(typeof(GetEmptyCellsTestCases))]
    public void GetEmptyCellsTest(Field field, Cell[] expected)
    {
        field.GetEmptyCells()
            .Should()
            .BeEquivalentTo(expected);
    }
    
    [Theory]
    [ClassData(typeof(GetWinnerTestCases))]
    public void GetWinnerTest(Field field, Symbol? expected)
    {
        field.GetWinner()
            .Should()
            .Be(expected);
    }
}