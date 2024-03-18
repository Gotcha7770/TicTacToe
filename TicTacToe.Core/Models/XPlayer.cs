namespace TicTacToe.Models;

public class XPlayer : Player
{
    public XPlayer(Func<Field, Task<Cell>> moveStrategy) : base(moveStrategy) { }

    public override Symbol Symbol => Symbol.X;
}