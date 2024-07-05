
public class Vector2
{

    public float x;
    public float y;

    public Vector2(float x=0.0f, float y=0.0f) //make a new vector.
    {
        setBoth(x, y);
    }

    public Vector2(Vector2 vectorBase) //make a new vector base on another (for not edit the first instance).
    {
        setBoth(vectorBase.x, vectorBase.y);
    }

    public Vector2(System.Numerics.Vector2 numericVector) //make a new vector base on a Numeric.Vector2.
    {
        setBoth(numericVector.X, numericVector.Y);
    }

    public void setBoth(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public void setBothByNumVector(System.Numerics.Vector2 numericVector)
    {
        setBoth(numericVector.X, numericVector.Y);
    }

    public static float dist(Vector2 vectorA, Vector2 vectorB)
    {
        float difX = vectorB.x - vectorA.x;
        float difY = vectorB.y - vectorA.y;
        return (float)Math.Sqrt(difX*difX + difY*difY);
    }

    public static void normalize(ref Vector2 vector)
    {
        float dist = (float)Math.Sqrt(vector.x*vector.x + vector.y*vector.y);
        if(dist == 0f)
            return;
        vector.setBoth(
            vector.x / dist,
            vector.y / dist
        );
    }

    public static Vector2 rotate(Vector2 vector, float degreeAngle)
    {
        float eulerAngle = Math2.degreeToEuler(degreeAngle);
        return new Vector2(
            (float)Math.Cos(eulerAngle) * vector.x + (-(float)Math.Sin(eulerAngle)) * vector.y,
			(float)Math.Sin(eulerAngle) * vector.x + (float)Math.Cos(eulerAngle) * vector.y
        );
    }

}