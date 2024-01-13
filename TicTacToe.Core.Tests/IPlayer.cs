using TicTacToe.Models;

namespace TicTacToe.Core.Tests;

public interface IPlayer
{
    Symbol Symbol { get; }

    void Move(Field field);
}