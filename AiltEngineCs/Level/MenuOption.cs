
using Raylib_cs;

public class MenuOption : Level
{

    public static MenuOption level = new MenuOption(){ nameLevel="level1" };

    public static void load(){ Level.loadALevel(level); }

    public static void activeStatic(){ level.active(); }
    public override void active()
    {
        //button back village (close menuOption).
        NavigateUi navBackButton = new NavigateUi(level.idLevel, false);
        navBackButton.pos.setBoth(
            70 +10,
            35 +10
        );
        navBackButton.scale.setBoth(0.5f, 0.5f);
        navBackButton.geometryTrigger = new Geometry[]{
            new Rect(
                new(-70, -35), 
                new(70, 35)
            )
        };
        navBackButton.actionClick = () => {
            Level.transitionLevel(
                new int[]{ MenuOption.level.idLevel },
                new int[]{},
                () => {
                    Village.level.isActive = true;
                }
            );
        };

        //scroll barr (and his button).
        ScrollBarrUi scrollBarrBackground = new ScrollBarrUi(level.idLevel); //scroll barr.
        scrollBarrButton = new ScrollBarrButtonUi(level.idLevel); //button scroll barr.
        Input.isScrollMode = true;
        Input.maxScroll = heightMenu;
        Camera.posScroll = 0f;

        //pos usefull for scrolable environement.
        float xPosStartScrollable = (140 +20) +(Window.size.x - (140 +20) - scrollBarrBackground.size.x - 1024)/2f;
        float yPosCurrent = 0;

        // <--- title save. --->
        OptionTitleUi title = new OptionTitleUi(level.idLevel, "save");
        title.pos.setBoth(
            xPosStartScrollable,
            60f
        );
        yPosCurrent += title.size.y +120f; //marge.

        TextUi labelSave = new TextUi(level.idLevel, "sauvegarder", Color.White, 40); //line with button save (checkbox).
        labelSave.encrage.setBoth(0, 0);
        labelSave.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        CheckBoxUi checkBoxSave = new CheckBoxUi(level.idLevel); //checkbox.
        checkBoxSave.pos.setBoth(
            labelSave.pos.x + 800f +65f,
            labelSave.pos.y + (checkBoxSave.size.y/2f)
        );
        checkBoxSave.actionClick = () => {
            checkBoxSave.state = (MenuSave.save()? "checked": "base");
        };
        checkBoxSave.isAutoBackOff = true;

        labelSave = new TextUi(level.idLevel, "retour main menu", Color.White, 40); //line with button save (checkbox).
        labelSave.encrage.setBoth(0, 0);
        labelSave.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        CheckBoxUi checkBoxBack = new CheckBoxUi(level.idLevel); //checkbox.
        checkBoxBack.pos.setBoth(
            labelSave.pos.x + 800f +65f,
            labelSave.pos.y + (checkBoxBack.size.y/2f)
        );
        checkBoxBack.actionClick = () => {
            Level.transitionLevel(
                new int[]{ MenuOption.level.idLevel, Village.level.idLevel },
                new int[]{ MenuSave.level.idLevel },
                () => {
                    MenuChoosePlayer.level.getPlayer.isActive = false; //hidde player (by security).
                    MenuChoosePlayer.level.player = null; //free player.
                }
            );
        };

        labelSave = new TextUi(level.idLevel, "fermer le jeu", Color.White, 40); //line with button save (checkbox).
        labelSave.encrage.setBoth(0, 0);
        labelSave.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        checkBoxBack = new CheckBoxUi(level.idLevel); //checkbox.
        checkBoxBack.pos.setBoth(
            labelSave.pos.x + 800f +65f,
            labelSave.pos.y + (checkBoxBack.size.y/2f)
        );
        checkBoxBack.actionClick = () => {
            Window.isExit = true;
        };

        yPosCurrent += 60f; //marge.

        // <--- title keyboard. --->
        title = new OptionTitleUi(level.idLevel, "keyboard");
        title.pos.setBoth(
            xPosStartScrollable,
            yPosCurrent
        );
        yPosCurrent += title.size.y +90f; //marge.

        TextUi labelKey = new TextUi(level.idLevel, "touch Haut", Color.White, 40); //key up.
        labelKey.encrage.setBoth(0, 0);
        labelKey.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        ButtonKeyUi buttonKey = new ButtonKeyUi(level.idLevel); //key button.
        buttonKey.pos.setBoth(
            labelKey.pos.x + 800f,
            labelKey.pos.y
        );
        buttonKey.charBasedPress = 'Z';
        buttonKey.charOfKey = Keyboard.charToStringPrintable(Keyboard.remap("customKeyboard", 'Z'));

        labelKey = new TextUi(level.idLevel, "touch Gauche", Color.White, 40); //key left.
        labelKey.encrage.setBoth(0, 0);
        labelKey.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        buttonKey = new ButtonKeyUi(level.idLevel); //key button.
        buttonKey.pos.setBoth(
            labelKey.pos.x + 800f,
            labelKey.pos.y
        );
        buttonKey.charBasedPress = 'Q';
        buttonKey.charOfKey = Keyboard.charToStringPrintable(Keyboard.remap("customKeyboard", 'Q'));

        labelKey = new TextUi(level.idLevel, "touch Bas", Color.White, 40); //key down.
        labelKey.encrage.setBoth(0, 0);
        labelKey.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        buttonKey = new ButtonKeyUi(level.idLevel); //key button.
        buttonKey.pos.setBoth(
            labelKey.pos.x + 800f,
            labelKey.pos.y
        );
        buttonKey.charBasedPress = 'S';
        buttonKey.charOfKey = Keyboard.charToStringPrintable(Keyboard.remap("customKeyboard", 'S'));

        labelKey = new TextUi(level.idLevel, "touch Droite", Color.White, 40); //key right.
        labelKey.encrage.setBoth(0, 0);
        labelKey.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        buttonKey = new ButtonKeyUi(level.idLevel); //key button.
        buttonKey.pos.setBoth(
            labelKey.pos.x + 800f,
            labelKey.pos.y
        );
        buttonKey.charBasedPress = 'D';
        buttonKey.charOfKey = Keyboard.charToStringPrintable(Keyboard.remap("customKeyboard", 'D'));

        labelKey = new TextUi(level.idLevel, "touch Valider", Color.White, 40); //key right.
        labelKey.encrage.setBoth(0, 0);
        labelKey.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        buttonKey = new ButtonKeyUi(level.idLevel); //key button.
        buttonKey.pos.setBoth(
            labelKey.pos.x + 800f,
            labelKey.pos.y
        );
        buttonKey.charBasedPress = '\n';
        buttonKey.charOfKey = Keyboard.charToStringPrintable(Keyboard.remap("customKeyboard", '\n'));

        labelKey = new TextUi(level.idLevel, "touch Option/Retour", Color.White, 40); //key right.
        labelKey.encrage.setBoth(0, 0);
        labelKey.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        buttonKey = new ButtonKeyUi(level.idLevel); //key button.
        buttonKey.pos.setBoth(
            labelKey.pos.x + 800f,
            labelKey.pos.y
        );
        buttonKey.charBasedPress = '\r';
        buttonKey.charOfKey = Keyboard.charToStringPrintable(Keyboard.remap("customKeyboard", '\r'));

        yPosCurrent += 60f; //marge.

        // <--- title twitch. --->
        title = new OptionTitleUi(level.idLevel, "twitch");
        title.pos.setBoth(
            xPosStartScrollable,
            yPosCurrent
        );
        yPosCurrent += title.size.y +90f; //marge.

        TextUi labelTwitch = new TextUi(level.idLevel, "nom de chaine twitch", Color.White, 40); //line.
        labelTwitch.encrage.setBoth(0, 0);
        labelTwitch.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        InputTextUi inputTextTwitch = new InputTextUi(level.idLevel); //key button.
        inputTextTwitch.pos.setBoth(
            labelTwitch.pos.x + 800f -120f,
            labelTwitch.pos.y
        );
        inputTextTwitch.text = MenuSave.level.getSaveSelected.channelTwitchToConnect;
        inputTextTwitch.saveTextInParams = () => {
            MenuSave.level.getSaveSelected.channelTwitchToConnect = inputTextTwitch.text;
        };

        yPosCurrent += 20f; //marge.

        labelTwitch = new TextUi(level.idLevel, "re-lancer le bot twitch", Color.White, 40); //line.
        labelTwitch.encrage.setBoth(0, 0);
        labelTwitch.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        CheckBoxUi checkBoxTwitch = new CheckBoxUi(level.idLevel); //checkbox.
        checkBoxTwitch.pos.setBoth(
            labelTwitch.pos.x + 800f +65f,
            labelTwitch.pos.y + (checkBoxTwitch.size.y/2f)
        );
        checkBoxTwitch.actionClick = () => {
            checkBoxTwitch.state = (
                BotTwitch.init(MenuSave.level.getSaveSelected.channelTwitchToConnect)? "checked": "base"
            );
        };
        checkBoxTwitch.isAutoBackOff = true;

        yPosCurrent += 20f; //marge.

        labelTwitch = new TextUi(level.idLevel, "commande rejoindre", Color.White, 40); //line.
        labelTwitch.encrage.setBoth(0, 0);
        labelTwitch.pos.setBoth(xPosStartScrollable +30f, yPosCurrent);
        yPosCurrent += 60f;
        InputTextUi inputTextTwitchCmdJoin = new InputTextUi(level.idLevel); //key button.
        inputTextTwitchCmdJoin.pos.setBoth(
            labelTwitch.pos.x + 800f -120f,
            labelTwitch.pos.y
        );
        inputTextTwitchCmdJoin.text = MenuSave.level.getSaveSelected.cmdTwitchJoinVillage;
        inputTextTwitchCmdJoin.saveTextInParams = () => {
            MenuSave.level.getSaveSelected.cmdTwitchJoinVillage = inputTextTwitchCmdJoin.text;
        };



        base.active();

    }

    //... entities and data used in update.
    public const float heightMenu = 720;
    public ScrollBarrButtonUi? scrollBarrButton;
    public ScrollBarrButtonUi getScrollBarrButton{
        get{ return scrollBarrButton ?? throw new Exception("error : scrollBarrButton is null"); }
    }
    public ButtonKeyUi? buttonKeySelected;
    public InputTextUi? inputTextSelected;
    public bool isInputSelected{
        get{ return (buttonKeySelected != null || inputTextSelected != null); }
    }

    public static void updateStatic(){ level.update(); }
    public override void update()
    {
        //back to off button.
        foreach(Entity e in entityInLevel){
            if(!(e is CheckBoxUi))
                continue;
            ((CheckBoxUi)e).backOffCheck();
        }

        //exit menu option if press escape.
        if(Input.hasKeyPressed('\r') && buttonKeySelected == null){
            entityInLevel.Find(e => (e is NavigateUi && e.state.Substring(0, 6) == "retour"))?.mouseClick();
        }

        //move scroll button, based on camera posScroll.
        getScrollBarrButton.pos.y = Math2.lerp(
            getScrollBarrButton.posYMin, 
            getScrollBarrButton.posYMax,
            Camera.posScroll/heightMenu
        );

        //verify button key remap.
        if(buttonKeySelected != null && Input.getInputPressed.Count == 1){
            Keyboard.editKeyValue(buttonKeySelected.charBasedPress, Input.getInputPressed[0].charKey);
            buttonKeySelected.charOfKey = Keyboard.charToStringPrintable(Input.getInputPressed[0].charKey);
            buttonKeySelected = null;
        }
        if(inputTextSelected != null && Input.fullPrompt != inputTextSelected.text){
            if(Input.fullPrompt.Length != 0 && Input.fullPrompt[Input.fullPrompt.Length-1] == '\n'){ //exit input text selection.
                inputTextSelected.saveTextInParams(); //update text in SaveParams.
                inputTextSelected.forceStaySelected = false;
                inputTextSelected.state = "base";
                inputTextSelected = null;
                Input.isPromptMode = false;
            }else{
                inputTextSelected.text = Input.fullPrompt; //update text from prompt.
            }

        }

        base.update();

    }

    public static void unActiveStatic(){ level.unActive(); }
    public override void unActive()
    {

        scrollBarrButton = null;
        buttonKeySelected = null;
        inputTextSelected = null;

        Input.isPromptMode = false;
        Input.fullPrompt = "";

        base.unActive();

    }



}