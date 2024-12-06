using Microsoft.FSharp.Collections;
using Xunit;

using static TicTacToe.FS.Domain;
using static TicTacToe.FS.Implementation;

namespace TicTacToe.TestCases;

public class RemainingMoves : TheoryData<GameState, FSharpList<Tuple<HorizontalPosition, VerticalPosition>>>
{
    private static readonly CellState X = CellState.NewPlayed(Player.PlayerX);
    private static readonly CellState O = CellState.NewPlayed(Player.PlayerO);
    private static readonly CellState _ = CellState.Empty;

    public RemainingMoves()
    {
        Add(GameState.Empty, allPositions);
        Add(
            GameState.Create(
            [
                X, _, O,
                _, X, _,
                O, _, X
            ]),
            ListModule.OfSeq(
            [
                Tuple.Create(HorizontalPosition.Center, VerticalPosition.Top),
                Tuple.Create(HorizontalPosition.Left, VerticalPosition.Middle),
                Tuple.Create(HorizontalPosition.Right, VerticalPosition.Middle),
                Tuple.Create(HorizontalPosition.Center, VerticalPosition.Bottom)
            ]));
        Add(
            GameState.Create(
            [
                X, O, X,
                X, O, O,
                O, X, X
            ]),
            ListModule.Empty<Tuple<HorizontalPosition, VerticalPosition>>());
    }
}