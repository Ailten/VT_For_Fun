
public class ArchMobSpawner : Entity
{

    public static Sprite? spriteStatic;

    public ArchMobSpawner(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/ArchMobSpawner/ArchMobSpawner.png", //path file sprite.
                new Vector2(256, 256) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("cave", new Vector2(0, 0));
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.state = "cave";
        this.encrage.y = 0.935f;

        this.timeLastSpawn = Update.time;

        //this.geometryTrigger = new Geometry[]{
        //    new Rect(
        //        new Vector2(-5, -5),
        //        new Vector2(5, 5)
        //    )
        //};
    }

    public string mobTypeToSpawn = "sanglier";
    public int timeLastSpawn;
    private const int timeBetweenTwoSpawn = 5000;
    private const int maxMobSpawnSimultany = 3;
    public List<Mob> mobSpawned = new();

    public void checkSpawn()
    {
        if(mobSpawned.Count >= maxMobSpawnSimultany)
            return;

        if((Update.time - timeLastSpawn) < timeBetweenTwoSpawn)
            return;

        timeLastSpawn = Update.time;
    
        //spawn a new mob.
        Mob newMob = new Mob(this.idLevel, mobTypeToSpawn);
        newMob.pos.setBoth(
            this.pos.x,
            this.pos.y
        );
        newMob.archFromSpawn = this;
        mobSpawned.Add(newMob);

    }
}