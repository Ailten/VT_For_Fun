
using System.Drawing;
using System.Text.RegularExpressions;

public class MenuChooseName : Level
{

    public static MenuChooseName level = new MenuChooseName(){ nameLevel="MenuChooseName" };

    public static void load(){ Level.loadALevel(level); }

    public static void activeStatic(){ level.active(); }
    public override void active()
    {
        //active prompt input mode.
        Input.isPromptMode = true;
        Input.fullPrompt = "";

        //title.
        TextUi title = new TextUi(level.idLevel, "Entre un nom", Raylib_cs.Color.White);
        title.pos.setBoth(15, 15);
        title.encrage.setBoth(0, 0);
        title.fontSize = 40;
        idEntityTitle = title.idEntity;

        //Entity button back enter name.
        NavigateUi navUi = new NavigateUi(level.idLevel, false); //back button.
        navUi.pos.setBoth(
            (navUi.size.x /2f) + 20f,
            Window.size.y - (navUi.size.y /2f) - 20f
        );
        posBaseEntityUi.Add(navUi.idEntity, new Vector2(navUi.pos));
        navUi.actionClick = () => { //function execute when click.
            Level.transitionLevel(MenuChooseName.level.idLevel, MenuSave.level.idLevel);
        };

        //Entity button valide enter name.
        navUi = new NavigateUi(level.idLevel, true); //valid button.
        navUi.pos.setBoth(
            Window.size.x - (navUi.size.x /2f) - 20f,
            Window.size.y - (navUi.size.y /2f) - 20f
        );
        posBaseEntityUi.Add(navUi.idEntity, new Vector2(navUi.pos));
        navUi.actionClick = () => { //function execute when click.
        
            //get text enter.
            TextUi TextUiEnter = MenuChooseName.level.getEntityInLevel<TextUi>(MenuChooseName.level.idEnityEnterName);

            //verify name format.
            if(!Regex.Match(TextUiEnter.txt, "[ -~]{1,8}").Success){
                Entity entityTitle = MenuChooseName.level.entityInLevel.Find(e => e.idEntity == MenuChooseName.level.idEntityTitle) ?? throw new Exception("error : idEntityTitle is not find");
                if(!(entityTitle is TextUi))
                    throw new Exception("error : entityTitle is not TextUi");
                TextUi textUiTitle = (TextUi)entityTitle;
                textUiTitle.txt = "1 a 8 caractères !";
                textUiTitle.color = Raylib_cs.Color.Red;
                return;
            }
            
            MenuSave.level.getSaveSelected.name = TextUiEnter.txt; //set text enter to save selected.
            JsonManager.saveFileJson($"assets/saves/{MenuSave.level.getSaveSelected.id}/save.json", MenuSave.level.getSaveSelected); //save with name.

            Level.transitionLevel(MenuChooseName.level.idLevel, MenuChoosePlayer.level.idLevel);
        };

        //title for choose name.
        TextUi chooseName = new TextUi(level.idLevel, "", Raylib_cs.Color.White);
        chooseName.pos.setBoth(Window.size.x /2f, Window.size.y /2f);
        chooseName.fontSize = 70;
        posBaseEntityUi.Add(chooseName.idEntity, new Vector2(chooseName.pos));
        idEnityEnterName = chooseName.idEntity;

        base.active();
    }

    public Dictionary<int, Vector2> posBaseEntityUi = new();
    public int idEntityTitle;
    public int idEnityEnterName;

    public static void updateStatic(){ level.update(); }
    public override void update()
    {

        //moving button navigateUi.
        entityInLevel.ForEach(e => {
            if(e is NavigateUi || e.idEntity == idEnityEnterName){
            
                //move of saveUI.
                double time = (double)Update.time;
                double decalageRotate = ((double)(e.idEntity % 3) /3) *Math.PI*2;

                //apply pos rotation.
                e.pos.x = posBaseEntityUi[e.idEntity].x + 8f * (float)Math.Sin(time * 0.002 + decalageRotate);
                e.pos.y = posBaseEntityUi[e.idEntity].y + 8f * (float)Math.Cos(time * 0.002 + decalageRotate);

            }
        });

        //verify user enter a key input and re-afect to textUi.
        TextUi textUiEnterName = getEntityInLevel<TextUi>(idEnityEnterName);
        if(textUiEnterName.txt != Input.fullPrompt){
            if(Input.fullPrompt.Length != 0 && Input.fullPrompt[Input.fullPrompt.Length-1] == '\n'){
                entityInLevel.Find(e => (e is NavigateUi && e.state.Substring(0, 8) == "validate"))?.mouseClick();
            }else{
                textUiEnterName.txt = Input.fullPrompt;
            }
        }

        base.update();
    }

    public static void unActiveStatic(){ level.unActive(); }
    public override void unActive()
    {
        posBaseEntityUi = new();
        idEntityTitle = 0;
        idEnityEnterName = 0;

        Input.isPromptMode = false; //disable input prompt mode.

        base.unActive();
    }

}