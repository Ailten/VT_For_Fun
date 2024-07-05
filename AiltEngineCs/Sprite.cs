
using Raylib_cs;

public class Sprite
{
    public static List<Texture2D> allTextures = new();
    public static void deinit(){
        int i = allTextures.Count -1;
        while(i>=0){
            Raylib.UnloadTexture(allTextures[i]);
            i--;
        }
    }
    public static void deinitOneTexture(Texture2D texture){
        Raylib.UnloadTexture(texture); //free the texture (memory).
        allTextures.Remove(texture); //remove texture object form list.
    }

    public Texture2D texture;
    public Vector2 size = new();

    private Dictionary<string, Vector2> tileMap = new(); //state, pos.

    public Sprite(string path, Vector2 size)
    {
        this.texture = Raylib.LoadTexture(path);
        this.size = size;

        allTextures.Add(this.texture);
    }

    public void addATileMap(string state, Vector2 posInTileMap)
    {
        tileMap.Add(state, posInTileMap);
    }

    public Rectangle getRectSource(string state)
    {
        Vector2 posInTileMap = tileMap[state];
        return new Rectangle( //get rectangle source by state.
            posInTileMap.x, posInTileMap.y,
            size.x, size.y
        );
    }
    public Rectangle getRectSource(string state, Vector2 reverceSprite)
    {
        Vector2 posInTileMap = tileMap[state];
        return new Rectangle( //get rectangle source by state.
            posInTileMap.x, posInTileMap.y,
            (float)Math.Round(size.x * reverceSprite.x), (float)Math.Round(size.y * reverceSprite.y)
        );
    }
    public Rectangle getPixelRamp(int x)
    {
        return new Rectangle( //get rectangle source by pos horizontal.
            x, 0,
            size.x, size.y
        );
    }

}