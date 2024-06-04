namespace Spectre.Console.Widgets.Field;

public record Cell(byte Column, byte Row);

public class FieldPrompt : IPrompt<Cell>
{
    private readonly Field _field;

    public Cell CurrentCell = new(0, 0);

    public FieldPrompt(Field field)
    {
        _field = field;
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
        int cellWidth = (_field.CellSize + 1) * _field.PixelWidth;
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
                        int horizontalPadding = center + (cellWidth * CurrentCell.Column) - cellWidth;
                        horizontalPadder.PadLeft(horizontalPadding);
                        int verticalPadding = 2 + (_field.CellSize + 1) * CurrentCell.Row;
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

        return CurrentCell;
    }

    private FieldPromptInputResult HandleInput(ConsoleKey key)
    {
        if (key is ConsoleKey.Enter or ConsoleKey.Spacebar or ConsoleKey.Packet)
        {
            return FieldPromptInputResult.Submit;
        }
        if (key is ConsoleKey.LeftArrow && CurrentCell.Column > 0)
        {
            CurrentCell = CurrentCell with { Column = (byte)(CurrentCell.Column - 1) };
            return FieldPromptInputResult.Refresh;
        }
        if (key is ConsoleKey.RightArrow && CurrentCell.Column < _field.Size.Width - 1)
        {
            CurrentCell = CurrentCell with { Column = (byte)(CurrentCell.Column + 1) };
            return FieldPromptInputResult.Refresh;
        }
        if (key is ConsoleKey.UpArrow && CurrentCell.Row > 0)
        {
            CurrentCell = CurrentCell with { Row = (byte)(CurrentCell.Row - 1) };
            return FieldPromptInputResult.Refresh;
        }
        if (key is ConsoleKey.DownArrow && CurrentCell.Row < _field.Size.Height - 1)
        {
            CurrentCell = CurrentCell with { Row = (byte)(CurrentCell.Row + 1) };
            return FieldPromptInputResult.Refresh;
        }

        return FieldPromptInputResult.None;
    }
}