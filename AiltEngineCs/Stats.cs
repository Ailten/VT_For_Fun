
public class Stats
{
    //name from viewer twitch.
    public string name = "";
    
    //stats.
    public int strenght;
    public int dexterity;
    public int intelligence;
    public int luck;
    public int sociability;

    //pos walk.
    public Vector2 posSave = new(); //need to be set before save.
    public Vector2? posToWalk;
    public Vector2 getPosToWalk{
        get{ return posToWalk ?? throw new Exception("error : posToWalk is null"); }
    }
    public int timeLastWalk = Update.time;
    public const int timeBetweenTwoWalk = 3000;
    public const float speedWalk = 90f;

}