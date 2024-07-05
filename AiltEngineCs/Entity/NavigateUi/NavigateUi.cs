
using System.Text.RegularExpressions;
using Raylib_cs;

public class NavigateUi : Entity
{

    public static Sprite? spriteStatic;
    public static Geometry[]? geometryTriggerStatic;

    public NavigateUi(int idLevel, bool stateValidate) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/NavigateUi/NavigateUi.png", //path file sprite.
                new Vector2(305, 162) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("retourBase", new Vector2(0, 0));
            spriteStatic.addATileMap("retourSelected", new Vector2(0, spriteStatic.size.y));
            spriteStatic.addATileMap("validateBase", new Vector2(spriteStatic.size.x, 0));
            spriteStatic.addATileMap("validateSelected", new Vector2(spriteStatic.size.x, spriteStatic.size.y));
            
            //set geometry solide.
            geometryTriggerStatic = new Geometry[]{
                new Rect( //rect base.
                    new Vector2(-140, -70), 
                    new Vector2(140, 70)
                )
            };

        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.geometryTrigger = geometryTriggerStatic;
        this.state = (stateValidate? "validate": "retour") +"Base";
        this.tags = new Tag[]{ Tag.isHud };
    }
    
    public override void mouseEnter()
    {
        this.state = Regex.Replace(this.state, "Base$", "Selected");
    }
    public override void mouseExit()
    {
        this.state = Regex.Replace(this.state, "Selected$", "Base");
    }

    private int timeLastClick;
    private const int timeBetweenTwoClick = 1000;

    public override void mouseClick()
    {
        if(Update.time - this.timeLastClick <= timeBetweenTwoClick)
            return;
        
        this.timeLastClick = Update.time;
        
        this.actionClick();
    }

    public Action actionClick = () => {};

}