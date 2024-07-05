
using System.Data;
using System.Formats.Asn1;
using Raylib_cs;

public class Entity
{
    //*// <--- Entity Manager (START) --->
    public static List<Entity> entities = new List<Entity>();

    public static Entity? getEntityById(int idEntity)
    {
        return entities.Find(e => e.idEntity == idEntity);
    }

    public static void SortAllEntities()
    {
        entities.Sort((a, b) => a.zIndex.CompareTo(b.zIndex));
    }
    public static void SortAllEntitiesByYAxis()
    {
        entities.Sort((a, b) => {
            if(a.zIndex != b.zIndex) //store by zIndex.
                return a.zIndex.CompareTo(b.zIndex);
            return a.pos.y.CompareTo(b.pos.y); //if same zIndex, store by y pos.
        });
    }
    //*// <--- Entity Manager (END) --->


    public int idEntity;
    public int idLevel;
    public bool isActive = true;

    public Vector2 pos = new();
    public string state = "";
    public Sprite? sprite;
    public Vector2 size{ get{ return (sprite!=null)? sprite.size: new(); } }
    public Vector2 encrage = new(0.5f, 0.5f);
    public Vector2 reverceSprite = new(1f, 1f);
    public Vector2 scale = new(1f, 1f);
    public float rotate = 0.0f;
    public int zIndex = 2000;

    public Geometry[]? geometrySolid;
    public Geometry[]? geometryTrigger;
    public Tag[]? tags;

    public virtual void mouseEnter(){}
    public virtual void mouseExit(){}
    public virtual void mouseClick(){}

    public virtual void addDraw(Vector2 posEncrageInCanvas){}

    public Entity(int idLevel)
    {
        this.idLevel = idLevel; //set id level.
        this.idEntity = getAnIdEntityValid(); //set id entity.
        Entity.entities.Add(this); //add in entities.
        Level.addEntityInEntityLevel(idLevel, this); //add in entities level.
    }

    public Entity() //instancie an entity without level.
    {
        this.idLevel = -1;
        this.idEntity = getAnIdEntityValid();
        Entity.entities.Add(this);
    }

    private int getAnIdEntityValid()
    {
        int i=0;
        IEnumerable<int> allIdEntity = Entity.entities.Select(e => e.idEntity);
        while(true){
            if(allIdEntity.Contains(i)){
                i++;
                continue;
            }
            return i;
        }
    }

    public bool hasTag(Tag tag)
    {
        if(this.tags == null)
            return false;
        return this.tags.Contains(tag);
    }

    public bool isLevelActive{ 
        get{
            Level? l = Level.getLevelNullableById(this.idLevel);
            if(l == null)
                return true;
            return l.isActive;
        } 
    }

}

public enum Tag{
    isMouse,
    isHud,
    isHudScrollable,
    isPlayer,
    isMob,
    isVillageoi
}

/* <--- example of entity child --->

public class EntityChild : Entity
{
    public static Sprite? spriteStatic;

    public NewEntity(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/EntityChild/EntityChild.png", //path file sprite.
                new Vector2(100, 100) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
            spriteStatic.addATileMap("base2", new Vector2(0, spriteStatic.size.x));
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.state = "base";
    }

}

*/