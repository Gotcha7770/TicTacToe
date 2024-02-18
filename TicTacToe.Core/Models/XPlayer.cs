namespace TicTacToe.Models;

public class XPlayer : Player
{
    public XPlayer(Func<Field, Symbol, Task<Cell>> moveStrategy) : base(moveStrategy) { }

    public override Symbol Symbol => Symbol.X;
}