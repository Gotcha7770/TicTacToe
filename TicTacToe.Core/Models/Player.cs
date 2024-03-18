using TicTacToe.Interfaces;

namespace TicTacToe.Models;

public abstract class Player : IPlayer
{
    private readonly Func<Field, Task<Cell>> _moveStrategy;

    public abstract Symbol Symbol { get; }
    
    protected Player(Func<Field, Task<Cell>> moveStrategy)
    {
        _moveStrategy = moveStrategy;
    }

    public async Task<Move> GetNextMove(Field field, CancellationToken cancellationToken = default)
    {
        var nextCell = await _moveStrategy(field);

        return new Move(nextCell, Symbol);
    }
}