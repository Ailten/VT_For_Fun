using Raylib_cs;

class Program
{
    public static void Main(string[] args)
    {
        //make a window.
        Window.init("VT for fun");
        Window.isDebugMode = true;

        //setup the update.
        Update.fps = 30;
        Update.init();

        //load all font.
        Font.loadAFont("IntensaFuente");

        //load all levels.
        Level.loadAllLevel();

        //active level start.
        MenuSave.activeStatic();

        //loop update.
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            //resize window.
            if (Raylib.IsWindowResized())
            {
                Window.resize(new Vector2(
                    Raylib.GetScreenWidth(),
                    Raylib.GetScreenHeight()
                ));
            }

            //inputs user.
            Input.update(); //process input keyboard.
            Mouse.update(); //process input of mouse.

            //flag exit for close window.
            if(Window.isExit)
                break;

            //update executif.
            Level.executeAllUpdate(); //update of all level active.
            Level.updateTransitionLevel(); //update of transition level.

            //camera follow an entity.
            Camera.update();

            //cleane.
            Raylib.ClearBackground(Color.Black);

            //draw phase.
            Window.draw();

            //wait in update, set deltaTime.
            Update.wait();

            Raylib.EndDrawing();
        }

        //free all elements load.
        Level.deinit(); //unactive all level acitve.
        Font.deinit(); //free font load.
        Sprite.deinit(); //free all texture.
        BotTwitch.deinit(); //disconect bot twitch.

        //close window.
        Raylib.CloseWindow();
    }
}