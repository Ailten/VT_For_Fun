
public class ArchSpawner : Entity
{

    public static Sprite? spriteStatic;

    public int timeLastSpawnCheck;
    public const int timeBetweenTwoCheck = 2000;

    public ArchSpawner(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/ArchSpawner/ArchSpawner.png", //path file sprite.
                new Vector2(256, 256) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.state = "base";
        this.encrage.y = 0.935f;

        //this.geometryTrigger = new Geometry[]{
        //    new Rect(
        //        new Vector2(-5, -5),
        //        new Vector2(5, 5)
        //    )
        //};
    }


    public bool isCheckTime()
    {
        if(Update.time - timeLastSpawnCheck >= timeBetweenTwoCheck){
            timeLastSpawnCheck = Update.time;
            return true;
        }
        return false;
    }

}