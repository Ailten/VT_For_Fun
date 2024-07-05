
using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using Raylib_cs;

public class MenuSave : Level
{

    public static MenuSave level = new MenuSave(){ nameLevel="MenuSave" };

    public static void load(){ Level.loadALevel(level); }

    public static void activeStatic(){ level.active(); }
    public override void active()
    {

        //build 3 objects entity for button saves ui (load or delete).
        for(int i=1; i<=3; i++){

            //create Entity saveUi.
            SaveUi saveUi = new SaveUi(level.idLevel);
            saveUi.pos.setBoth( //set pos.
                (Window.size.x/2f) + (saveUi.size.x + 30) *(i-2),
                (Window.size.y/2f) + 50*(3-i)
            );
            posBaseSaveUi.Add(saveUi.idEntity, new Vector2(saveUi.pos));

            //load list of file saves.
            saves.Add(
                saveUi.idEntity,
                JsonManager.loadFileJson<SaveParams>($"assets/saves/{i}/save.json") ?? 
                new SaveParams()
            );

            //save file if default or edit from folder.
            if(saves[saveUi.idEntity].id != i){
                saves[saveUi.idEntity].id = i;

                JsonManager.saveFileJson($"assets/saves/{i}/save.json", saves[saveUi.idEntity]);
            }

        }
        
        //Entity text "choose save ..."
        TextUi title = new TextUi(level.idLevel, "Selectionne une save", Raylib_cs.Color.White);
        title.pos.setBoth(15, 15);
        title.encrage.setBoth(0, 0);
        title.fontSize = 40;

        //clean save selected (if user return in menu save).
        saveSelected = null;

        base.active();

    }

    public Dictionary<int, SaveParams> saves = new();
    public Dictionary<int, Vector2> posBaseSaveUi = new();
    public SaveParams? saveSelected; //not clean when unActive Level (but in active), because used as params in the game.
    public SaveParams getSaveSelected{
        get{ return saveSelected ?? throw new Exception("error : saveSelected is null"); }
    }

    public static void updateStatic(){ level.update(); }
    public override void update()
    {

        //update select a save.
        entityInLevel.ForEach(e => {
            if(e is SaveUi){

                //move of saveUI.
                double time = (double)Update.time;
                double decalageRotate = ((double)(e.idEntity % saves.Count) /saves.Count) *Math.PI*2;
        
                e.pos.x = posBaseSaveUi[e.idEntity].x + 8f * (float)Math.Sin(time * 0.002 + decalageRotate);
                e.pos.y = posBaseSaveUi[e.idEntity].y + 8f * (float)Math.Cos(time * 0.002 + decalageRotate);

                //change type saveUI selection (selected - del).
                if(e.state == "selected" || e.state == "del"){

                    Vector2 posEInCanvas = new Vector2(e.pos); //pos e in canvas.
                    Window.resizePosCanvasInto(ref posEInCanvas);

                    bool isMouseInDelTrigger = (
                        e.geometryTrigger != null &&
                        e.geometryTrigger[1] is Rect &&
                        Colide.posIsInRect(Mouse.pos, posEInCanvas, (Rect)(e.geometryTrigger[1]))
                    );

                    if((e.state == "selected") == isMouseInDelTrigger){ //switch mode (selected - del).
                        e.state = (e.state == "selected")? "del": "selected";
                    }

                }

            }
        });

        base.update();

    }

    public static void unActiveStatic(){ level.unActive(); }
    public override void unActive()
    {
        saves = new();
        posBaseSaveUi = new();

        base.unActive();
    }


    public static bool save()
    {
        //get current obj save.
        SaveParams save = MenuSave.level.getSaveSelected;

        //edit time played in save.
        int timePlayed = Update.time - Update.timeWhenLastSave;
        save.timeTotalInGame = save.timeTotalInGame.Add(TimeSpan.FromMilliseconds(timePlayed));
        Update.timeWhenLastSave = Update.time;

        //save custom keyboard (if a custome one).
        if(save.keyboard == "customKeyboard"){
            save.customKeyboard = Keyboard.getCustomKeyboard();
        }

        //save all villageoi (stats).
        save.villageoiStats = Village.level.villageois.Select((v) => {
            v.makeStatsValideForSave(); //encapsul params in stats.
            return v.stats;
        }).ToArray();

        //save file json.
        return JsonManager.saveFileJson<SaveParams>($"assets/saves/{save.id}/save.json", save);
    }

}