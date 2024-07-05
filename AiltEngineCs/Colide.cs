
using System.Numerics;

public static class Colide
{

    // <--- Mouse colide UI (START) --->
    public static Entity? getEntityTriggerdByMouse(Vector2 posMouse)
    {
        foreach(Entity e in Entity.entities){

            if( //list of condition for skip colide check.
                !e.isActive || //skip entity disable.
                !e.isLevelActive || //skip entity with level disable.
                !e.hasTag(Tag.isHud) || //skip entity not hud.
                e.geometryTrigger == null //skip entity without geometry.
            )
                continue;

            foreach(Geometry g in e.geometryTrigger){

                Vector2 entityPos = (!e.hasTag(Tag.isHudScrollable)? e.pos: //entity pos.
                    new Vector2(e.pos.x, e.pos.y - Camera.posScroll) //entity pos + pos camera (for scroll value).
                );
                if(checkIfPosIsInGeometry(posMouse, entityPos, g)){ //check colide pos mouse is into g.
                    return e;
                }

            }

        }

        return null;
    }

    private static bool checkIfPosIsInGeometry(Vector2 pos, Vector2 entityPos, Geometry geometry)
    {
        if(geometry is Rect){
            return posIsInRect(pos, entityPos, (Rect)geometry);
        }
        if(geometry is Circle){
            return posIsInCircle(pos, entityPos, ((Circle)geometry).rayon);
        }
        return false;
    }

    public static bool posIsInRect(Vector2 pos, Vector2 posRect, Rect rect)
    {
        return (
            Math2.inRange(
                posRect.x + rect.posTopLeft.x, 
                pos.x, 
                posRect.x + rect.posDownRight.x
            ) &&
            Math2.inRange(
                posRect.y + rect.posTopLeft.y, 
                pos.y, 
                posRect.y + rect.posDownRight.y
            )
        );
    }
    public static bool posIsInCircle(Vector2 pos, Vector2 posCircle, float rayon)
    {
        return Vector2.dist(pos, posCircle) < rayon;
    }
    // <--- Mouse colide UI (END) --->

}