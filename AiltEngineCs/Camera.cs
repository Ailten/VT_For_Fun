
public static class Camera
{
    public static Vector2 pos = new();
    public static float posScroll;

    public static Entity? entityFollow;

    public static void update()
    {
        if(entityFollow == null)
            return;

        const float speedLerp = 0.06f;

        pos.setBoth( //lerp pos cam to pos entity follow.
            Math2.lerp(pos.x, entityFollow.pos.x -(Window.size.x/2f), speedLerp),
            Math2.lerp(pos.y, entityFollow.pos.y -(Window.size.y/2f), speedLerp)
        );
    }

    public static void remapPosWorldToFollowCam(ref Vector2 posWorld)
    {
        posWorld.setBoth(
            posWorld.x - pos.x,
            posWorld.y - pos.y
        );
    }

    public static void focus()
    {
        if(entityFollow == null)
            return;

        pos.setBoth(
            entityFollow.pos.x -(Window.size.x/2f),
            entityFollow.pos.y -(Window.size.y/2f)
        );
    }

}