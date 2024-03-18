using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using ReactiveUI;
using TicTacToe.Avalonia.ViewModels;

namespace TicTacToe.Avalonia.Controls;

public partial class GameBoard : SelectingItemsControl
{
    public GameBoard()
    {
        InitializeComponent();
        // this.WhenAnyValue(x => x.Size)
        //     .Subscribe(RecreateDecorator);
    }
    
    public static readonly StyledProperty<BoardSize> SizeProperty = AvaloniaProperty.Register<GameBoard, BoardSize>(nameof(Size));

    public BoardSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    // private void RecreateDecorator(BoardSize size)
    // {
    //     if(size is null)
    //     {
    //         Decorator.Children.Clear();
    //     }
    //     else
    //     {
    //         Decorator.RowDefinitions.Add(new RowDefinition(1, GridUnitType.Star));
    //         for (int i = 1; i < size.Rows; i++)
    //         {
    //             Decorator.RowDefinitions.Add(new RowDefinition(6, GridUnitType.Pixel));
    //             var border = new Border();
    //             border.Classes.Add("gridBorder");
    //             border.SetValue(Grid.RowProperty, i);
    //             Decorator.Children.Add(border);
    //             Decorator.RowDefinitions.Add(new RowDefinition(1, GridUnitType.Star));
    //         }
    //
    //         Decorator.ColumnDefinitions.Add(new ColumnDefinition(1, GridUnitType.Star));
    //         for (int i = 1; i < size.Columns; i++)
    //         {
    //             Decorator.ColumnDefinitions.Add(new ColumnDefinition(6, GridUnitType.Pixel));
    //             var border = new Border();
    //             border.Classes.Add("gridBorder");
    //             border.SetValue(Grid.ColumnProperty, i);
    //             Decorator.Children.Add(border);
    //             Decorator.ColumnDefinitions.Add(new ColumnDefinition(1, GridUnitType.Star));
    //         }
    //     }
    // }
}