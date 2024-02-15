using FluentAssertions;
using TicTacToe.Models;
using TicTacToe.Models.AI;

namespace TicTacToe.Core.Tests;

public class GameTests
{
    [Fact]
    public async Task TwoSimplePlayers()
    {
        var x = SimpleAiPlayer.FromX();
        var o = SimpleAiPlayer.FromO();
        var game = new Game(x, o);

        while (await game.NextMove()) { }

        game.State.Should()
            .BeOneOf(GameState.Draw, GameState.GameOver);
    }
}