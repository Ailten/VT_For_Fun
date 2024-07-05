
using System.Globalization;
using System.Text;
using Raylib_cs;

public class TextUi : Entity
{
    public string txt = "";
    private Raylib_cs.Font font;
    public float fontSize;
    private float spacing;
    public Color color;

    public TextUi(int idLevel, string txt, Color color, float fontSize=20f, float spacing=2f, string fontName="IntensaFuente") : base(idLevel)
    {
        this.txt = txt;
        this.color = color;
        this.fontSize = fontSize;
        this.spacing = spacing;
        this.font = Font.getFont(fontName);
        this.tags = new Tag[]{ Tag.isHud, Tag.isHudScrollable };
    }

    public override void addDraw(Vector2 posEncrageInCanvas)
    {
        System.Numerics.Vector2 sizeTextureTxt = Raylib.MeasureTextEx(
            font,
            txt,
            fontSize *Window.scaleCanvasWindow,
            spacing *Window.scaleCanvasWindow
        );

        Raylib.DrawTextEx(
            font, //font.
            txt, //txt.
            new System.Numerics.Vector2( //pos in canvas.
                posEncrageInCanvas.x - sizeTextureTxt.X * encrage.x * Window.scaleCanvasWindow, 
                posEncrageInCanvas.y - sizeTextureTxt.Y * encrage.x * Window.scaleCanvasWindow
            ), 
            fontSize *Window.scaleCanvasWindow, //font size.
            spacing *Window.scaleCanvasWindow, //space between two letter.
            color //color.
        );

    }

}