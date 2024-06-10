using Spectre.Console.Rendering;

namespace Spectre.Console.Widgets.Field;

public class Field : Renderable
{
    public Size Size { get; set; }
    public int CellSize { get; set; }
    public int PixelWidth { get; set; } = 2;
    public Color Background { get; set; }
    public Color BorderColor { get; set; }

    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        if (Size is { Width: < 3 } or { Height: < 3 })
        {
            throw new InvalidOperationException("Size 3 is the minimum meaningful game field size.");
        }
        if (CellSize < 1)
        {
            throw new InvalidOperationException("Cell size 3 must be greater than zero.");
        }

        if (PixelWidth < 1)
        {
            throw new InvalidOperationException("Pixel width must be greater than zero.");
        }
        
        var pixel = new string(' ', PixelWidth);
        for (var y = 1; y < Size.Height * (CellSize + 1); y++)
        {
            for (var x = 1; x < Size.Width * (CellSize + 1); x++)
            {
                var color = GetColor(x, y);
                yield return new Segment(pixel, new Style(background: color));
            }
        
            yield return Segment.LineBreak;
        }
    }

    private Color GetColor(int x, int y)
    {
        if (x % (CellSize + 1) == 0 || y % (CellSize + 1) == 0)
            return BorderColor;

        return Background;
    }
}