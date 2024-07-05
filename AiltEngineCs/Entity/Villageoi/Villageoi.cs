
using System.Numerics;
using Raylib_cs;

public class Villageoi : Entity
{
    public Stats stats = new();

    public static Sprite? spriteViewerStatic;
    public static Geometry[]? geometryTriggerStatic;

    public int timeWhenStartAnime;

    public Villageoi(int idLevel) : base(idLevel)
    {
        if(geometryTriggerStatic == null){
            geometryTriggerStatic = new Geometry[]{
                //new Rect( //full body.
                //    new(-22, -125), 
                //    new(22, 3)
                //)
                new Rect( //feet.
                    new(-22, -3), 
                    new(22, 3)
                )
            };
        }

        if(spriteViewerStatic == null && this.sprite == null){
            spriteViewerStatic = new Sprite(
                "assets/Entity/Skin/Viewer.png", //path file sprite.
                new Vector2(133, 167) //size of one tile.
            );

            //set tile map in sprite.
            spriteViewerStatic.addATileMap("base", new Vector2(0, 0));
            spriteViewerStatic.addATileMap("walk0", new Vector2(spriteViewerStatic.size.x, 0));
            spriteViewerStatic.addATileMap("walk1", new Vector2(spriteViewerStatic.size.x * 2, 0));
            spriteViewerStatic.addATileMap("walk2", new Vector2(spriteViewerStatic.size.x * 3, 0));
            spriteViewerStatic.addATileMap("walk3", new Vector2(spriteViewerStatic.size.x * 4, 0));
        }

        //set params of entity.
        this.state = "base";
        this.encrage.y = 0.925f;
        this.geometryTrigger = geometryTriggerStatic;
        if(this.sprite == null)
            this.sprite = spriteViewerStatic;
    }

    public override void addDraw(Vector2 posEncrageInCanvas)
    {
        Raylib_cs.Font font = Font.getFont("IntensaFuente");

        //eval with txt sprite.
        System.Numerics.Vector2 sizeTextureTxt = Raylib.MeasureTextEx(
            font,
            stats.name,
            25f *Window.scaleCanvasWindow,
            2f *Window.scaleCanvasWindow
        );

        //draw text name.
        Raylib.DrawTextEx(
            font, //font.
            stats.name, //txt.
            new System.Numerics.Vector2( //pos in canvas.
                posEncrageInCanvas.x - (sizeTextureTxt.X/2f), 
                posEncrageInCanvas.y - (size.y * Window.scaleCanvasWindow * 1.1f)
            ), 
            25f *Window.scaleCanvasWindow, //font size.
            2f *Window.scaleCanvasWindow, //space between two letter.
            Color.White //color.
        );
    }

    public const int timeOneFrameAnimeWalk = 200;
    public const int countFrameAnimeWalk = 4;
    public void updateAnimeWalk(int currentTimeLevel)
    {
        int indexAnimeWalk = (int)((float)(currentTimeLevel - this.timeWhenStartAnime) /timeOneFrameAnimeWalk) %countFrameAnimeWalk;
        this.state = "walk" + indexAnimeWalk;
    }


    public void loadStatsFromSave(Stats statsLoad)
    {
        this.stats = statsLoad;
        this.pos = statsLoad.posSave;
    }
    public void makeStatsValideForSave()
    {
        this.stats.posSave = this.pos;
    }

    public void setRandomStats()
    {
        //param.
        const int quantityOfStats = 5;
        const int maxByStats = 100;

        //first random.
        List<int> rngStats = new List<int>(new int[quantityOfStats]);
        for(int i=0; i<rngStats.Count; i++){
            rngStats[i] = Math2.rng(0, 101);
        }

        //rectif random.
        int rest = (quantityOfStats * (maxByStats/2)) - rngStats.Sum();
        while(rest != 0){
            int indexRng = Math2.rng(0, rngStats.Count);
            int valueIncrement = Math2.rng(1, Math.Min(Math.Abs(rest)+1, 6));
            int multiplyPositive = (rest > 0? 1: -1);
            int newValueStats = rngStats[indexRng] + valueIncrement * multiplyPositive;
            
            if(newValueStats < 0 || newValueStats > maxByStats) //skip if over or under range.
                continue;

            rngStats[indexRng] = newValueStats;
            rest -= valueIncrement * multiplyPositive;
        }

        //random order.
        Math2.rngList(rngStats);
        
        //apply.
        int indexIncrement = 0;
        this.stats.strenght = rngStats[indexIncrement++];
        this.stats.dexterity = rngStats[indexIncrement++];
        this.stats.intelligence = rngStats[indexIncrement++];
        this.stats.luck = rngStats[indexIncrement++];
        this.stats.sociability = rngStats[indexIncrement++];

        //debug.
        Console.WriteLine($"force : [{this.stats.strenght}]");
        Console.WriteLine($"dexte : [{this.stats.dexterity}]");
        Console.WriteLine($"intel : [{this.stats.intelligence}]");
        Console.WriteLine($"chanc : [{this.stats.luck}]");
        Console.WriteLine($"socia : [{this.stats.sociability}]");
    }

    public void setRandomPosToWalk()
    {
        this.stats.posToWalk = Vector2.rotate( //random vector walk.
            new Vector2(0, 1),
            Math2.rng(0, 360)
        );

        float distRayonWalk = Math2.rng(250, 300); //distance rayon walk.
        this.stats.posToWalk.setBoth(
            this.stats.posToWalk.x * distRayonWalk,
            this.stats.posToWalk.y * distRayonWalk
        );

        this.stats.posToWalk.setBoth( //from pos villageoi.
            this.stats.posToWalk.x + this.pos.x,
            this.stats.posToWalk.y + this.pos.y
        );

        Vector2 vectorVillageoiToCenterVillage = new Vector2(-this.pos.x, -this.pos.y); //edit rng walk to came closer to center village.
        Vector2.normalize(ref vectorVillageoiToCenterVillage);
        float intencityBackToCenter = 50f;
        vectorVillageoiToCenterVillage.setBoth(
            vectorVillageoiToCenterVillage.x * intencityBackToCenter,
            vectorVillageoiToCenterVillage.y * intencityBackToCenter
        );
        this.stats.posToWalk.setBoth(
            this.stats.posToWalk.x + vectorVillageoiToCenterVillage.x,
            this.stats.posToWalk.y + vectorVillageoiToCenterVillage.y
        );
    }
}