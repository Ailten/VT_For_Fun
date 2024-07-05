
public class Mob : Entity
{

    public static Sprite? spriteStatic;

    public Mob(int idLevel, string typeMob="sanglier") : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/Mob/Mob.png", //path file sprite.
                new Vector2(126, 126) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("sanglier", new Vector2(0, 0));
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.state = typeMob;
        this.encrage.y = 0.9f;

        this.timeLastActivity = Update.time;
    }

    public ArchMobSpawner? archFromSpawn;
    private ArchMobSpawner getArchFromSpawn{
        get{ return archFromSpawn ?? throw new Exception("error : archFromSpawn is null.");}
    }

    private Vector2? posToWalk;
    private Vector2 getPosToWalk{
        get{ return posToWalk ?? throw new Exception("error : posToWalk is null."); }
    }

    private int timeLastActivity;
    private const int timeCooldownActivity = 600;
    private const float speedWalk = 60f;

    public void update()
    {
        if(posToWalk != null){ //do walk.

            float distToDestination = Vector2.dist(this.pos, this.getPosToWalk); //walk pos.
            Vector2 walkVector = new Vector2(
                this.getPosToWalk.x - this.pos.x,
                this.getPosToWalk.y - this.pos.y
            );
                
            if(walkVector.x < -0.5f) //reverce sprite anime walk.
                this.reverceSprite.x = -1f; 
            else if(walkVector.x > 0.5f)
                this.reverceSprite.x = 1f; 

            Vector2.normalize(ref walkVector);
            this.pos.setBoth(
                this.pos.x + walkVector.x * speedWalk * Update.deltaTime,
                this.pos.y + walkVector.y * speedWalk * Update.deltaTime
            );
            //this.updateAnimeWalk(timeInLevel); //anime walk (todo).

            if(Vector2.dist(this.pos, this.getPosToWalk) > distToDestination){ //end walk.
                //this.state = "base"; //reset anime not walk (todo).
                this.timeLastActivity = Update.time;
                this.posToWalk = null;
            }

            return;
        }

        if((Update.time - timeLastActivity) < timeCooldownActivity) //cooldown.
            return;

        setRandomPosToWalk(); //choose a new random pos to walk.
    }

    private void setRandomPosToWalk()
    {
        this.posToWalk = Vector2.rotate( //random vector walk.
            new Vector2(0, 1),
            Math2.rng(0, 360)
        );

        float distRayonWalk = Math2.rng(50, 100); //distance rayon walk.
        this.posToWalk.setBoth(
            this.posToWalk.x * distRayonWalk,
            this.posToWalk.y * distRayonWalk
        );

        this.posToWalk.setBoth( //from pos villageoi.
            this.posToWalk.x + this.pos.x,
            this.posToWalk.y + this.pos.y
        );

        Vector2 vectorMobToSpawner = new Vector2( //edit rng walk to came closer to spawner.
            this.getArchFromSpawn.pos.x - this.pos.x,
            this.getArchFromSpawn.pos.y - this.pos.y
        );
        Vector2.normalize(ref vectorMobToSpawner);
        float intencityBackToCenter = 50f;
        vectorMobToSpawner.setBoth(
            vectorMobToSpawner.x * intencityBackToCenter,
            vectorMobToSpawner.y * intencityBackToCenter
        );
        this.posToWalk.setBoth(
            this.posToWalk.x + vectorMobToSpawner.x,
            this.posToWalk.y + vectorMobToSpawner.y
        );
    }


}