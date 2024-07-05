
public class Grass : Entity
{
    public static Sprite? spriteStatic;

    public Grass(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/Grass/Grass.png", //path file sprite.
                new Vector2(512, 512) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.state = "base";
        this.zIndex = 1000;
    }

    public static void replace(List<List<Grass>> listGrassAroundPlayer)
    {
        Vector2 posCam = Camera.pos;
        listGrassAroundPlayer.ForEach(lg => {
            lg.ForEach(g => {
                float difX = Camera.pos.x + Window.size.x/2 - g.pos.x;
                float fifY = Camera.pos.y + Window.size.y/2 - g.pos.y;

                if(difX > 1024){
                    g.pos.x += 2048;
                }else if(difX < -1024){
                    g.pos.x -= 2048;
                }

                if(fifY > 768){
                    g.pos.y += 1536;
                }else if(fifY < -768){
                    g.pos.y -= 1536;
                }
            });
        });
        //Camera.pos.
    }
}