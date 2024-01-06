using FluentAssertions;
using TicTacToe.Models;

namespace TicTacToe.Core.Tests;

public class FieldTests
{
    [Theory]
    [ClassData(typeof(GetEmptyTestCases))]
    public void GetEmptyCellsTest(Field field, Cell[] expected)
    {
        field.GetEmptyCells()
            .Should()
            .BeEquivalentTo(expected);
    }
}