
public class Village : Level
{

    public static Village level = new Village(){ nameLevel="Village" };

    public static void load(){ Level.loadALevel(level); }

    public static void activeStatic(){ level.active(); }
    public override void active()
    {

        //reset pos player and focus camera.
        MenuChoosePlayer.level.getPlayer.pos.setBoth(0, 0);
        Camera.entityFollow = MenuChoosePlayer.level.getPlayer;
        Camera.focus();

        //load all villageoi from save.
        foreach(Stats villageoiStats in MenuSave.level.getSaveSelected.villageoiStats){
            Villageoi villageoiLoad = new Villageoi(level.idLevel);
            villageoiLoad.loadStatsFromSave(villageoiStats);
            villageois.Add(villageoiLoad);
            
        }

        //arch for spawner villageoi.
        villageoiSpawner = new ArchSpawner(level.idLevel);
        villageoiSpawner.pos.setBoth(0, -300);

        //manage update order entity Y axis.
        timeLastOrder = timeInLevel;

        //bouton option.
        buttonOption = new ButtonOptionUi(level.idLevel);
        buttonOption.pos.setBoth(
            Window.size.x - 15,
            15
        );
        buttonOption.actionClick = () => {
            Level.transitionLevel(
                new int[]{}, 
                new int[]{ MenuOption.level.idLevel }, //todo..
                () => { 
                    Village.level.isActive = false; 
                }
            );
        };

        //init grass.
        const int grassSizeX = 512;
        const int grassSizeY = 512;
        const int grassLengthX = 4;
        const int grassLengthY = 3;
        for(int x=0; x<grassLengthX; x++){
            listGrassAroundPlayer.Add(new());
            for(int y=0; y<grassLengthY; y++){
                Grass g = new Grass(level.idLevel);
                g.pos.setBoth(
                    ((grassLengthX-1) * grassSizeX * ((x/(float)(grassLengthX-1)) - 0.5f) ),
                    ((grassLengthY-1) * grassSizeY * ((y/(float)(grassLengthY-1)) - 0.5f) )
                );
                listGrassAroundPlayer[x].Add(g);
            }
        }

        //sanglier spawner.
        ArchMobSpawner mobSpawner = new ArchMobSpawner(level.idLevel);
        mobSpawner.pos.setBoth(-500, 200);
        mobSpawner.mobTypeToSpawn = "sanglier";
        mobSpawners.Add(mobSpawner);

        base.active();

    }

    //... entities and data used in update.
    public List<Villageoi> villageois = new();
    private ArchSpawner? villageoiSpawner;
    private ArchSpawner getVillageoiSpawner{
        get{ return villageoiSpawner ?? throw new Exception("error villageoiSpawner is null"); }
    }
    private const int timeBetweenTwoOrder = 600;
    private int timeLastOrder;
    private ButtonOptionUi? buttonOption;
    private List<List<Grass>> listGrassAroundPlayer = new();
    private List<ArchMobSpawner> mobSpawners = new();

    public static void updateStatic(){ level.update(); }
    public override void update()
    {
        //do update mob spawner. --->
        mobSpawners.ForEach(ms => {
            ms.checkSpawn(); //check for new spawn mob.
            ms.mobSpawned.ForEach(m => m.update()); //update mobs.
        });

        //replace grass. --->
        Grass.replace(listGrassAroundPlayer);

        //check for new spawn villageoi. --->
        if(getVillageoiSpawner.isCheckTime()){

            foreach(string nameViewer in BotTwitch.pseudosViewerWaitingList){ //spawn villageoi based on name stock in BotTwitch.

                if(villageois.Select(v => v.stats.name).Contains(nameViewer)) //skip if viewer déja in village.
                    continue;

                Villageoi viewerSpawned = new Villageoi(level.idLevel);
                viewerSpawned.pos.setBoth(getVillageoiSpawner.pos.x, getVillageoiSpawner.pos.y);
                viewerSpawned.stats.name = nameViewer;
                viewerSpawned.stats.timeLastWalk = timeInLevel;
                viewerSpawned.setRandomStats();
                villageois.Add(viewerSpawned);
            }

            if(BotTwitch.pseudosViewerWaitingList.Count > 0){ //if has spawn villageoi, sort entity, and reset name pool.
                //Entity.SortAllEntitiesByYAxis();
                BotTwitch.pseudosViewerWaitingList = new();
            }

        }

        //update of all villageoi. --->
        foreach(Villageoi viewer in villageois){

            //walk randomely.
            if(viewer.state == "base" && ((timeInLevel - viewer.stats.timeLastWalk) >= Stats.timeBetweenTwoWalk)){ //start new random walk.
                
                viewer.setRandomPosToWalk(); //new random pos to walk.
                viewer.state = "walk0";

            }else if(viewer.state.Substring(0, 4) == "walk"){ //update walk.

                float distToDestination = Vector2.dist(viewer.pos, viewer.stats.getPosToWalk); //walk pos.
                Vector2 walkVector = new Vector2(
                    viewer.stats.getPosToWalk.x - viewer.pos.x,
                    viewer.stats.getPosToWalk.y - viewer.pos.y
                );
                
                if(walkVector.x < -0.5f) //reverce sprite anime walk.
                    viewer.reverceSprite.x = -1f; 
                else if(walkVector.x > 0.5f)
                    viewer.reverceSprite.x = 1f; 

                Vector2.normalize(ref walkVector);
                viewer.pos.setBoth(
                    viewer.pos.x + walkVector.x * Stats.speedWalk * Update.deltaTime,
                    viewer.pos.y + walkVector.y * Stats.speedWalk * Update.deltaTime
                );
                viewer.updateAnimeWalk(timeInLevel); //anime walk.

                if(Vector2.dist(viewer.pos, viewer.stats.getPosToWalk) > distToDestination){ //end walk.
                    viewer.state = "base";
                    viewer.stats.timeLastWalk = timeInLevel;
                }

            }
        }



        //player move. --->
        Vector2 newPosPlayer = new Vector2( //input axis.
            (Input.hasKeyPressed('Q')? -1f: 0f) + (Input.hasKeyPressed('D')? 1f: 0f),
            (Input.hasKeyPressed('Z')? -1f: 0f) + (Input.hasKeyPressed('S')? 1f: 0f)
        );

        //set orientation of sprite (left, right)
        if(newPosPlayer.x < -0.5f)
            MenuChoosePlayer.level.getPlayer.reverceSprite.x = -1f; 
        else if(newPosPlayer.x > 0.5f)
            MenuChoosePlayer.level.getPlayer.reverceSprite.x = 1f; 
        
        //set state walk or stand.
        if(newPosPlayer.x == 0f && newPosPlayer.y == 0f){
            MenuChoosePlayer.level.getPlayer.state = "base";
        }else{
            if(MenuChoosePlayer.level.getPlayer.state.Substring(0, 4) != "walk"){
                MenuChoosePlayer.level.getPlayer.state = "walk0";
                MenuChoosePlayer.level.getPlayer.timeWhenStartAnime = timeInLevel;
            }else{
                MenuChoosePlayer.level.getPlayer.updateAnimeWalk(timeInLevel);
            }
        }

        Vector2.normalize(ref newPosPlayer); //normalize.
        newPosPlayer.setBoth( //apply speed (and delta time).
            newPosPlayer.x * SkinVillageoi.speedWalk * Update.deltaTime,
            newPosPlayer.y * SkinVillageoi.speedWalk * Update.deltaTime
        );
        newPosPlayer.setBoth( //apply player pos.
            newPosPlayer.x + MenuChoosePlayer.level.getPlayer.pos.x,
            newPosPlayer.y + MenuChoosePlayer.level.getPlayer.pos.y
        );

        //tcheck colide, if not coliding, apply move.
        //todo..

        MenuChoosePlayer.level.getPlayer.pos.setBoth( //apply player move.
            newPosPlayer.x,
            newPosPlayer.y
        );


        //input escape open menu. --->
        if(Input.hasKeyPressedNow('\r')){
            if(buttonOption != null)
                buttonOption.actionClick();
        }


        //order entity in y axis. --->
        if(timeInLevel - timeLastOrder >= timeBetweenTwoOrder){
            timeLastOrder = timeInLevel;
            Entity.SortAllEntitiesByYAxis();
        }

        base.update();

    }

    public static void unActiveStatic(){ level.unActive(); }
    public override void unActive()
    {

        villageoiSpawner = null;
        villageois = new();
        buttonOption = null;
        listGrassAroundPlayer = new();
        mobSpawners.ForEach(ms => { //free mob spawner and mobs spawned (double link).
            ms.mobSpawned.ForEach(m => m.archFromSpawn = null);
            ms.mobSpawned = new();
        });
        mobSpawners = new();

        base.unActive();

    }

}