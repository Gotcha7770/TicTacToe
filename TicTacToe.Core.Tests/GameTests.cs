using FluentAssertions;
using TicTacToe.Models;
using TicTacToe.Models.AI;

namespace TicTacToe.Core.Tests;

public class GameTests
{
    [Fact]
    public async Task TwoSimplePlayers()
    {
        var xPlayer = new SimpleAiStrategy().AsXPlayer();
        var oPlayer = new SimpleAiStrategy().AsOPlayer();
        var game = new Game(xPlayer, oPlayer);

        while (await game.NextMove()) { }

        game.State.Should()
            .BeOneOf(GameState.Draw, GameState.GameOver);
    }
}