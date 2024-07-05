
using System.Numerics;
using Raylib_cs;

public class Rect : Geometry
{
    public Vector2 posTopLeft{
        get{ return this.lines[0].posStart; }
    }
    public Vector2 posDownRight{
        get{ return this.lines[2].posStart; }
    }

    public Rect(Vector2 posTopLeft, Vector2 posDownRight)
    {
        Vector2 posTopRight = new Vector2(posDownRight.x, posTopLeft.y);
        Vector2 posDownLeft = new Vector2(posTopLeft.x, posDownRight.y);

        this.lines = new Line[]{
            new Line( //line top.
                posTopLeft,
                posTopRight
            ),
            new Line( //line right.
                posTopRight,
                posDownRight
            ),
            new Line( //line down.
                posDownRight,
                posDownLeft
            ),
            new Line( //line left.
                posDownLeft,
                posTopLeft
            )
        };
    }


    // <--- get a rect destination of a sprite (START) --->
    public static Rectangle getRectDest(Vector2 posInCanvas, Vector2 size)
    {
        return new Rectangle(
            posInCanvas.x,
            posInCanvas.y,
            size.x * Window.scaleCanvasWindow,
            size.y * Window.scaleCanvasWindow
        );
    }
    public static Rectangle getRectDest(Vector2 posInCanvas, Vector2 size, Vector2 scale)
    {
        return new Rectangle(
            posInCanvas.x,
            posInCanvas.y,
            size.x * Window.scaleCanvasWindow *scale.x,
            size.y * Window.scaleCanvasWindow *scale.y
        );
    }
    // <--- get a rect destination of a sprite (END) --->

}