using FluentAssertions;
using TicTacToe.Models;
using TicTacToe.Models.AI;

namespace TicTacToe.Core.Tests;

public class MinimaxPlayerTests
{
    private static readonly Symbol? _ = null;

    [Fact]
    public async Task Opening_OPlayerMovesToCenter()
    {
        var player = new MinimaxPlayer(O);
        var field = new Field(new[,]
        {
            { _, _, _ },
            { _, _, _ }, // <- [1, 1]
            { X, _, _ }
        });

        var move = await player.GetNextMove(field);
        move.Should()
            .Be(new Move(new Cell(1, 1), O));
    }

    [Fact]
    public async Task OneMoveToWin_PlayerDoThatMove()
    {
        var player = new MinimaxPlayer(X);
        var field = new Field(new[,] { { O, _, X }, { X, _, _ }, { X, O, O } });

        var move = await player.GetNextMove(field);
        move.Should()
            .Be(new Move(new Cell(1, 1), X));

        field = new Field(new[,]
        {
            { X, O, _ },
            { X, X, _ }, // <- [1, 2]
            { O, _, O }
        });

        move = await player.GetNextMove(field);
        move.Should()
            .Be(new Move(new Cell(1, 2), X));
    }

    [Fact]
    public async Task OneMoveToFork_PlayerDoThatMove()
    {
        var player = new MinimaxPlayer(X);
        var field = new Field(new[,]
        {
            { X, O, _ }, 
            { _, X, _ }, 
            { _, _, O } // <- [2, 0]
        });

        var move = await player.GetNextMove(field);
        move.Should()
            .BeOneOf(new Move(new Cell(1, 0), X), new Move(new Cell(2, 0), X));
    }

    [Fact]
    public async Task OneMoveToLose_PlayerBreaksTheOpponentsLine()
    {
        var player = new MinimaxPlayer(X);
        var field = new Field(new[,]
        {
            { X, _, _ }, // <- [0, 2]
            { _, O, _ },
            { O, _, X }
        });

        var move = await player.GetNextMove(field);
        move.Should()
            .Be(new Move(new Cell(0, 2), X));

        player = new MinimaxPlayer(O);
        field = new Field(new[,]
        {
            { _, O, _ },
            { X, X, _ }, // <- [1, 2] 
            { _, _, _ }
        });

        move = await player.GetNextMove(field);
        move.Should()
            .Be(new Move(new Cell(1, 2), O));
    }

    [Fact]
    public async Task AnyMoveToLoose()
    {
        var player = new MinimaxPlayer(O);
        var field = new Field(new[,]
        {
            { X, O, _ }, 
            { X, X, _ },
            { _, _, O }
        });

        var move = await player.GetNextMove(field);
        move.Should()
            .BeOneOf(new Move(new Cell(1, 1), O), new Move(new Cell(2, 0), O));

        field = new Field(new[,]
        {
            { X, O, _ },
            { _, X, _ }, 
            { X, _, O }
        });

        move = await player.GetNextMove(field);
        move.Should()
            .BeOneOf(new Move(new Cell(0, 2), O), new Move(new Cell(1, 0), O));
    }

    [Fact]
    public async Task ExampleFromTheArticle_PlayerBreaksTheOpponentsLine()
    {
        var player = new MinimaxPlayer(O);
        var field = new Field(new[,]
        {
            { _, X, _ }, 
            { _, _, X }, 
            { O, O, X }
        });

        var move = await player.GetNextMove(field);
        move.Should()
            .Be(new Move(new Cell(0, 2), O));
    }
}