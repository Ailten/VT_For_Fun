
public static class Math2
{
    private static Random random = new();
    public static int rng(int min=0, int max=100){
        return random.Next(min, max);
    }
    public static int rng(int max){
        return random.Next(max);
    }

    public static void rngList<T>(this IList<T> list)  
    {  
        int quantity = list.Count;  
        while (quantity > 1) {
            int randomIndex = Math2.rng(quantity);
            quantity--;  
            T value = list[randomIndex]; //swap value.
            list[randomIndex] = list[quantity];  
            list[quantity] = value;  
        }  
    }

    public static float lerp(float a, float b, float i){
        return a*(1f-i) + b*i;
    }

    public static float degreeToEuler(float degree){
        return ((degree /180f) * (float)Math.PI);
    }
    public static float eulerToDegree(float euler){
        return ((euler /(float)Math.PI) * 180f);
    }

    public static bool inRange(float min, float value, float max){
        return min <= value && value <= max;
    }

}