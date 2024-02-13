namespace TicTacToe.Models;

public class OPlayer : Player
{
    protected OPlayer(Func<Field, Task<Cell>> moveStrategy) : base(moveStrategy) { }

    public override Symbol Symbol => Symbol.O;
}