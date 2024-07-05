
public class ScrollBarrButtonUi : Entity
{
    public static Sprite? spriteStatic;

    public float posYMin, posYMax;

    public ScrollBarrButtonUi(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/ScrollBarrUi/ScrollBarrButtonUi.png", //path file sprite.
                new Vector2(50, 50) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.state = "base";
        this.zIndex = 2100; //draw 100 upper scroll barr background.
        this.tags = new Tag[]{ Tag.isHud };

        //min and max pos in scroll barr background.
        this.posYMin = 25f;
        this.posYMax = Window.size.y - 25f;

        this.pos.setBoth( //default pos, up right of screen.
            Window.size.x - 25f,
            this.posYMin
        );
    }

}