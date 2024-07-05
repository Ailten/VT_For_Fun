
using Raylib_cs;

public static class Mouse
{
    private static Entity mouse = new(){
        tags = new Tag[]{ Tag.isMouse, Tag.isHud },
        zIndex = 1
        //no geometry, use colide into rect (specific colision for UI).
    };
    public static Vector2 pos{
        get{ return mouse.pos; }
    }

    private static Entity? entitySelectedByMouse;

    public static void update()
    {
        if(Level.isTransitionActive) //skip input if in transtion level.
            return;
            
        mouse.pos.setBothByNumVector(Raylib.GetMousePosition());//get pos mouse in window.
        bool isLeftClick = Raylib.IsMouseButtonDown(MouseButton.Left); //input click left.

        Entity? entitySelectedThisFrame = Colide.getEntityTriggerdByMouse(Mouse.pos);

        if( //if click.
            isLeftClick && 
            entitySelectedByMouse != null && 
            entitySelectedByMouse.mouseClick != null
        ){
            entitySelectedByMouse.mouseClick();
        }

        if( //if mouse enter.
            entitySelectedThisFrame != null &&
            (entitySelectedByMouse == null || entitySelectedByMouse.idEntity != entitySelectedThisFrame.idEntity)
        ){
            entitySelectedThisFrame.mouseEnter();
        }

        if( //if mouse exit.
            entitySelectedByMouse != null &&
            (entitySelectedThisFrame == null || entitySelectedByMouse.idEntity != entitySelectedThisFrame.idEntity)
        ){
            entitySelectedByMouse.mouseExit();
        }

        //save the entity hover for next frame.
        entitySelectedByMouse = entitySelectedThisFrame;

    }

}