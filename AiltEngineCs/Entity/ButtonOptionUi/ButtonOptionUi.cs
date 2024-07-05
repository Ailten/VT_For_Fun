
public class ButtonOptionUi : Entity
{
    public static Sprite? spriteStatic;
    public static Geometry[]? geometryTriggerStatic;

    public ButtonOptionUi(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/ButtonOptionUi/ButtonOptionUi.png", //path file sprite.
                new Vector2(60, 60) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));

            //set geometry solide.
            geometryTriggerStatic = new Geometry[]{
                new Rect( //rect base.
                    new Vector2(-55, 4), 
                    new Vector2(-5, 56)
                )
            };
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.geometryTrigger = geometryTriggerStatic;
        this.state = "base";
        this.encrage.setBoth(1, 0);
        this.tags = new Tag[]{ Tag.isHud };
    }

    public override void mouseClick()
    {
        this.actionClick();
    }

    public Action actionClick = () => {};

}