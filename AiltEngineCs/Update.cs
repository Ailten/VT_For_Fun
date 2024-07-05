using Raylib_cs;
using System.Diagnostics;

public static class Update
{
    private static int timeOfOneFrame;
    public static int fps
    {
        set{ timeOfOneFrame = (1000 / value); }
    }

    private static int _deltaTime = 0;
    public static float deltaTime
    {
        get{ return (((float)_deltaTime) / 1000.0f) * timeMultiplyer; }
    }

    public static float timeMultiplyer = 1.0f;

    private static Stopwatch stopWatchForUpdateFrame = new Stopwatch();
    private static int timeFromStartGame;
    public static int time
    {
        get{ return timeFromStartGame; }
    }

    public static int timeWhenLastSave;

    public static void init()
    {
        //parameters for manage time in the game.
        stopWatchForUpdateFrame.Start();
        timeFromStartGame = 0;

        //disable FPS system of RayLib (never wait in endDrawing).
        Raylib.SetTargetFPS(1000);
    }

    public static void wait()
    {
        //mesure milisecondes from last update event.
        stopWatchForUpdateFrame.Stop();
        int timeFromLastUpdate = (int)stopWatchForUpdateFrame.ElapsedMilliseconds;
        int frameSkip = 0;

        //frames skiped when to long to render the sceen.
        while(timeFromLastUpdate > timeOfOneFrame){
            frameSkip++;
            timeFromLastUpdate -= timeOfOneFrame;
        }

        //do the sleep.
        Thread.Sleep(timeOfOneFrame - timeFromLastUpdate);
        timeFromLastUpdate++;

        //actualise the delta time.
        _deltaTime = timeOfOneFrame * timeFromLastUpdate;
        
        //increase time from start game.
        timeFromStartGame += _deltaTime;

        //reset stopWatch for next update event.
        stopWatchForUpdateFrame.Reset();
        stopWatchForUpdateFrame.Start();
    }

}