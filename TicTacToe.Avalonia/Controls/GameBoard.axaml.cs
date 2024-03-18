using System.Collections;
using Avalonia;
using Avalonia.Controls.Primitives;
using TicTacToe.Avalonia.ViewModels;

namespace TicTacToe.Avalonia.Controls;

public class GameBoard : TemplatedControl
{
    public static readonly StyledProperty<BoardSize> SizeProperty = AvaloniaProperty.Register<GameBoard, BoardSize>(nameof(Size));

    public BoardSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public static readonly StyledProperty<IEnumerable> CellsProperty = AvaloniaProperty.Register<GameBoard, IEnumerable>(nameof(Cells));

    public IEnumerable Cells
    {
        get => GetValue(CellsProperty);
        set => SetValue(CellsProperty, value);
    }
}