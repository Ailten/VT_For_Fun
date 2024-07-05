
using Raylib_cs;

public class ButtonKeyUi : Entity
{

    public static Sprite? spriteStatic;
    public static Geometry[]? geometryTriggerStatic;

    public ButtonKeyUi(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/ButtonKeyUi/ButtonKeyUi.png", //path file sprite.
                new Vector2(124, 60) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
            spriteStatic.addATileMap("selected", new Vector2(0, spriteStatic.size.y));

            //set geometry solide.
            geometryTriggerStatic = new Geometry[]{
                new Rect( //rect base.
                    new Vector2(5, 2), 
                    new Vector2(124, 58)
                )
            };
        }

        //set params of entity.
        this.sprite = spriteStatic;
        this.geometryTrigger = geometryTriggerStatic;
        this.state = "base";
        this.encrage.setBoth(0, 0);
        this.tags = new Tag[]{ Tag.isHud, Tag.isHudScrollable };
    }

    public char charBasedPress = '_';
    public string charOfKey = "";

    public override void addDraw(Vector2 posEncrageInCanvas)
    {
        Raylib_cs.Font font = Font.getFont("IntensaFuente");

        System.Numerics.Vector2 sizeTextureTxt = Raylib.MeasureTextEx(
            font,
            charOfKey,
            25 *Window.scaleCanvasWindow,
            2 *Window.scaleCanvasWindow
        );

        Raylib.DrawTextEx(
            font, //font.
            charOfKey, //txt.
            new System.Numerics.Vector2( //pos in canvas.
                posEncrageInCanvas.x + (size.x/2f) *Window.scaleCanvasWindow - sizeTextureTxt.X/2f, 
                posEncrageInCanvas.y + (size.y/2f -12f) * Window.scaleCanvasWindow
            ), 
            25 *Window.scaleCanvasWindow, //font size.
            2 *Window.scaleCanvasWindow, //space between two letter.
            Color.White //color.
        );
    }

    public override void mouseClick()
    {
        if(MenuOption.level.isInputSelected) //skip selection if an other input was selected.
            return;

        if(MenuSave.level.getSaveSelected.keyboard != "customKeyboard"){
            Keyboard.makeACustomKeyboard();
            MenuSave.level.getSaveSelected.keyboard = "customKeyboard";
        }

        MenuOption.level.buttonKeySelected = this;
        this.charOfKey = "?";
    }

    public override void mouseEnter()
    {
        this.state = "selected";
    }

    public override void mouseExit()
    {
        this.state = "base";
    }

}