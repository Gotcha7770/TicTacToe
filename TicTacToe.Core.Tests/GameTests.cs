using FluentAssertions;
using TicTacToe.Models;
using TicTacToe.Models.AI;

namespace TicTacToe.Core.Tests;

public class GameTests
{
    [Fact]
    public async Task TwoSimplePlayers()
    {
        var x = SimpleAiStrategy.FromX();
        var o = SimpleAiStrategy.FromO();
        var game = new Game(x, o);

        while (await game.NextMove(CancellationToken.None)) { }

        game.State.Should()
            .BeOneOf(GameState.Draw, GameState.GameOver);
    }
}