
public class CheckBoxUi : Entity
{
    public static Sprite? spriteStatic;
    public static Geometry[]? geometryTriggerStatic;

    public CheckBoxUi(int idLevel, bool isChecked=false) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/CheckBoxUi/CheckBoxUi.png", //path file sprite.
                new Vector2(60, 60) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
            spriteStatic.addATileMap("checked", new Vector2(spriteStatic.size.x, 0));

            //set geometry solide.
            geometryTriggerStatic = new Geometry[]{
                new Rect( //rect base.
                    new Vector2(-25, -25), 
                    new Vector2(25, 25)
                )
            };
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.geometryTrigger = geometryTriggerStatic;
        this.state = (isChecked? "checked": "base");
        this.tags = new Tag[]{ Tag.isHud, Tag.isHudScrollable };
    }

    private int timeLastClick;
    private const int timeBetweenTwoClick = 1000;
    public bool isAutoBackOff;

    public void backOffCheck()
    {
        if(
            !this.isAutoBackOff ||
            this.state == "base" ||
            Update.time - this.timeLastClick <= timeBetweenTwoClick
        )
            return;

        this.state = "base";
    }

    public override void mouseClick()
    {
        if(Update.time - this.timeLastClick <= timeBetweenTwoClick) //enable many click in same seconde.
            return;

        this.timeLastClick = Update.time;
        this.state = (this.state=="base"? "checked": "base");
        this.actionClick();
    }

    public Action actionClick = () => {};
}