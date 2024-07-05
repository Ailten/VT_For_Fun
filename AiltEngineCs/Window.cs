using System.Numerics;
using Raylib_cs;

public static class Window
{

    public static Vector2 size = new Vector2(1280, 720);

    private static Vector2 posDecalCanvas = new Vector2(0, 0);
    private static float posScaleCanvas = 1.0f;
    public static float scaleCanvasWindow{
        get{ return posScaleCanvas; }
    }

    public static bool isDebugMode;

    public static bool isExit;

    public static void init(string titleWindow)
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow); //window can be resize.
        Raylib.InitWindow((int)size.x, (int)size.y, titleWindow);
        Raylib.SetExitKey(Raylib_cs.KeyboardKey.Null); //replace escape key (not close the window).
    }

    public static void resize(Vector2 newSizeWindow)
    {
        float ratioWindow = newSizeWindow.x / newSizeWindow.y;
        float ratio = size.x / size.y;

        if(ratioWindow < ratio){ //window is a square but canvas is an horizontal rectangle.
            //set scale canvas.
            posScaleCanvas = newSizeWindow.x / size.x;

            //set decal canvas.
            posDecalCanvas.x = 0.0f;
            posDecalCanvas.y = (newSizeWindow.y - (newSizeWindow.x / ratio)) /2.0f;

            //set border black.
            rectBlackBorder[0] = 0; //rect top-left.
            rectBlackBorder[1] = 0;
            rectBlackBorder[2] = (int)newSizeWindow.x;
            rectBlackBorder[3] = (int)posDecalCanvas.y;
            rectBlackBorder[4] = 0; //rect down-right.
            rectBlackBorder[5] = (int)(newSizeWindow.y - posDecalCanvas.y);
            rectBlackBorder[6] = (int)newSizeWindow.x;
            rectBlackBorder[7] = (int)posDecalCanvas.y +1;
        }else{ //window in an horizontal rectangle but canvas is a square.
            //set scale canvas.
            posScaleCanvas = newSizeWindow.y / size.y;

            //set decal canvas.
            posDecalCanvas.x = (newSizeWindow.x - (newSizeWindow.y * ratio)) /2.0f;
            posDecalCanvas.y = 0.0f;

            //set border black.
            rectBlackBorder[0] = 0; //rect top-left.
            rectBlackBorder[1] = 0;
            rectBlackBorder[2] = (int)posDecalCanvas.x;
            rectBlackBorder[3] = (int)newSizeWindow.y;
            rectBlackBorder[4] = (int)(newSizeWindow.x - posDecalCanvas.x); //rect down-right.
            rectBlackBorder[5] = 0;
            rectBlackBorder[6] = (int)posDecalCanvas.x +1;
            rectBlackBorder[7] = (int)newSizeWindow.y;
        }

    }

    private static int[] rectBlackBorder = {0,0,0,0, 0,0,0,0}; //array of two rect, for black border pos and size.

    private static void drawBlackBorder()
    {
        Raylib.DrawRectangle(
            rectBlackBorder[0], 
            rectBlackBorder[1], 
            rectBlackBorder[2], 
            rectBlackBorder[3], 
            Color.Black
        );
        Raylib.DrawRectangle(
            rectBlackBorder[4], 
            rectBlackBorder[5], 
            rectBlackBorder[6], 
            rectBlackBorder[7], 
            Color.Black
        );
    }

    public static void resizePosCanvasInto(ref Vector2 posInCanvas)
    {
        //scale canvas.
        posInCanvas.x *= scaleCanvasWindow;
        posInCanvas.y *= scaleCanvasWindow;

        //replace decal canvas.
        posInCanvas.x += posDecalCanvas.x;
        posInCanvas.y += posDecalCanvas.y;
    }

    public static void draw()
    {

        foreach(Entity e in Entity.entities){

            //skip all entity disable.
            if(!e.isActive || !e.isLevelActive)
                continue;

            //evalue pos in world.
            Vector2 posEncrageInCanvas = new Vector2(e.pos); //get pos duplicate.
            if(!e.hasTag(Tag.isHud)){ //cam replace.
                Camera.remapPosWorldToFollowCam(ref posEncrageInCanvas);
            }
            if(e.hasTag(Tag.isHudScrollable)){ //add pos cam (for scroll ui).
                posEncrageInCanvas.y -= Camera.posScroll;
            }
            Window.resizePosCanvasInto(ref posEncrageInCanvas); //cast pos canvas to pos canvas resized.

            //draw sprite if has one.
            if(e.sprite != null){
                Raylib.DrawTexturePro(
                    e.sprite.texture,
                    e.sprite.getRectSource(e.state, e.reverceSprite),
                    Rect.getRectDest(posEncrageInCanvas, e.sprite.size, e.scale),
                    new System.Numerics.Vector2( //origine.
                        e.encrage.x * e.size.x * scaleCanvasWindow *e.scale.x, 
                        e.encrage.y * e.size.y * scaleCanvasWindow *e.scale.x
                    ),
                    e.rotate, //rotation.
                    Color.White
                );
            }
            
            //add draw after sprite.
            e.addDraw(posEncrageInCanvas);

            //debug block (for colision of all entity).
            if(isDebugMode){

                if(e.geometrySolid != null){ //debug geometry solid.

                    foreach(Geometry g in e.geometrySolid){
                        foreach(Line l in g.linesArray){

                            Raylib.DrawLine(
                                (int)(l.posStart.x * scaleCanvasWindow + posEncrageInCanvas.x),
                                (int)(l.posStart.y * scaleCanvasWindow + posEncrageInCanvas.y),
                                (int)(l.posEnd.x * scaleCanvasWindow + posEncrageInCanvas.x),
                                (int)(l.posEnd.y * scaleCanvasWindow + posEncrageInCanvas.y),
                                Color.Blue
                            );

                        }
                    }

                }

                if(e.geometryTrigger != null){ //debug geometry solid

                    foreach(Geometry g in e.geometryTrigger){
                        foreach(Line l in g.linesArray){

                            Raylib.DrawLine(
                                (int)(l.posStart.x * scaleCanvasWindow + posEncrageInCanvas.x),
                                (int)(l.posStart.y * scaleCanvasWindow + posEncrageInCanvas.y),
                                (int)(l.posEnd.x * scaleCanvasWindow + posEncrageInCanvas.x),
                                (int)(l.posEnd.y * scaleCanvasWindow + posEncrageInCanvas.y),
                                Color.Orange
                            );

                        }
                    }
                    
                }

            }

        }

        //debug block (for mouse).
        if(isDebugMode){

            Raylib.DrawCircleLines( //debug pos mouse.
                (int)Mouse.pos.x, (int)Mouse.pos.y, //pos.
                5f * scaleCanvasWindow, //radius.
                Color.Blue
            );
            
        }

        //draw black border (of resize window).
        drawBlackBorder();

        //draw transition level (if has one).
        if(Level.isTransitionActive){

            //draw rectangle low opacity.
            Raylib.DrawTexturePro(
                Level.blackOpacityRamp.texture, //texture ramp.
                Level.blackOpacityRamp.getPixelRamp((int)(Level.transitionOpacity * 255f)), //rect source texture.
                new Rectangle( //rect dest canvas.
                    Window.posDecalCanvas.x, Window.posDecalCanvas.y, //pos.
                    Window.size.x * scaleCanvasWindow, Window.size.y * scaleCanvasWindow //size.
                ),
                new System.Numerics.Vector2(0, 0), //origine.
                0f, //rotation.
                Color.White //color.
            );

        }

    }

}