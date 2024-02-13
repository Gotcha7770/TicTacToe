using TicTacToe.Models;
using TicTacToe.Models.AI;

namespace TicTacToe.Core.Tests;

public class GameTests
{
    [Fact]
    public async Task TwoSimplePlayers()
    {
        var first = SimpleAiPlayer.FromX();
        var second = SimpleAiPlayer.FromO();
        var game = new Game(first, second);

        while (await game.NextMove()) { }
    }
}