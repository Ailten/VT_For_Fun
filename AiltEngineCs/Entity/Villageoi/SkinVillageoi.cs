
using Raylib_cs;

public class SkinVillageoi : Villageoi
{

    public static Dictionary<string, Sprite> skinStatic = new();

    public const float speedWalk = 180f;

    public string skinName = "";

    public SkinVillageoi(int idLevel, string skinName) : base(idLevel)
    {
        if(!skinStatic.ContainsKey(skinName)){
            skinStatic.Add(skinName, new Sprite(
                $"assets/Entity/Skin/{skinName}.png", //path file sprite.
                new Vector2(133, 167) //size of one tile.
            ));

            //set tile map in sprite.
            skinStatic[skinName].addATileMap("base", new Vector2(0, 0));
            skinStatic[skinName].addATileMap("walk0", new Vector2(skinStatic[skinName].size.x, 0));
            skinStatic[skinName].addATileMap("walk1", new Vector2(skinStatic[skinName].size.x * 2, 0));
            skinStatic[skinName].addATileMap("walk2", new Vector2(skinStatic[skinName].size.x * 3, 0));
            skinStatic[skinName].addATileMap("walk3", new Vector2(skinStatic[skinName].size.x * 4, 0));
        }

        //set params entity.
        this.sprite = skinStatic[skinName];
        this.skinName = skinName;
    }


    public void freeTextureSkin()
    {
        Sprite.deinitOneTexture(skinStatic[skinName].texture); //remove texture from allTextures (and free texture memory).
        skinStatic.Remove(skinName); //remove from dictionary SkinVillageoi.
    }
}