
using Raylib_cs;
using System.Text;
using System.Runtime.InteropServices;

public static class Font
{

    private static Dictionary<string, Raylib_cs.Font> fontsLoaded = new();

    public static void loadAFont(string fontName)
    {
        fontsLoaded.Add(fontName, Raylib.LoadFont($"assets/Font/{fontName}.ttf"));
    }

    public static void deinit()
    {
        foreach(KeyValuePair<string, Raylib_cs.Font> keyValuePairFontLoaded in fontsLoaded){
            Raylib.UnloadFont(keyValuePairFontLoaded.Value);
        }
    }

    public static Raylib_cs.Font getFont(string fontName)
    {
        return fontsLoaded[fontName];
    }

}