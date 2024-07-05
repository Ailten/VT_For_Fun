
public class SelectorUi : Entity
{
    
    public static Sprite? spriteStatic;

    public SelectorUi(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/SelectorUi/SelectorUi.png", //path file sprite.
                new Vector2(128, 128) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.state = "base";
        this.encrage.y = 0f;
        this.tags = new Tag[]{ Tag.isHud };
    }

}