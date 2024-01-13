using TicTacToe.Models;

namespace TicTacToe.Interfaces;

public interface IPlayer
{
    Symbol Symbol { get; }

    void Move(Field field);
}