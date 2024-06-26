﻿using TicTacToe.Models;

namespace TicTacToe.Core.Tests.TestCases;

public class GetEmptyCellsTestCases : TheoryData<Field, Cell[]>
{
    private static readonly Symbol? _ = null;

    public GetEmptyCellsTestCases()
    {
        Add(
            new Field(new Symbol?[9]),
            [
                new Cell(0, 0),
                new Cell(0, 1),
                new Cell(0, 2),
                new Cell(1, 0),
                new Cell(1, 1),
                new Cell(1, 2),
                new Cell(2, 0),
                new Cell(2, 1),
                new Cell(2, 2)
            ]);

        Add(
            new Field(
            [
                X, _, O,
                _, X, _,
                O, _, X
            ]),
            [
                new Cell(0, 1),
                new Cell(1, 0),
                new Cell(1, 2),
                new Cell(2, 1)
            ]);

        Add(
            new Field(
            [
                X, O, X,
                X, O, O,
                O, X, X
            ]), 
            []);
    }
}