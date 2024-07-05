
public class MenuChoosePlayer : Level 
{
    public static MenuChoosePlayer level = new MenuChoosePlayer(){ nameLevel="MenuChoosePlayer" };

    public static void load(){ Level.loadALevel(level); }

    public static void activeStatic(){ level.active(); }
    public override void active()
    {
        //Entity text "Selectionne skin"
        TextUi title = new TextUi(level.idLevel, "Selectionne un skin", Raylib_cs.Color.White);
        title.pos.setBoth(15, 15);
        title.encrage.setBoth(0, 0);
        title.fontSize = 40;

        TextUi explayTxt = new TextUi(level.idLevel, "(zqsd et entrer)", Raylib_cs.Color.White);
        explayTxt.pos.setBoth(15, 70);
        explayTxt.encrage.setBoth(0, 0);
        explayTxt.fontSize = 20;

        //get all skin name (from folder).
        List<string> skinNames = new List<string>(JsonManager.getAllNameFiles("assets/Entity/Skin", "*.png"));
        skinNames.Remove("Viewer"); //exclue Viewer.

        //spawn every skin player.
        for(int i = 0; i<skinNames.Count; i++){
            SkinVillageoi skin = new SkinVillageoi(level.idLevel, skinNames[i]);
            skin.pos.x = 200f * i;
            skin.stats.name = skinNames[i];
            skins.Add(skin);

            if(i == 0){ //first skin.
                Camera.entityFollow = skin;
                Camera.focus();
            }
        }

        //selector (arrow up).
        seletorUi = new SelectorUi(level.idLevel);
        seletorUi.tags = null;
        seletorUi.pos.setBoth(
            skins[0].pos.x, 
            skins[0].pos.y + 100f
        );
        seletorUi.scale.setBoth(0.5f, 0.5f);

        //skin selected.
        indexSkinSelected = 0;

        //nav button back.
        NavigateUi navUi = new NavigateUi(level.idLevel, false); //back button.
        navUi.pos.setBoth(
            (navUi.size.x /2f) + 20f,
            Window.size.y - (navUi.size.y /2f) - 20f
        );
        navUi.actionClick = () => { //function execute when click.
            Level.transitionLevel(
                MenuChoosePlayer.level.idLevel, 
                MenuSave.level.idLevel
            );
        };

        //nav button valide.
        navUi = new NavigateUi(level.idLevel, true); //valid button.
        navUi.pos.setBoth(
            Window.size.x - (navUi.size.x /2f) - 20f,
            Window.size.y - (navUi.size.y /2f) - 20f
        );
        navUi.actionClick = () => { //function execute when click.
            MenuChoosePlayer.level.navUiValid = true;
        };
        navUiValid = false;



        //log bot twitch.
        BotTwitch.init(MenuSave.level.getSaveSelected.channelTwitchToConnect);

        base.active();

    }

    public int indexSkinSelected;
    public List<SkinVillageoi> skins = new();
    public SkinVillageoi? player = null; //not free.
    public SkinVillageoi getPlayer{
        get{ return player ?? throw new Exception("error : player is null"); }
    }
    public SelectorUi? seletorUi;
    public bool navUiValid;

    public static void updateStatic(){ level.update(); }
    public override void update()
    {
        //follow indexSkinSelected
        if(seletorUi != null){
            seletorUi.pos.setBoth(
                Math2.lerp(seletorUi.pos.x, skins[indexSkinSelected].pos.x, 0.06f),
                Math2.lerp(seletorUi.pos.y, skins[indexSkinSelected].pos.y + 50f + 20f * (float)Math.Sin(Update.time * 0.004), 0.06f)
            );
        }

        if(Input.hasKeyPressedNow('Q') && indexSkinSelected != 0){ //input left.
            indexSkinSelected-=1;
            Camera.entityFollow = skins[indexSkinSelected];
        }

        if(Input.hasKeyPressedNow('D') && indexSkinSelected != (skins.Count-1)){ //input right.
            indexSkinSelected+=1;
            Camera.entityFollow = skins[indexSkinSelected];
        }

        if(Input.hasKeyPressedNow('\n') || navUiValid){ //enter.
            navUiValid = false;
            player = skins[indexSkinSelected];

            player.idLevel = Village.level.idLevel; //free from level (set to level village).
            player.stats.name = MenuSave.level.getSaveSelected.name;
            entityInLevel.Remove(player);

            //todo : load pos player and stats in save.
            Level.transitionLevel(MenuChoosePlayer.level.idLevel, Village.level.idLevel);
        }

        base.update();

    }

    public static void unActiveStatic(){ level.unActive(); }
    public override void unActive()
    {

        //free texture of skin not selected.
        foreach(SkinVillageoi sv in skins){
            if(player != null && sv.idEntity == player.idEntity) //not free texture of skin selected.
                continue;

            sv.freeTextureSkin(); //free texture of skin (warning, not call if an other entity use again this skin).
        }

        skins = new();
        seletorUi = null;

        Camera.entityFollow = null;

        base.unActive();

    }

}