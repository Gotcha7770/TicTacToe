namespace TicTacToe.Models;

public class OPlayer : Player
{
    public OPlayer(Func<Field, Task<Cell>> moveStrategy) : base(moveStrategy) { }

    public override Symbol Symbol => Symbol.O;
}