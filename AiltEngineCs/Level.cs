
using System.Data.Common;
using System.Drawing;

public class Level
{
    // <--- Level Manager (START) --->
    private static List<Level> levelLoaded = new List<Level>();

    public static void loadALevel(Level level)
    {
        level.isLoaded = true;
        level.idLevel = levelLoaded.Count;
        levelLoaded.Add(level);
    }
    public static void loadAllLevel()
    {
        MenuSave.load();
        MenuChooseName.load();
        MenuChoosePlayer.load();
        Village.load();
        MenuOption.load();
    }

    public static void activeALevel(Level level)
    {
        Entity.SortAllEntities();

        level.isActive = true;
    }

    public static void unActiveALevel(Level level)
    {
        Entity.entities = Entity.entities.Where(e => e.idLevel != level.idLevel).ToList(); //drop all entity in level (stay order).
        level.entityInLevel = new(); //drop all entity from entityInLevel.

        level.isActive = false;
    }

    public static Level getLevelByName(string nameLevel)
    {
        return levelLoaded.Find(l => l.nameLevel == nameLevel) ?? throw new Exception("error level not found");
    }
    public static Level getLevelById(int idLevel)
    {
        return levelLoaded.Find(l => l.idLevel == idLevel) ?? throw new Exception("error level not found");
    }
    public static Level? getLevelNullableById(int idLevel)
    {
        return levelLoaded.Find(l => l.idLevel == idLevel);
    }

    public static void addEntityInEntityLevel(int idLevel, Entity entity)
    {
        getLevelById(idLevel).entityInLevel.Add(entity);
    }

    public static void executeAllUpdate()
    {
        levelLoaded.ForEach(l => {
            if(l.isActive)
                l.update();
        });
    }

    private static bool _isTransitionActive;
    public static bool isTransitionActive{
        get{ return _isTransitionActive; }
    }
    private static int[] idLevelStartTransition = new int[]{};
    private static int[] idLevelEndTransition = new int[]{};
    private static int timeWhenStartTransition;
    public static float transitionOpacity;
    public static Sprite blackOpacityRamp = new Sprite("assets/Level/blackOpacityRamp.png", new(1, 1));
    private static Action? midTransitionAction;
    public static void transitionLevel(int idLevelStart, int idLevelEnd)
    {
        transitionLevel(new int[]{ idLevelStart }, new int[]{ idLevelEnd });
    }
    public static void transitionLevel(int[] idLevelStart, int[] idLevelEnd, Action midAction)
    {
        midTransitionAction = midAction;
        transitionLevel(idLevelStart, idLevelEnd);
    }
    private static bool isMidTransitionPast;
    public static void transitionLevel(int[] idLevelStart, int[] idLevelEnd)
    {
        _isTransitionActive = true;
        idLevelStartTransition = idLevelStart;
        idLevelEndTransition = idLevelEnd;
        timeWhenStartTransition = Update.time;
        transitionOpacity = 0f;
        isMidTransitionPast = false;
    }
    public static void updateTransitionLevel()
    {
        if(!isTransitionActive) //skip if no transition active.
            return;

        const float timeOfFullTransition = 1000f; //delay of full transition anime.

        float i = Math.Min((float)(Update.time - timeWhenStartTransition) / timeOfFullTransition, 1f); //interpolation of transation (0f~1f).

        if(i == 1f){ //end transition.
            _isTransitionActive = false;
            transitionOpacity = 0f;
            return;
        }

        if(i >= 0.5f && !isMidTransitionPast){ //mid transition execution.
            isMidTransitionPast = true;
            transitionOpacity = 1f;
            foreach(int idLevelUnActive in idLevelStartTransition){ //disable all level for transition.
                getLevelById(idLevelUnActive).unActive();
            }
            foreach(int idLevelActive in idLevelEndTransition){ //active all level for transition.
                getLevelById(idLevelActive).active();
            }
            if(midTransitionAction != null){ //action in mid transition (if has one).
                midTransitionAction();
                midTransitionAction = null;
            }
            return;
        }

        transitionOpacity = ((i <= 0.5f)? //transition opacity.
            i*2f : //transition opacity 0 to 1.
            1f-(i-0.5f) //transition opacity 1 to 0.
        );

    }

    public static void deinit()
    {
        foreach(Level l in levelLoaded){ //unActive all level active (so free all texture).
            if(!l.isActive)
                continue;
            l.unActive();
        }
    }
    // <--- Level Manager (END) --->

    public int idLevel;
    public string nameLevel = "";
    public bool isLoaded;
    public bool isActive;
    public int timeInLevel;
    public List<Entity> entityInLevel = new List<Entity>();

    public virtual void active()
    {
        timeInLevel = 0;

        Level.activeALevel(this);
    }

    public virtual void update()
    {
        timeInLevel += (int)(Update.deltaTime * 1000);
    }

    public virtual void unActive()
    {
        Level.unActiveALevel(this);
    }



    public T getEntityInLevel<T>(int idEntity) where T : Entity
    {
        return (T)(entityInLevel.Find(e => e.idEntity == idEntity && e is T) ?? throw new Exception("error : entity not found"));
    }

}

/* <--- example of level child --->

public class level1 : Level
{

    public static level1 level = new level1(){ nameLevel="level1" };

    public static void load(){ Level.loadALevel(level); }

    public static void activeStatic(){ level.active(); }
    public override void active()
    {

        //... instantie entities.

        base.active();

    }

    //... entities and data used in update.

    public static void updateStatic(){ level.update(); }
    public override void update()
    {

        //... move, colide.

        base.update();

    }

    public static void unActiveStatic(){ level.unActive(); }
    public override void unActive()
    {

        //... clean entities store for update.

        base.unActive();

    }

}

*/