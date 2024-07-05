
public class OptionTitleUi : Entity
{
    public static Sprite? spriteStatic;

    public OptionTitleUi(int idLevel, string state) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/OptionTitleUi/OptionTitleUi.png", //path file sprite.
                new Vector2(1024, 60) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("keyboard", new Vector2(0, 0));
            spriteStatic.addATileMap("twitch", new Vector2(0, spriteStatic.size.y));
            spriteStatic.addATileMap("save", new Vector2(0, spriteStatic.size.y *2));
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.state = state;
        this.encrage.setBoth(0, 0);
        this.tags = new Tag[]{ Tag.isHud, Tag.isHudScrollable };
    }
}