namespace Spectre.Console.Widgets.Field;

public record Cell(byte Column, byte Row);

public class FieldPrompt : IPrompt<Cell>
{
    private readonly Field _field;

    private Cell _currentCell = new(0, 0);
    private readonly int _hStep;
    private readonly int _vStep;

    public FieldPrompt(Field field)
    {
        _field = field;
        _hStep = (_field.CellSize + 1) * _field.PixelWidth;
        _vStep = _field.CellSize + 1;
    }

    public Cell Show(IAnsiConsole console)
    {
        return ShowAsync(console, CancellationToken.None).GetAwaiter().GetResult();
    }

    public async Task<Cell> ShowAsync(IAnsiConsole console, CancellationToken cancellationToken)
    {
        if (!console.Profile.Capabilities.Interactive)
        {
            throw new NotSupportedException(
                "Cannot show selection prompt since the current " +
                "terminal isn't interactive.");
        }

        if (!console.Profile.Capabilities.Ansi)
        {
            throw new NotSupportedException(
                "Cannot show selection prompt since the current " +
                "terminal does not support ANSI escape sequences.");
        }

        int center = AnsiConsole.Profile.Width / 2;
        var horizontalPadder = new Padder(new Markup(Emoji.Known.FastDownButton), new Padding(0, 0));
        var verticalPadder = new Padder(new Markup(":fast_forward_button:"), new Padding(0, 0));

        var align = Align.Center(
            new Layout()
                .SplitRows(
                    new Layout(horizontalPadder).Size(1),
                    new Layout()
                        .SplitColumns(
                            new Layout(verticalPadder).Size(2),
                            new Layout(_field).Size(34))),
            VerticalAlignment.Middle);

        var result = FieldPromptInputResult.Refresh;
        await console.Live(align)
            .StartAsync(async ctx =>
            {
                do
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    
                    if (result == FieldPromptInputResult.Submit)
                    {
                        break;
                    }

                    if (result is FieldPromptInputResult.Refresh)
                    {
                        int horizontalPadding = center + (_hStep * _currentCell.Column) - _hStep;
                        horizontalPadder.PadLeft(horizontalPadding);
                        int verticalPadding = 2 + _vStep * _currentCell.Row;
                        verticalPadder.PadTop(verticalPadding);
                        ctx.Refresh();
                    }

                    var rawKey = await console.Input.ReadKeyAsync(true, cancellationToken).ConfigureAwait(false);
                    if (rawKey is null)
                    {
                        result = FieldPromptInputResult.None;
                        continue;
                    }

                    result = HandleInput(rawKey.Value.Key);
                } while (true);
            });
        
        console.Clear();

        return _currentCell;
    }

    private FieldPromptInputResult HandleInput(ConsoleKey key)
    {
        if (key is ConsoleKey.Enter or ConsoleKey.Spacebar or ConsoleKey.Packet)
        {
            return FieldPromptInputResult.Submit;
        }
        if (key is ConsoleKey.LeftArrow && _currentCell.Column > 0)
        {
            _currentCell = _currentCell with { Column = (byte)(_currentCell.Column - 1) };
            return FieldPromptInputResult.Refresh;
        }
        if (key is ConsoleKey.RightArrow && _currentCell.Column < _field.Size.Width - 1)
        {
            _currentCell = _currentCell with { Column = (byte)(_currentCell.Column + 1) };
            return FieldPromptInputResult.Refresh;
        }
        if (key is ConsoleKey.UpArrow && _currentCell.Row > 0)
        {
            _currentCell = _currentCell with { Row = (byte)(_currentCell.Row - 1) };
            return FieldPromptInputResult.Refresh;
        }
        if (key is ConsoleKey.DownArrow && _currentCell.Row < _field.Size.Height - 1)
        {
            _currentCell = _currentCell with { Row = (byte)(_currentCell.Row + 1) };
            return FieldPromptInputResult.Refresh;
        }

        return FieldPromptInputResult.None;
    }
}