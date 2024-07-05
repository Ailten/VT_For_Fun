
using Raylib_cs;

public class InputTextUi : Entity
{

    public static Sprite? spriteStatic;
    public static Geometry[]? geometryTriggerStatic;

    public InputTextUi(int idLevel) : base(idLevel)
    {
        if(spriteStatic == null){
            spriteStatic = new Sprite(
                "assets/Entity/InputTextUi/InputTextUi.png", //path file sprite.
                new Vector2(250, 60) //size of one tile.
            );

            //set tile map in sprite.
            spriteStatic.addATileMap("base", new Vector2(0, 0));
            spriteStatic.addATileMap("selected", new Vector2(0, spriteStatic.size.y));

            //set geometry solide.
            geometryTriggerStatic = new Geometry[]{
                new Rect( //rect base.
                    new Vector2(5, 5), 
                    new Vector2(245, 55)
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

    public string text = "";

    public override void addDraw(Vector2 posEncrageInCanvas)
    {
        Raylib_cs.Font font = Font.getFont("IntensaFuente");

        string textToPrint = (text!=""? text: "(vide)");

        System.Numerics.Vector2 sizeTextureTxt = Raylib.MeasureTextEx(
            font,
            textToPrint,
            25 *Window.scaleCanvasWindow,
            2 *Window.scaleCanvasWindow
        );

        Raylib.DrawTextEx(
            font, //font.
            textToPrint, //txt.
            new System.Numerics.Vector2( //pos in canvas.
                posEncrageInCanvas.x + (size.x/2f) *Window.scaleCanvasWindow - sizeTextureTxt.X/2f, 
                posEncrageInCanvas.y + (size.y/2f -12f) * Window.scaleCanvasWindow
            ), 
            25 *Window.scaleCanvasWindow, //font size.
            2 *Window.scaleCanvasWindow, //space between two letter.
            (text!=""? Color.White: Color.Gray) //color.
        );
    }

    public bool forceStaySelected;

    private int timeLastClick;
    private const int timeBetweenClick = 1000;

    public override void mouseClick()
    {
        if(Update.time - timeLastClick <= timeBetweenClick)
            return;

        timeLastClick = Update.time;

        if(MenuOption.level.inputTextSelected != null && MenuOption.level.inputTextSelected.idEntity == this.idEntity){
            Input.fullPrompt += '\n'; //if re-click on selected, same action as push enter on prompt mode.
        }

        if(MenuOption.level.isInputSelected) //skip selection if an other input was selected.
            return;

        MenuOption.level.inputTextSelected = this;
        this.state = "selected";
        Input.fullPrompt = this.text;
        Input.isPromptMode = true;
        this.forceStaySelected = true;
    }

    public override void mouseEnter()
    {
        this.state = "selected";
    }

    public override void mouseExit()
    {
        if(this.forceStaySelected)
            return;

        this.state = "base";
    }


    public Action saveTextInParams = () => {};

}