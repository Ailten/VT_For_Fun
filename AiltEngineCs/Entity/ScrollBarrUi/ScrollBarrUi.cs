
public class ScrollBarrUi : Entity
{

    public static Sprite? spriteStatic;

    public ScrollBarrUi(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/ScrollBarrUi/ScrollBarrUi.png", //path file sprite.
                new Vector2(50, 720) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.state = "base";
        this.tags = new Tag[]{ Tag.isHud };

        this.pos.setBoth( //default pos, right of screen.
            Window.size.x - (this.size.x/2f),
            Window.size.y/2f
        );
    }
}