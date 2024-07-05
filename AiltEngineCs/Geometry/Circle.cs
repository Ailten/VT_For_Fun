
using System.Numerics;

public class Circle : Geometry
{

    public float rayon{
        get{ return this.lines[0].posStart.y; }
    }

    public Circle(Vector2 posCenter, float rayon, int precision=8)
    {
        Vector2 topRayon = new Vector2(0, rayon);
        float fractionAngle = 360f / precision;

        List<Line> listLines = new();
        listLines.Add(new Line( //first line.
            topRayon,
            Vector2.rotate(topRayon, fractionAngle)
        ));
        for(int i=1; i<precision-1; i++){ //loop (expet first and last line).
            listLines.Add(new Line(
                Vector2.rotate(topRayon, fractionAngle * i),
                Vector2.rotate(topRayon, fractionAngle * (i+1))
            ));
        }
        listLines.Add(new Line( //last line.
            listLines[listLines.Count-1].posEnd,
            topRayon
        ));

        this.lines = listLines.ToArray();


    }

}