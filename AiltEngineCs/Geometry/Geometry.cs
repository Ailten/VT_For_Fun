
public class Geometry
{
    protected Line[] lines;
    public Line[] linesArray{
        get{ return lines; }
    }

    public Geometry(params Line[] lines)
    {
        this.lines = lines;
    }

}