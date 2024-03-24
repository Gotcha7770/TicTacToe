using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace TicTacToe.Avalonia.Controls;

public class Cell : TemplatedControl
{
    public static readonly StyledProperty<object> ContentProperty = AvaloniaProperty.Register<Cell, object>(nameof(Content));

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static readonly StyledProperty<ICommand> CommandProperty = AvaloniaProperty.Register<Cell, ICommand>("Command");

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly StyledProperty<object> CommandParameterProperty = AvaloniaProperty.Register<Cell, object>("CommandParameter");

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
}