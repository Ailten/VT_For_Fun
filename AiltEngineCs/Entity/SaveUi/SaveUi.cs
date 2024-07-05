
using Raylib_cs;

public class SaveUi : Entity
{

    public static Sprite? spriteStatic;
    public static Geometry[]? geometryTriggerStatic;

    public SaveUi(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/SaveUi/SaveUi.png", //path file sprite.
                new Vector2(387, 473) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
            spriteStatic.addATileMap("selected", new Vector2(spriteStatic.size.x, 0));
            spriteStatic.addATileMap("del", new Vector2(spriteStatic.size.x * 2, 0));

            //set geometry solide.
            geometryTriggerStatic = new Geometry[]{
                new Rect( //rect base.
                    new Vector2(-181, -224), 
                    new Vector2(181, 227)
                ),
                new Rect( //rect del.
                    new Vector2(-168, 134),
                    new Vector2(-94, 208)
                )
            };
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.geometryTrigger = geometryTriggerStatic;
        this.state = "base";
        this.tags = new Tag[]{ Tag.isHud };
    }

    public override void addDraw(Vector2 posEncrageInCanvas)
    {
        Raylib_cs.Font font = Font.getFont("IntensaFuente");

        //draw text name.
        string name = MenuSave.level.saves[this.idEntity].name;
        Raylib.DrawTextEx(
            font, //font.
            (name != "")? name: "______", //txt.
            new System.Numerics.Vector2( //pos in canvas.
                posEncrageInCanvas.x - 160 * Window.scaleCanvasWindow, 
                posEncrageInCanvas.y - 125 * Window.scaleCanvasWindow
            ), 
            25f *Window.scaleCanvasWindow, //font size.
            2f *Window.scaleCanvasWindow, //space between two letter.
            Color.White //color.
        );

        //draw text name.
        string time = MenuSave.level.saves[this.idEntity].getTimeTotalInGameStr();
        Raylib.DrawTextEx(
            font, //font.
            time, //txt.
            new System.Numerics.Vector2( //pos in canvas.
                posEncrageInCanvas.x - 160 * Window.scaleCanvasWindow, 
                posEncrageInCanvas.y - 5 * Window.scaleCanvasWindow
            ), 
            25f *Window.scaleCanvasWindow, //font size.
            2f *Window.scaleCanvasWindow, //space between two letter.
            Color.White //color.
        );
    }

    public override void mouseEnter()
    {
        this.state = "selected";
    }
    public override void mouseExit()
    {
        this.state = "base";
    }

    public override void mouseClick()
    {
        //delete the save selected.
        if(this.state == "del"){

            //erase save with an empty one, and save in file.
            int saveId = MenuSave.level.saves[this.idEntity].id;
            MenuSave.level.saves[this.idEntity] = new SaveParams(){ id=saveId };
            JsonManager.saveFileJson<SaveParams>($"assets/saves/{saveId}/save.json", MenuSave.level.saves[this.idEntity]);
            
            return;
        }

        //load the save selected.
        MenuSave.level.saveSelected = MenuSave.level.saves[this.idEntity];

        //load custom keyboard.
        if(MenuSave.level.saveSelected.customKeyboard != null){
            Keyboard.loadKeyboard(MenuSave.level.saveSelected.customKeyboard);
        }

        if(MenuSave.level.saveSelected.name == ""){ //select a new save.
            Level.transitionLevel(MenuSave.level.idLevel, MenuChooseName.level.idLevel);
            return;
        }

        //load a save.
        Level.transitionLevel(MenuSave.level.idLevel, MenuChoosePlayer.level.idLevel);

    }

}