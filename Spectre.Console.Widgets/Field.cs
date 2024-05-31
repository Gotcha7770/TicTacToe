using Spectre.Console.Rendering;

namespace Spectre.Console.Widgets;

public class Field : Renderable
{
    public Size Size { get; set; }
    public int CellSize { get; set; }
    public int PixelWidth { get; set; } = 2;
    public Color Background { get; set; }
    public Color BorderColor { get; set; }

    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        if (CellSize < 0)
        {
            throw new InvalidOperationException("Cell size must be greater than zero.");
        }

        if (PixelWidth < 0)
        {
            throw new InvalidOperationException("Pixel width must be greater than zero.");
        }

        var pixel = new string(' ', PixelWidth);
        for (var y = 0; y < Size.Height * (CellSize + 1) - 1; y++)
        {
            for (var x = 0; x < Size.Width * (CellSize + 1) - 1; x++)
            {
                var color = GetColor(x, y);
                yield return new Segment(pixel, new Style(background: color));
            }

            yield return Segment.LineBreak;
        }
    }

    private Color GetColor(int x, int y)
    {
        if ((x + 1) % (CellSize + 1) == 0 || (y + 1) % (CellSize + 1) == 0)
            return BorderColor;

        return Background;
    }
}