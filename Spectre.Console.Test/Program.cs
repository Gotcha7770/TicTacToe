using System.Drawing;
using System.Runtime.InteropServices;
using Spectre.Console;
using Spectre.Console.Widgets;
using Spectre.Console.Widgets.Field;
using Color = Spectre.Console.Color;
using Size = Spectre.Console.Size;

var field = new Field
{
    Size = new Size(3, 3),
    CellSize = 5,
    Background = Color.LightSeaGreen,
    BorderColor = Color.Turquoise4
};

// AnsiConsole.Write(Align.Center(
//     new Layout()
//         .SplitRows(
//             new Layout(new Padder(new Markup("V"), new Padding(112, 0))).Size(1),
//             new Layout()
//                 .SplitColumns(
//                     new Layout(new Padder(new Markup(">"), new Padding(0, 2))).Size(1), 
//                     new Layout(field).Size(34))), VerticalAlignment.Middle));

AnsiConsole.Write(field);
//var cell = AnsiConsole.Prompt(new FieldPrompt(field));

//Console.WriteLine(cell);

public static class Mouse
{
    public static Point GetCursorPosition()
    {
        if (NativeMethods.GetCursorPos(out var cursorPos))
        {
            return new Point(cursorPos.X, cursorPos.Y);
        }

        return Point.Empty;
    }
}

static class NativeMethods
{
    internal struct CursorPos
    {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll")]
    internal static extern bool GetCursorPos(out CursorPos lpPoint);
}