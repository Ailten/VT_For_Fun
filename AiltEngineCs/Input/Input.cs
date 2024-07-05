
using System.Text.RegularExpressions;
using Raylib_cs;

public static class Input
{
    public static bool isPromptMode = false;
    public static string fullPrompt = "";

    private static List<KeyInput> inputPressed = new();
    public static List<KeyInput> getInputPressed{
        get{ return inputPressed; }
    }

    public static bool isScrollMode = false;
    public const float speedScroll = 30f;
    public static float maxScroll = 720f;

    public static void update()
    {

        //mode input game (released).
        for(int i=inputPressed.Count-1; i>=0; i--){
            char charPress = Keyboard.castCharFrToEn(inputPressed[i].charKey); //to querty.
            if(Raylib.IsKeyReleased((KeyboardKey)charPress)){
                inputPressed.RemoveAt(i);
            }
        }
        
        if(Level.isTransitionActive) //skip input if in transtion level.
            return;

        //mode input prompt.
        if(isPromptMode){

            int intCharPressed;
            while((intCharPressed = Raylib.GetCharPressed()) > 0){
                fullPrompt += (char)intCharPressed;
            }

            //press enter key.
            if(Raylib.IsKeyDown(Raylib_cs.KeyboardKey.Enter)){
                fullPrompt += '\n';
            }
            
            //erase key pressed.
            if(Raylib.IsKeyDown((Raylib_cs.KeyboardKey)259) && fullPrompt.Length > 0){
                fullPrompt = fullPrompt.Substring(0, fullPrompt.Length-1);
            }

            return;
        }

        //mode scroll.
        if(isScrollMode){
            float scroll = Raylib.GetMouseWheelMove();
            if(scroll != 0f)
                Camera.posScroll = Math.Clamp(Camera.posScroll - scroll * speedScroll, 0f, maxScroll);
        }

        //mode input game (pressed).
        int keyPressedInt;
        while((keyPressedInt = Raylib.GetKeyPressed()) > 0){

            char charPress = Keyboard.castCharEnToFr((char)keyPressedInt); //get char azerty.

            if(inputPressed.Find(ki => { return (ki.charKey == charPress); }) != null) //skip input if already in list.
                continue;

            inputPressed.Add(new(){ //add in list if press this update.
                charKey = charPress, 
                timePressed = Update.time
            });

        }

    }

    public static bool hasKeyPressed(char charAsk)
    {
        char charRemap = Keyboard.remap("customKeyboard", charAsk);
        foreach(KeyInput keyPressed in inputPressed){
            if(keyPressed.charKey == charRemap)
                return true;
        }
        return false;
    }
    public static bool hasKeyPressedNow(char charAsk)
    {
        char charRemap = Keyboard.remap("customKeyboard", charAsk);
        foreach(KeyInput keyPressed in inputPressed){
            if(keyPressed.charKey == charRemap && keyPressed.timePressed == Update.time)
                return true;
        }
        return false;
    }
}

public class KeyInput
{
    public char charKey;
    public int timePressed;
}